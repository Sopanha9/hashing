# Project Structure & Architecture

This document explains the organization of the **Student Management System** codebase. Each folder serves a specific architectural purpose to separate concerns (Data, Logic, UI).

## 📂 Folder Overview

### 1. `Models` 🏗️
**Role:** The Blueprints / Enaities.
This folder contains the **Class Definitions** that represent the data objects in your system. These map directly to database tables.
*   **What's inside?**
    *   `Student.cs`: Represents a student (ID, Name, Course, etc.).
    *   `Teacher.cs`, `Course.cs`, `SystemAdmin.cs`: Other entities representing people or objects in the school system.
*   **Analogy:** These are like the blank forms you fill out on paper. They define *what* data looks like, but don't hold the data themselves permanently.

### 2. `Data` 💾
**Role:** The Database Connector.
This folder contains the code responsible for communicating with the SQL Server database.
*   **What's inside?**
    *   `ApplicationDbContext.cs`: This is the bridge (using Entity Framework Core) that translates your C# `Models` into SQL tables and queries. It handles saving, retrieving, and updating records in the database.

### 3. `DataStructures` 🧩
**Role:** Custom Algorithms.
This folder contains your manual implementations of data structures (as opposed to using generic C# Lists/Dictionaries).
*   **What's inside?**
    *   `MyHashTable.cs`: A custom implementation of a Hash Table. This is used for fast in-memory lookups (e.g., searching for a student by ID in O(1) time) to demonstrate algorithmic efficiency alongside database storage.

### 4. `Services` ⚙️
**Role:** The Business Logic / The Brain.
This folder contains the "worker" classes that do the processing. They don't just store data (Models) or show screens (Forms); they perform actions.
*   **What's inside?**
    *   `AuthenticationService.cs`: Hashing passwords, checking login credentials.
    *   `StudentManager.cs`: Handles logic for adding students, searching (using the generic `MyHashTable`), and validation before talking to the database.

### 5. `Forms` 🖼️
**Role:** The User Interface (UI).
This folder contains the visual windows that the user interacts with.
*   **What's inside?**
    *   `LoginForm.cs`: The entry screen.
    *   `MainDashboardForm.cs`: The central hub after logging in.
    *   `StudentManagementForm.cs`: The screen for adding/editing students.
    *   *Code-Behind files*: Each `.cs` form has limits logic strictly for UI behavior (button clicks, showing alerts), while delegating heavy lifting to the `Services`.

### 6. `Helpers` 🛠️
**Role:** Utilities & Styling.
This folder contains code that is used across multiple places to keep things consistent or make life easier.
*   **What's inside?**
    *   `ModernTheme.cs`: Defines the color palette (Primary colors, Backgrounds) and styles for the UI, ensuring the application has a consistent "Modern" look (e.g., the dark theme logic).

### 📄 Root Files
*   `Program.cs`: The **Encryption Point**. This is where the application starts. It configures the database, sets up themes, and launches the `LoginForm`.
*   `SampleData.sql`: A helper script to set up your SQL database with initial test data.
