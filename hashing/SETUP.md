# Project Setup Guide

This guide provides instructions on how to set up and run the Student Management System project using either **Visual Studio Code (Blue Icon)** or **Visual Studio 2022 (Purple Icon)**.

## Prerequisites
Before running the project, ensure you have the following installed:
1.  **.NET 8.0 SDK** (or the version matching the project).
2.  **SQL Server** (LocalDB or Full Instance).
3.  **SQL Server Management Studio (SSMS)** (optional, for running the database script).

---

## 1. Setting up the Database
Regardless of which editor you use, you need to set up the database first.

1.  Open **SQL Server Management Studio (SSMS)** or a SQL CLI tool.
2.  Open the `CREATE_CLEAN_DB.sql` file located in the project root.
3.  Execute the script. This will:
    *   Create a new database named `CleanStudentDB`.
    *   Create all necessary tables with the correct schema.
    *   Populate it with sample data (6 students, admins, etc.).
    *   *Note: Ensure the connection string in `Data/ApplicationDbContext.cs` matches your SQL Server instance (default is `.\SQLEXPRESS`).*

---

## 2. Using Visual Studio Code (Blue Icon) 🔵
*Best for lightweight, fast editing and command-line preference.*

### Extensions Required
Install the **C# Dev Kit** or the standard **C#** extension by Microsoft from the Extensions Marketplace.

### Steps to Run:
1.  **Open the Folder**:
    *   Open VS Code.
    *   Go to **File > Open Folder...**
    *   Select the `sysHashing` folder (the root folder containing `Program.cs`).

2.  **Open Terminal**:
    *   Press `` Ctrl + ` `` to open the integrated terminal.

3.  **Restore Dependencies**:
    *   Run the following command to download required libraries:
        ```powershell
        dotnet restore
        ```

4.  **Run the Application**:
    *   Run the command:
        ```powershell
        dotnet run
        ```
    *   Alternatively, press **F5** to start debugging.

---

## 3. Using Visual Studio 2022 (Purple Icon) 🟣
*Best for full-featured development, visual designers, and advanced debugging.*

### Steps to Run:
1.  **Open Project**:
    *   Launch **Visual Studio 2022**.
    *   Click on **"Open a project or solution"**.
    *   Navigate to the `sysHashing` folder.
    *   Select the `StudentManagementSystem.csproj` file (or `.sln` if available).

2.  **Build the Project**:
    *   Go to the **Build** menu at the top.
    *   Select **Build Solution** (or press `Ctrl + Shift + B`).
    *   Ensure there are no errors in the "Error List" window.

3.  **Start the Application**:
    *   Click the **Green "Start" Arrow** at the top toolbar (usually labeled with the project name).
    *   Or press **F5**.

### troubleshooting
*   **Database Connection Errors**: If the app fails to start, check `Data/ApplicationDbContext.cs` to ensure the `Data Source` matches your SQL Server name (e.g., `(localdb)\MSSQLLocalDB` or `.\SQLEXPRESS`).
