using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotasApp
{
    public partial class MainForm : Form
    {
        private readonly MongoDBServices _mongoDBServices;
        private List<Nota> _notas;
        private Nota _notaSeleccionada;

        private Panel panelPrincipal, panelHeader, panelFormulario, panelLista, panelListaNotas;
        private TextBox txtTitulo, txtContenido, txtTags, txtBuscar;
        private Button btnNuevaNota, btnGuardar, btnEliminar, btnBuscar;
        private RadioButton rbTitulo, rbTag;
        private GroupBox groupBoxBuscar;
        private Label lblTituloApp, lblContadorNotas, lblSubtitulo;

        private readonly Color colorFondo = Color.FromArgb(12, 12, 18);
        private readonly Color colorPanel = Color.FromArgb(22, 20, 30);
        private readonly Color colorPanelClaro = Color.FromArgb(32, 30, 45);
        private readonly Color colorBoton = Color.FromArgb(42, 40, 62);
        private readonly Color colorTexto = Color.FromArgb(240, 240, 240);
        private readonly Color colorTextoClaro = Color.FromArgb(180, 180, 200);
        private readonly Color colorPrimario = Color.FromArgb(106, 176, 205);
        private readonly Color colorPrimarioHover = Color.FromArgb(126, 196, 225);
        private readonly Color colorSecundario = Color.FromArgb(255, 107, 107);
        private readonly Color colorSecundarioHover = Color.FromArgb(255, 127, 127);
        private readonly Color colorAcento = Color.FromArgb(168, 85, 247);
        private readonly Color colorBorde = Color.FromArgb(50, 50, 70);

        public MainForm()
        {
            InitializeComponent();
            _mongoDBServices = new MongoDBServices();
            _notas = new List<Nota>();
            _notaSeleccionada = null;
            CrearInterfaz();
            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;
        }

        private void CrearInterfaz()
        {
            this.Text = "GESTOR DE NOTAS - MONGODB";
            this.Size = new Size(1300, 850);
            this.MinimumSize = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colorFondo;
            this.Font = new Font("Segoe UI", 9);
            this.ForeColor = colorTexto;
            this.Padding = new Padding(10);

            panelHeader = CrearPanel(null, DockStyle.Top, 120, Color.Transparent);
            lblTituloApp = CrearLabel(panelHeader, "GESTOR DE NOTAS", new Point(40, 25),
                new Size(500, 50), new Font("Segoe UI", 28, FontStyle.Bold), colorPrimario);
            lblSubtitulo = CrearLabel(panelHeader, "ORGANIZA TUS IDEAS CON MONGODB JEJE",
                new Point(45, 75), new Size(300, 25), new Font("Segoe UI", 11, FontStyle.Italic), colorTextoClaro);
            lblContadorNotas = CrearLabel(panelHeader, "0 NOTAS", new Point(panelHeader.Width - 190, 40),
                new Size(150, 40), new Font("Segoe UI", 14, FontStyle.Bold), colorPrimario);
            lblContadorNotas.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            panelPrincipal = CrearPanel(this, DockStyle.Fill, 0, Color.Transparent);
            panelFormulario = CrearPanel(panelPrincipal, DockStyle.None, 0, colorPanel);
            panelLista = CrearPanel(panelPrincipal, DockStyle.None, 0, colorPanel);
            panelListaNotas = CrearPanel(panelLista, DockStyle.None, 0, Color.Transparent);
            panelListaNotas.AutoScroll = true;

            AplicarSombraSuave(panelFormulario);
            AplicarSombraSuave(panelLista);

            CrearFormulario();
            CrearListaNotas();
            CrearBusqueda();

            this.Shown += (s, e) => AjustarTamanos();
        }

        private void CrearFormulario()
        {
            int x = 30, y = 30;
            CrearLabel(panelFormulario, "EDITOR DE NOTAS", new Point(x, y), new Size(300, 35),
                new Font("Segoe UI", 18, FontStyle.Bold), colorPrimario);

            y += 50;
            CrearLabel(panelFormulario, "TITULO:", new Point(x, y), new Size(80, 25),
                new Font("Segoe UI", 11, FontStyle.Bold), colorTexto);
            txtTitulo = CrearTextBox(panelFormulario, new Point(x + 110, y), new Size(450, 35));

            y += 50;
            CrearLabel(panelFormulario, "CONTENIDO:", new Point(x, y), new Size(100, 25),
                new Font("Segoe UI", 11, FontStyle.Bold), colorTexto);
            txtContenido = CrearTextBox(panelFormulario, new Point(x + 110, y), new Size(450, 100));
            txtContenido.Multiline = true;

            var scrollTimer = new System.Windows.Forms.Timer();
            scrollTimer.Interval = 100;
            scrollTimer.Tick += (s, e) =>
            {
                if (txtContenido.IsHandleCreated)
                {
                    const int EM_GETLINECOUNT = 0x00BA;
                    const int EM_GETRECT = 0x00B2;

                    int lineCount = SendMessage(txtContenido.Handle, EM_GETLINECOUNT, 0, 0);

                    int visibleLines = txtContenido.ClientSize.Height / txtContenido.Font.Height;

                    if (lineCount > visibleLines)
                    {
                        txtContenido.ScrollBars = ScrollBars.Vertical;
                    }
                    else
                    {
                        txtContenido.ScrollBars = ScrollBars.None;
                    }
                }
            };
            scrollTimer.Start();

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);


            var lblTags = CrearLabel(panelFormulario, "ETIQUETAS:", new Point(x, 0), new Size(100, 25),
                new Font("Segoe UI", 11, FontStyle.Bold), colorTexto);
            lblTags.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            txtTags = CrearTextBox(panelFormulario, new Point(x + 110, 0), new Size(450, 35));
            txtTags.PlaceholderText = "SEPARADAS POR COMA (EJE: TRABAJO, PERSONAL, IDEAS)";
            txtTags.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            var panelBotones = CrearPanel(panelFormulario, DockStyle.None, 0, Color.Transparent,
                new Point(x, 0), new Size(560, 60));
            panelBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            btnNuevaNota = CrearBoton(panelBotones, "🆕 NUEVA NOTA", colorBoton, new Point(10, 15), colorTexto);
            btnGuardar = CrearBoton(panelBotones, "💾 GUARDAR", colorPrimario, new Point(160, 15), Color.White);
            btnEliminar = CrearBoton(panelBotones, "🗑️ ELIMINAR", colorSecundario, new Point(310, 15), Color.White);

            btnNuevaNota.Click += btnNuevaNota_Click;
            btnGuardar.Click += btnGuardar_Click;
            btnEliminar.Click += btnEliminar_Click;

            panelFormulario.SizeChanged += (s, e) =>
            {
                int bottomY = panelFormulario.Height - 140;
                lblTags.Location = new Point(x, bottomY);
                txtTags.Location = new Point(x + 110, bottomY);
                panelBotones.Location = new Point(x, bottomY + 70);
            };
        }

        private void CrearListaNotas()
        {
            CrearLabel(panelLista, "TUS NOTAS", new Point(30, 30), new Size(300, 35),
                new Font("Segoe UI", 18, FontStyle.Bold), colorPrimario);
            CrearLabel(panelLista, "HAZ CLICK EN TU NOTA PARA EDITARLA", new Point(32, 60),
                new Size(300, 25), new Font("Segoe UI", 10, FontStyle.Italic), colorTextoClaro);
        }

        private void CrearBusqueda()
        {
            var panelBusqueda = CrearPanel(panelLista, DockStyle.None, 0, colorPanelClaro,
                new Point(30, 85), new Size(560, 45));
            AplicarBordeSuave(panelBusqueda);

            txtBuscar = CrearTextBox(panelBusqueda, new Point(15, 12), new Size(400, 25));
            txtBuscar.PlaceholderText = "🔍 BUSCAR EN TUS NOTAS...";

            btnBuscar = CrearBoton(panelBusqueda, "BUSCAR", colorPrimario, new Point(470, 8),
                Color.White, new Size(80, 30));

            var btnLimpiar = CrearBoton(panelBusqueda, "X", colorTextoClaro, new Point(430, 8),
                colorPanel, new Size(30, 30));
            btnLimpiar.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                btnBuscar_Click(s, e);
            };

            panelBusqueda.SizeChanged += (s, e) =>
            {
                int margen = 15;
                int espacio = 8;

                btnBuscar.Location = new Point(panelBusqueda.Width - btnBuscar.Width - margen, 8);
                btnLimpiar.Location = new Point(btnBuscar.Left - btnLimpiar.Width - espacio, 8);

                int anchoDisponible = btnLimpiar.Left - margen - txtBuscar.Left;
                txtBuscar.Width = Math.Max(150, anchoDisponible);
            };

            groupBoxBuscar = new GroupBox
            {
                Text = "BUSCAR POR:",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = colorTexto,
                Size = new Size(200, 50),
                Location = new Point(30, 135),
                BackColor = Color.Transparent
            };

            rbTitulo = new RadioButton
            {
                Text = "TITULO",
                Font = new Font("Segoe UI", 9),
                ForeColor = colorTexto,
                Location = new Point(10, 20),
                Size = new Size(70, 20),
                Checked = true,
                BackColor = Color.Transparent
            };
            rbTag = new RadioButton
            {
                Text = "ETIQUETA",
                Font = new Font("Segoe UI", 9),
                ForeColor = colorTexto,
                Location = new Point(90, 20),
                BackColor = Color.Transparent
            };

            groupBoxBuscar.Controls.AddRange(new Control[] { rbTitulo, rbTag });
            panelLista.Controls.Add(groupBoxBuscar);
            btnBuscar.Click += btnBuscar_Click;
        }


        private Panel CrearPanel(Control parent, DockStyle dock, int height, Color backColor,
            Point? location = null, Size? size = null, Padding? padding = null)
        {
            var panel = new Panel { Dock = dock, BackColor = backColor };
            if (height > 0) panel.Height = height;
            if (location.HasValue) { panel.Location = location.Value; panel.Dock = DockStyle.None; }
            if (size.HasValue) panel.Size = size.Value;
            if (padding.HasValue) panel.Padding = padding.Value;
            parent?.Controls.Add(panel);
            return panel;
        }

        private Label CrearLabel(Control parent, string text, Point location, Size size, Font font, Color color)
        {
            var label = new Label
            {
                Text = text,
                Location = location,
                Size = size,
                Font = font,
                ForeColor = color,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            parent.Controls.Add(label);
            return label;
        }

        private TextBox CrearTextBox(Control parent, Point location, Size size)
        {
            var txt = new TextBox
            {
                Location = location,
                Size = size,
                BackColor = colorPanelClaro,
                ForeColor = colorTexto,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11)
            };
            parent.Controls.Add(txt);
            return txt;
        }

        private Button CrearBoton(Control parent, string texto, Color colorBase, Point location,
            Color colorTexto, Size? size = null)
        {
            var btn = new Button
            {
                Text = texto,
                Location = location,
                BackColor = colorBase,
                ForeColor = colorTexto,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = size ?? new Size(140, 40)
            };
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = colorBase == colorPrimario ? colorPrimarioHover :
                colorBase == colorSecundario ? colorSecundarioHover : ControlPaint.Light(colorBase, 0.3f);
            btn.MouseLeave += (s, e) => btn.BackColor = colorBase;

            parent.Controls.Add(btn);
            return btn;
        }

        private void MainForm_Resize(object sender, EventArgs e) => AjustarTamanos();

        private void AjustarTamanos()
        {
            if (panelHeader == null || panelPrincipal == null) return;

            lblContadorNotas.Location = new Point(panelHeader.Width - 190, 40);

            int margen = 20, margenSuperior = 100;
            int anchoDisponible = panelPrincipal.Width - (margen * 3);
            int anchoPanel = anchoDisponible / 2;
            int altoPanel = panelPrincipal.Height - margenSuperior - (margen * 2);

            panelFormulario.Location = new Point(margen, margenSuperior + margen);
            panelFormulario.Size = new Size(anchoPanel, altoPanel);

            panelLista.Location = new Point(anchoPanel + (margen * 2), margenSuperior + margen);
            panelLista.Size = new Size(anchoPanel, altoPanel);

            RedondearPanel(panelFormulario, 20);
            RedondearPanel(panelLista, 20);

            int anchoControl = panelFormulario.Width - 150;
            if (anchoControl < 200) anchoControl = 200;
            txtTitulo.Width = txtContenido.Width = txtTags.Width = anchoControl;

            int altoContenido = panelFormulario.Height - 300; 
            if (altoContenido < 150) altoContenido = 150; 
            txtContenido.Height = altoContenido;

            int anchoLista = panelLista.Width - 30;
            int altoLista = panelLista.Height - 220;
            panelListaNotas.Location = new Point(15, 200);
            panelListaNotas.Size = new Size(anchoLista, altoLista);

            if (_notas?.Count > 0) MostrarNotasEnLista(_notas);
        }

        private void MostrarNotasEnLista(List<Nota> notas)
        {
            panelListaNotas.SuspendLayout();
            panelListaNotas.Controls.Clear();

            if (notas.Count == 0)
            {
                MostrarMensajeVacio();
                lblContadorNotas.Text = "0 NOTAS";
                panelListaNotas.ResumeLayout();
                return;
            }

            int y = 10, espacioEntreNotas = 120;
            int anchoNota = panelListaNotas.Width - 30;

            foreach (var nota in notas.OrderByDescending(n => n.FechaCreacion))
            {
                panelListaNotas.Controls.Add(CrearPanelNota(nota, y, anchoNota));
                y += espacioEntreNotas;
            }

            lblContadorNotas.Text = $"{notas.Count} nota{(notas.Count != 1 ? "s" : "")}";
            panelListaNotas.ResumeLayout();
        }

        private Panel CrearPanelNota(Nota nota, int yPos, int ancho)
        {
            var panel = new Panel
            {
                Size = new Size(ancho, 110),
                Location = new Point(10, yPos),
                BackColor = nota.Id == _notaSeleccionada?.Id ?
                    Color.FromArgb(70, 106, 176, 205) : Color.FromArgb(200, 35, 33, 50),
                Cursor = Cursors.Hand,
                Tag = nota
            };

            RedondearPanel(panel, 15);
            AplicarSombraSuave(panel);
            AplicarBordeSuave(panel);

            var colorOriginal = panel.BackColor;
            panel.MouseEnter += (s, e) => { if (nota.Id != _notaSeleccionada?.Id) panel.BackColor = Color.FromArgb(220, 45, 43, 65); };
            panel.MouseLeave += (s, e) => { if (nota.Id != _notaSeleccionada?.Id) panel.BackColor = colorOriginal; };
            panel.Click += (s, e) => SeleccionarNota(nota);

            string titulo = "📝 " + (nota.Titulo.Length > 35 ? nota.Titulo.Substring(0, 35) + "..." : nota.Titulo);
            string contenido = nota.Contenido.Length > 70 ? nota.Contenido.Substring(0, 70) + "..." : nota.Contenido;
            string tags = "🏷️ " + (nota.Tags.Count > 0 ? string.Join(", ", nota.Tags.Take(3)) : "SIN ETIQUETAS");
            string fecha = "📅 " + nota.FechaCreacion.ToString("dd/MM/yyyy HH:mm");

            CrearLabel(panel, titulo, new Point(15, 15), new Size(ancho - 30, 30),
                new Font("Segoe UI", 13, FontStyle.Bold), colorPrimario).Click += (s, e) => SeleccionarNota(nota);
            CrearLabel(panel, contenido, new Point(15, 45), new Size(ancho - 30, 25),
                new Font("Segoe UI", 9.5f), colorTextoClaro).Click += (s, e) => SeleccionarNota(nota);

            var panelMetadata = CrearPanel(panel, DockStyle.None, 0, Color.Transparent, new Point(15, 75), new Size(ancho - 30, 25));
            panelMetadata.Click += (s, e) => SeleccionarNota(nota);

            CrearLabel(panelMetadata, tags, new Point(0, 2), new Size(ancho - 180, 20),
                new Font("Segoe UI", 8, FontStyle.Italic), colorAcento).Click += (s, e) => SeleccionarNota(nota);
            CrearLabel(panelMetadata, fecha, new Point(ancho - 180, 2), new Size(150, 20),
                new Font("Segoe UI", 8, FontStyle.Bold), colorTextoClaro).Click += (s, e) => SeleccionarNota(nota);

            return panel;
        }

        private void MostrarMensajeVacio()
        {
            var panelVacio = CrearPanel(panelListaNotas, DockStyle.None, 0, Color.Transparent,
                new Point(10, 50), new Size(panelListaNotas.Width - 20, 200));

            CrearLabel(panelVacio, "NO HAY NOTAS DISPONIBLES", new Point(120, 110),
                new Size(panelVacio.Width - 20, 30), new Font("Segoe UI", 16, FontStyle.Bold), colorTextoClaro);
        }

        private void AplicarSombraSuave(Control control)
        {
            control.Paint += (s, e) =>
            {
                using (var path = new GraphicsPath())
                {
                    var rect = control.ClientRectangle;
                    rect.Inflate(-2, -2);
                    path.AddRectangle(rect);
                    using (var pen = new Pen(Color.FromArgb(40, 0, 0, 0), 4))
                        e.Graphics.DrawPath(pen, path);
                }
            };
        }

        private void AplicarBordeSuave(Control control)
        {
            control.Paint += (s, e) =>
            {
                using (var pen = new Pen(colorBorde, 1))
                {
                    var rect = control.ClientRectangle;
                    rect.Width--; rect.Height--;
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };
        }

        private void RedondearPanel(Panel panel, int radio)
        {
            var path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(panel.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(panel.Width - radio, panel.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, panel.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();
            panel.Region = new Region(path);
        }

        private void RedondearControl(Control control, int radio)
        {
            var path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(control.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(control.Width - radio, control.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, control.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private async void MainForm_Load(object sender, EventArgs e) => await CargarNotas();

        private async Task CargarNotas()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _notas = await _mongoDBServices.GetNotasAsync();
                MostrarNotasEnLista(_notas);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"ERROR AL CARGAR NOTAS: {ex.Message}", "ERROR", MessageBoxIcon.Error);
                MostrarNotasEnLista(new List<Nota>());
            }
            finally { Cursor = Cursors.Default; }
        }

        private void SeleccionarNota(Nota nota)
        {
            _notaSeleccionada = nota;
            txtTitulo.Text = nota.Titulo;
            txtContenido.Text = nota.Contenido;
            txtTags.Text = string.Join(", ", nota.Tags);

            foreach (Control control in panelListaNotas.Controls)
            {
                if (control is Panel panel && panel.Tag is Nota panelNota)
                {
                    panel.BackColor = panelNota.Id == nota.Id ?
                        Color.FromArgb(70, 106, 176, 205) : Color.FromArgb(200, 35, 33, 50);
                }
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MostrarMensaje("EL TITULO ES OBLIGATORIO HIJO MIO", "ADVERTENCIA", MessageBoxIcon.Warning);
                txtTitulo.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var nota = new Nota
                {
                    Titulo = txtTitulo.Text.Trim(),
                    Contenido = txtContenido.Text.Trim(),
                    Tags = txtTags.Text.Split(',').Select(t => t.Trim()).Where(t => !string.IsNullOrEmpty(t)).ToList()
                };

                if (_notaSeleccionada == null)
                {
                    await _mongoDBServices.CreateNotaAsync(nota);
                    MostrarMensaje("NOTA CREADA EXITOSAMENTE", "EXITO", MessageBoxIcon.Information);
                }
                else
                {
                    nota.Id = _notaSeleccionada.Id;
                    nota.FechaCreacion = _notaSeleccionada.FechaCreacion;
                    await _mongoDBServices.UpdateNotaAsync(_notaSeleccionada.Id, nota);
                    MostrarMensaje("NOTA ACTUALIZADA EXITOSAMENTE", "EXITO", MessageBoxIcon.Information);
                }

                LimpiarFormulario();
                await CargarNotas();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"ERROR AL GUARDAR LA NOTA: {ex.Message}", "ERROR", MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void btnNuevaNota_Click(object sender, EventArgs e) => LimpiarFormulario();

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_notaSeleccionada == null)
            {
                MostrarMensaje("SELECCIONA UNA NOTA PARA ELIMINAR", "ADVERTENCIA", MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"¿ESTAS SEGURO QUE QUIERES ELIMINAR LA NOTA: \"{_notaSeleccionada.Titulo}\"?",
                "SIPO", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    await _mongoDBServices.DeleteNotaAsync(_notaSeleccionada.Id);
                    MostrarMensaje("NOTA ELIMINADA", "EXITO", MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarNotas();
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"ERROR AL ELIMINAR NOTA: {ex.Message}", "ERROR", MessageBoxIcon.Error);
                }
                finally { Cursor = Cursors.Default; }
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(criterio)) { await CargarNotas(); return; }

            try
            {
                Cursor = Cursors.WaitCursor;
                var resultados = rbTitulo.Checked ?
                    await _mongoDBServices.BuscarPorTituloAsync(criterio) :
                    await _mongoDBServices.BuscarPorTagAsync(criterio);
                MostrarNotasEnLista(resultados);
                lblTituloApp.Text = $"GESTOR DE NOTAS - {resultados.Count} RESULTADO(S)";
            }
            catch (Exception ex)
            {
                MostrarMensaje($"ERROR AL BUSCAR: {ex.Message}", "ERROR", MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void LimpiarFormulario()
        {
            _notaSeleccionada = null;
            txtTitulo.Clear(); txtContenido.Clear(); txtTags.Clear();
            foreach (Control control in panelListaNotas.Controls)
            {
                if (control is Panel panel && panel.Tag is Nota)
                    panel.BackColor = Color.FromArgb(200, 35, 33, 50);
            }
            txtTitulo.Focus();
        }

        private void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icon)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icon);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1300, 850);
            this.Name = "MainForm";
            this.ResumeLayout(false);
        }
    }
}