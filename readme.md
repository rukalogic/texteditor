# TextEditor

A simple small text editor project — lightweight GUI application for creating, editing, and saving plain text files. Intended as a learning/demo app and starting point for further features.

## Features
- Open and save plain text files
- Create, edit, and close documents
- Basic Edit actions: Undo/Redo, Cut/Copy/Paste, Find/Replace
- Supports .rtf, .txtm .xaml files

## Requirements
- .NET SDK 6.0+ (or the target framework used by the project)
- JetBrains Rider, Visual Studio, or other IDE supporting the project type

## Getting started
1. Clone the repository:
  ```
  git clone <repo-url>
  cd TextEditor
  ```
2. Restore and build:
  ```
  dotnet restore
  dotnet build
  ```
3. Run:
  ```
  dotnet run --project src/TextEditor
  ```
  Or open the solution in Rider/Visual Studio and run from the IDE.

## Usage
- Use File > Open to load a file.
- Edit in the main editor area.
- Use File > Save / Save As to write changes.
- Use Edit menu or keyboard shortcuts for undo/redo and clipboard operations.

## Tests
- If tests exist:
  ```
  dotnet test
  ```

## Contributing
- Open an issue to discuss changes.
- Send pull requests for bug fixes and features.
- Follow the existing code style and provide unit tests where applicable.

## License
This project is available under the MIT License. See LICENSE file for details.

<!-- Replace placeholders (requirements, project path, repo url) with project-specific details. -->
