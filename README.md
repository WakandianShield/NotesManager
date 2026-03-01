<h1 align="center">Notes Manager with MongoDB</h1>

<p align="center">
  <img src="https://img.shields.io/badge/C%23-512BD4?logo=dotnet&logoColor=white&style=plastic" />
  <img src="https://img.shields.io/badge/Windows_Forms-0078D6?logo=windows&logoColor=white&style=plastic" />
  <img src="https://img.shields.io/badge/MongoDB-47A248?logo=mongodb&logoColor=white&style=plastic" />
</p>

<p align="center">
  A modern and elegant desktop application built with Windows Forms and MongoDB that allows you to efficiently manage your notes.
  It provides full CRUD functionality to create, read, update, and delete notes, features a clean and user-friendly interface,
  includes real-time search for instant filtering, and delivers fast, reliable performance through seamless MongoDB integration.
</p>

<h2 align="center">Overview</h2>

<p align="center">
  Notes Manager is a desktop productivity tool designed to give you a fast and reliable way to organize your thoughts, tasks, and ideas.
  Built on top of the .NET 8.0 ecosystem and powered by a cloud-hosted MongoDB Atlas database, the application eliminates the need for
  local database setup while providing enterprise-grade data persistence. The application follows a clean layered architecture, separating
  the UI layer (Windows Forms) from the data access layer (MongoDB Driver), ensuring maintainability and scalability for future features.
</p>

<h2 align="center">Features</h2>

<div>
  <ul>
    <li><b>Full CRUD</b> — Create, Read, Update, and Delete notes with ease</li>
    <li><b>Real-time Search</b> — Instantly filter notes as you type</li>
    <li><b>Cloud Storage</b> — All notes are persisted in MongoDB Atlas</li>
    <li><b>Clean UI</b> — Intuitive and user-friendly Windows Forms interface</li>
    <li><b>Fast Performance</b> — Seamless and responsive MongoDB integration</li>
    <li><b>Secure Config</b> — Credentials managed via <code>appsettings.json</code></li>
  </ul>
</div>

<h2 align="center">Tech Stack</h2>

<div>
  <ul>
    <li><b>Language:</b> C# (.NET 8.0)</li>
    <li><b>UI Framework:</b> Windows Forms (WinForms)</li>
    <li><b>Database:</b> MongoDB Atlas (Cloud)</li>
    <li><b>Driver:</b> MongoDB.Driver (NuGet)</li>
    <li><b>Configuration:</b> Microsoft.Extensions.Configuration + appsettings.json</li>
    <li><b>IDE:</b> Visual Studio 2022 / VS Code</li>
  </ul>
</div>

<h2 align="center">System Requirements</h2>

<h3 align="center">Make sure you have installed</h3>

<div>
  <ul>
    <li><a href="https://git-scm.com/">Git</a></li>
    <li><a href="https://www.mongodb.com/atlas">MongoDB Atlas</a> account</li>
    <li><a href="https://dotnet.microsoft.com/es-es/download/dotnet/8.0">.NET 8.0</a> or higher</li>
    <li><a href="https://visualstudio.microsoft.com/">Visual Studio 2022</a> or <a href="https://code.visualstudio.com/">VS Code</a></li>
  </ul>
</div>

<h3 align="center">Verify Installations</h3>

```bash
# Verify .NET (Should display: 8.0.x or higher)
dotnet --version
```
```bash
# Verify Git
git --version
```

<h2 align="center">Installation</h2>

<h3 align="center">1. Clone the Repository</h3>

```bash
git clone https://github.com/WakandianShield/NotesManager.git
```
```bash
cd NotesManager
```

<h3 align="center">2. Open & Build the Project</h3>

<div>
  <ol>
    <li>Open the <b>.sln</b> file in Visual Studio 2022 (recommended) or VS Code</li>
    <li>Build/Compile the solution — some errors will appear until configuration is complete</li>
    <li>Navigate to the output folder where the NuGet packages and <b>.exe</b> file are located</li>
    <li>Create a new file named <b>appsettings.json</b> in that folder</li>
    <li>Paste and fill in the following content with your credentials:</li>
  </ol>
</div>

```json
{
  "MongoDB": {
    "ConnectionString": "mongodb+srv://youruser:yourpassword@cluster0.xxxxx.mongodb.net/",
    "DatabaseName": "GestorNotas",
    "CollectionName": "Notas"
  }
}
```

<h2 align="center">Database Configuration</h2>

<div>
  <ol>
    <li>Create a free account at <a href="https://cloud.mongodb.com/">https://cloud.mongodb.com/</a></li>
    <li>Create a new cluster</li>
    <li>Create a database user:
      <ul>
        <li>Go to <b>"Database Access"</b></li>
        <li>Click <b>"Add New Database User"</b></li>
        <li>Save your username and password</li>
      </ul>
    </li>
    <li>Locate your cluster:
      <ul>
        <li>Go to <b>"Database"</b> → <b>"Clusters"</b></li>
        <li>Your database will appear there</li>
      </ul>
    </li>
    <li>Get your connection string:
      <ul>
        <li>Click <b>"Connect"</b></li>
        <li>Select <b>"MongoDB for VS Code"</b></li>
        <li>Copy the assigned connection string</li>
        <li>Replace <code>&lt;username&gt;</code> and <code>&lt;password&gt;</code> with your credentials</li>
        <li>Paste it into the <b>"ConnectionString"</b> field in <code>appsettings.json</code></li>
      </ul>
    </li>
  </ol>
</div>

<p align="center">
  <b>⚠️ Important:</b> Replace the <b>"ConnectionString"</b> parameter in the JSON file with your real MongoDB Atlas credentials.
  Never commit this file to a public repository — add <code>appsettings.json</code> to your <code>.gitignore</code>.
</p>

<h2 align="center">How to Run</h2>

<div>
  <ol>
    <li>Make sure <code>appsettings.json</code> is correctly configured with your MongoDB Atlas credentials</li>
    <li>Open the solution in Visual Studio 2022</li>
    <li>Build the project (<code>Ctrl + Shift + B</code>)</li>
    <li>Run the application (<code>F5</code> or <code>Ctrl + F5</code>)</li>
    <li>The app will launch and connect to your MongoDB Atlas database automatically</li>
  </ol>
</div>

<h2 align="center">Project Structure</h2>

```
NotesManager/                            # Root folder of the project
├── GestorNotas_MongoDB/                 # Folder containing all the application source code
│   ├── .gitignore                       # Files and folders to be ignored by Git
│   ├── Form1.cs                         # Main Windows Forms form
│   ├── GestorNotas_MongoDB.csproj       # Visual Studio project file
│   ├── GestorNotas_MongoDB.csproj.user  # User-specific configuration for Visual Studio
│   ├── MongoDBService.cs                # Class handling MongoDB connection and operations
│   ├── Nota.cs                          # Class representing the “Note” entity
│   └── Program.cs                       # Entry point of the application
├── GestorNotas_MongoDB.sln              # Visual Studio solution file that groups projects
├── README.md                            # This file
└── appsettings.example.json             # Example configuration file for database connection
```

<h2 align="center">Screenshots</h2>

<p align="center">
  <img width="70%" src="https://github.com/user-attachments/assets/162eef35-d4e1-4c88-935a-006ad768a594" />
  <br>
  <i>Main interface showing the list of notes with search and filters</i>
</p>

<h2 align="center">Contact</h2>

<p align="center">
  If you would like to provide feedback or share ideas, you can contact me through my GitHub profile or social media.
  <br><br>
  <b>Note:</b> Some files contain explanatory information in Spanish.
</p>
