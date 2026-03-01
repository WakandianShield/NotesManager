


<h1 align="center">Notes Manager with MongoDB</h1>

<p align="center">
  <img src="https://img.shields.io/badge/C%23-512BD4?logo=dotnet&logoColor=white&style=plastic" />
  <img src="https://img.shields.io/badge/MongoDB-47A248?logo=mongodb&logoColor=white&style=plastic" />
</p>

<p align="center">
  A modern and elegant desktop application built with Windows Forms and MongoDB that allows you to efficiently manage your notes. 
  It provides full CRUD functionality to create, read, update, and delete notes, features a clean and user-friendly interface, 
  includes real-time search for instant filtering, and delivers fast, reliable performance through seamless MongoDB integration.
</p>

<h2 align="center">Requirements</h2>

<h3 align="center">Make sure you have installed</h3>

<p align="center">

</p>

<div>
  <ul>
    <li><a href="https://git-scm.com/">Git</a></li>
    <li><a href="https://www.mongodb.com/atlas">MongoDB Atlas</a></li>
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

<h3 align="center">1 - Clone the Repository</h3>

```bash
git clone https://github.com/WakandianShield/GestorNotas_MongoDB.git
```
```bash
cd GestorNotas_MongoDB
```

<h3 align="center">2 - Configure the Application</h3>

<ol>
  <li>Open the <b>.sln</b> on Visual Studio 2022 or vscode (use Visual Studio 2022 preferably)
  <li>Compile the code (errors will be thrown)</li>
  <li>Open the folder where the NuGet packages and the project <b>.exe</b> file are located</li>
  <li>Add a file named <b>appsettings.json</b></li>
  <li>Write the following content replacing the ConnectionString with yours:</li>
</ol>

```bash
{
  "MongoDB": {
    "ConnectionString": "mongodb+srv://youruser:yourpassword@cluster0.xxxxx.mongodb.net/",
    "DatabaseName": "GestorNotas",
    "CollectionName": "Notas"
  }
}
```

<h3 align="center">3 - Configure Connection</h3>

<div>
  <ol>
    <li>Create a free account at https://cloud.mongodb.com/</li>
    <li>Create a new cluster</li>
    <li>Create a database user:
      <ul>
        <li>Go to "Database Access"</li>
        <li>Click "Add New Database User"</li>
        <li>Save your username and password</li>
      </ul>
    </li>
    <li>Connect to C#:
      <ul>
        <li>Go to "Database"</li>
        <li>Go to "Clusters"</li>
        <li>Your database will appear there</li>
      </ul>
    </li>
    <li>Get your connection string:
      <ul>
        <li>Select "Connect"</li>
        <li>Select "MongoDB for VS Code"</li>
        <li>Copy the assigned connection string</li>
        <li>Replace the username and password with the ones you created</li>
        <li>Paste the connection link into the JSON parameter <b>"ConnectionString":</b></li>
      </ul>
    </li>
  </ol>
</div>

<p align="center">
<b>Important:</b> Replace the <b>"ConnectionString":</b> parameter in the JSON file with your real MongoDB Atlas credentials.
</p>

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
