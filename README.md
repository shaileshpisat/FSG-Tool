# FSG-Tool (Folder Structure Generator) ![Badge](https://badgen.net/badge/build/passing/green?icon=github)

> Automatically create folder structures from markdown code blocks

## Features

- 🚀 Instant folder/file structure generation
- 📋 Parses standard tree diagrams from READMEs
- ✔️ Supports both directories (with `/`) and files
- ⚙️ Configurable through command-line arguments

## Installation

1. Build the Project (Release Mode)
Open Command Prompt or PowerShell in the project root and run:
```bash
# Clone repository
git clone https://github.com/shaileshpisat/FSG-Tool.git
cd FSG-Tool

# Build release version
dotnet publish -c Release -o ./dist
```
This creates a dist folder with the compiled executable.

2. Add to PATH (Permanent Access)
Using System Environment Variables
Press Win + S, type "Environment Variables", and open "Edit the system environment variables".

Click "Environment Variables" → Under "System variables", select "Path" → Click "Edit".

Click "New" and paste the full path to your dist folder (e.g., C:\Projects\FSG-Tool\dist).

Click OK → OK → OK to save.

3. Verify Installation
Run in a new terminal (to reload PATH):
```
FSGTool
```
If correctly added, it should show 'Error: project-structure.md file not found'.

# Usage
1. Create a project-structure.md with your structure in the source code folder:

```
project/
├── src/
│ └── Program.cs
├── configs/
│ └── settings.json
└── README.md
```

2. Run the tool:
Run in a new terminal (to reload PATH):
```
FSGTool
```
The folders and files in the project structure will not be created.

# Contribution
1. Fork the project
2. Create your feature branch (git checkout -b feature/AmazingFeature)
3. Commit changes (git commit -m 'Add amazing feature')
4. Push to branch (git push origin feature/AmazingFeature)

Open a Pull Request

# License
MIT © 2024 Shailesh Pisat

For suggestions and comments reach out to [me](mailto:ace.dev.100@gmail.com)



