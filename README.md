# 📝 Gestor de Notas con MongoDB

Una aplicación web moderna y elegante para gestionar tus notas, construida con ASP.NET Core y MongoDB.

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![.NET](https://img.shields.io/badge/.NET-6.0+-purple.svg)
![MongoDB](https://img.shields.io/badge/MongoDB-4.4+-green.svg)

## Características

- **CRUD Completo**: Crea, lee, actualiza y elimina notas
- **Interfaz Moderna**: Diseño amigable
- **Búsqueda en Tiempo Real**: Filtra tus notas instantáneamente
- **Rápido y Eficiente**: Powered by MongoDB

## Capturas de Pantalla

### Vista Principal
<img width="1275" height="834" alt="Image" src="https://github.com/user-attachments/assets/162eef35-d4e1-4c88-935a-006ad768a594" />
*Interfaz principal mostrando la lista de notas con búsqueda y filtros*

### Crear/Editar Nota
<img width="1278" height="838" alt="Image" src="https://github.com/user-attachments/assets/31d5ae94-fa71-469a-a3a7-ce1ba50e6ae9" />
<img width="1273" height="837" alt="Image" src="https://github.com/user-attachments/assets/bfb7204e-e74b-404e-90cb-244bef6e123a" />
<img width="1276" height="832" alt="Image" src="https://github.com/user-attachments/assets/45ea4ddc-43f6-421f-9590-07c7170b8851" />
<img width="1277" height="845" alt="image" src="https://github.com/user-attachments/assets/c2a9e00e-c8cc-4bd0-be95-2582b3e4a825" />
*Formulario intuitivo para crear y editar notas*

### MongoDB
<img width="661" height="673" alt="image" src="https://github.com/user-attachments/assets/7f033e21-745d-4948-b729-01ec51dfceed" />
<img width="1320" height="183" alt="image" src="https://github.com/user-attachments/assets/2a9e81e5-83f3-4c96-a68d-5dd4ce2ae881" />
*Pagina Oficial MongoDB*

## Inicio Rápido

### Requisitos Previos

Asegúrate de tener instalado:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) o superior
- [MongoDB Community Server](https://www.mongodb.com/try/download/community) (local) o cuenta en [MongoDB Atlas](https://www.mongodb.com/atlas) (nube)
- [Git](https://git-scm.com/)
- Editor de código ([Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/))

### Verificar Instalaciones

```bash
# Verificar .NET
dotnet --version
# Debe mostrar: 6.0.x o superior

# Verificar Git
git --version

# Verificar MongoDB (solo si usas instalación local)
mongod --version
```

## Instalación

### 1️- Clonar el Repositorio

```bash
git clone https://github.com/WakandianShield/GestorNotas_MongoDB.git
cd GestorNotas_MongoDB
```

### 2- Configurar MongoDB


#### ☁️ (Gratis, en la nube)

1. Crea una cuenta gratuita en https://cloud.mongodb.com/
2. Crea un nuevo cluster (selecciona el tier gratuito M0)
3. Crea un usuario de base de datos:
   - Ve a "Database Access"
   - Click "Add New Database User"
   - Guarda usuario y contraseña
4. Conectar a C#:
   - Ve a "Database"
   - Ve a "Clusters"
   - Ahí aparecera tu base de datos
     
5. Obtén tu cadena de conexión:
   - Selecciona "Connect"
   - Selecciona "MongoDB for VS code"
   - Copia la connection string que se te asigne
   - Cambia el usuario y la contraseña por la que creaste
   - pega el link de la conexión en el json en esta sección
   "ConnectionString": "AQUI PON TU STRING CONNECTION",

### 3️- Configurar la Aplicación

#### Crear Archivo de JSON en ejectuble

1. Abre la carpeta en donde aparecen los paquetes de nuget y el .exe del form
2. Agrega ahí un archivo nombrado como "appsettings.json" 
3. Escribe en el archivo el json que esta a continuación pero reemplazando la ConnectionString por la tuya 

**Para MongoDB:**
```json
{
 "MongoDB": {
  "ConnectionString": "mongodb+srv://tuusuario:tupassword@cluster0.xxxxx.mongodb.net/",
  "DatabaseName": "GestorNotas",
  "CollectionName": "Notas"
  }
}
```

> ⚠️ **Importante**: Reemplaza `tuusuario`, `tupassword` y `cluster0.xxxxx` con tus credenciales reales de MongoDB Atlas
