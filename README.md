<h1 align="center">Notes Manager with MongoDB</h1>

<p align="center">
  <img src="https://img.shields.io/badge/.NET%208.0-512BD4?logo=dotnet&logoColor=white&style=plastic" />
  <img src="https://img.shields.io/badge/C%23-512AD5?logo=csharp&logoColor=white&style=plastic" />
  <img src="https://img.shields.io/badge/MongoDB-47A248?logo=mongodb&logoColor=white&style=plastic" />
</p>

<p align="center">
A modern and elegant desktop application to manage your notes.
<br>
Built with Windows Forms and MongoDB.
</p>

<h2 align="center">Features</h2>

tr

<h2 align="center">Quick Start</h2>

<h3 align="center">Prerequisites</h3>

<p align="center">
Make sure you have installed:
</p>

<div align="center">
  <ul style="display: inline-block; text-align: left;">
    <li><a href="https://git-scm.com/">Git</a></li>
    <li><a href="https://dotnet.microsoft.com/es-es/download/dotnet/8.0">.NET 8.0</a> or higher</li>
    <li><a href="https://visualstudio.microsoft.com/">Visual Studio 2022</a> or <a href="https://code.visualstudio.com/">VS Code</a></li>
    <li><a href="https://www.mongodb.com/try/download/community">MongoDB Local</a> or <a href="https://www.mongodb.com/atlas">MongoDB Cloud</a></li>
  </ul>
</div>

<h3 align="center">Verify Installations</h3>

<pre align="center">
# Verify .NET
dotnet --version
# Should display: 8.0.x or higher

# Verify Git
git --version

# Verify MongoDB (only if using local installation)
mongod --version
</pre>

<h2 align="center">Installation</h2>

<h3 align="center">1 - Clone the Repository</h3>

<pre align="center">
git clone https://github.com/WakandianShield/GestorNotas_MongoDB.git
cd GestorNotas_MongoDB
</pre>

<h3 align="center">2 - Configure the Application</h3>

<h4 align="center">Create appsettings.json file in the executable folder</h4>

<div align="center">
<ol style="display: inline-block; text-align: left;">
  <li>Open the folder where the NuGet packages and the project <b>.exe</b> file are located</li>
  <li>Add a file named <b>appsettings.json</b></li>
  <li>Write the following content replacing the ConnectionString with yours:</li>
</ol>

<pre align="center">
{
  "MongoDB": {
    "ConnectionString": "mongodb+srv://youruser:yourpassword@cluster0.xxxxx.mongodb.net/",
    "DatabaseName": "GestorNotas",
    "CollectionName": "Notas"
  }
}
</pre>

<h3 align="center">3 - Configure Connection</h3>

<h4 align="center">MongoDB Atlas (Free, Cloud)</h4>

<div align="center">
  <ol style="display: inline-block; text-align: left;">
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

<h3 align="center">Main View</h3>

<p align="center">
  <img width="70%" src="https://github.com/user-attachments/assets/162eef35-d4e1-4c88-935a-006ad768a594" />
  <br>
  <i>Main interface showing the list of notes with search and filters</i>
</p>

<h3 align="center">Create / Edit Note</h3>

<p align="center">
  <img width="70%" src="https://github.com/user-attachments/assets/31d5ae94-fa71-469a-a3a7-ce1ba50e6ae9" /><br><br>
  <img width="70%" src="https://github.com/user-attachments/assets/bfb7204e-e74b-404e-90cb-244bef6e123a" /><br><br>
  <img width="70%" src="https://github.com/user-attachments/assets/45ea4ddc-43f6-421f-9590-07c7170b8851" /><br><br>
  <img width="70%" src="https://github.com/user-attachments/assets/c2a9e00e-c8cc-4bd0-be95-2582b3e4a825" />
  <br>
</p>

<h3 align="center">MongoDB</h3>

<p align="center">
  <img width="70%" src="https://github.com/user-attachments/assets/7f033e21-745d-4948-b729-01ec51dfceed" /><br><br>
  <img width="70%" src="https://github.com/user-attachments/assets/2a9e81e5-83f3-4c96-a68d-5dd4ce2ae881" />
  <br>
  <i>Official MongoDB Website</i>
</p>

<h2 align="center">Contact</h2>

<p align="center">
If you would like to provide feedback or share ideas, you can contact me through my GitHub profile or social media.
<br><br>
<b>Note:</b> Some files contain explanatory information in Spanish.
</p>
