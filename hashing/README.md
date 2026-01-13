# Student Management System - Windows Forms (Data Structures Final Exam)

## 🎯 Project Overview

This is a **C# Windows Forms** desktop application demonstrating the power of **Custom Hash Tables** for managing student data with **O(1) time complexity** for lookups, insertions, and deletions.

## 🔑 Key Features

### 1. Custom Hash Table Implementation

- **Location**: `DataStructures/MyHashTable.cs`
- **Collision Handling**: Chaining with Linked Lists
- **Custom Hash Function**: Uses modulo operation for uniform distribution
- **Time Complexity**: O(1) average case for all operations

### 2. Windows Forms GUI

- **Login Form**: Demonstrates O(1) authentication using hash table
- **Main Dashboard**: Navigation and hash table statistics
- **Student Management Form**: Full CRUD operations with O(1) performance

### 3. Authentication System

- **Location**: `Services/AuthenticationService.cs`
- O(1) login lookup using username as hash key
- SHA-256 password hashing

### 4. Student Management

- **Location**: `Services/StudentManager.cs`
- O(1) student lookup by StudentID
- O(1) insertion, update, and deletion

## 📊 Time Complexity Comparison

| Operation    | Hash Table | List/Array |
| ------------ | ---------- | ---------- |
| Search by ID | **O(1)**   | O(n)       |
| Insert       | **O(1)**   | O(1)       |
| Update       | **O(1)**   | O(n)       |
| Delete       | **O(1)**   | O(n)       |

**Example**: Finding 1 student among 10,000:

- **Hash Table**: ~2 comparisons (instant!)
- **List**: Up to 10,000 comparisons (slow!)

## 🚀 Quick Start

### 1. Restore & Build

```powershell
cd c:\Users\Johnn\Documents\sysHashing
dotnet restore
dotnet build
```

### 2. Run

```powershell
dotnet run
```

### 3. Login

- **Username**: admin
- **Password**: admin123

## 📖 How to Use

### Login Screen

- Shows "O(1) Lookup Time" indicator
- Click "Login (O(1))" for instant authentication
- Can register new admin accounts

### Main Dashboard

- "Manage Students" - Open student management
- "View Hash Table Statistics" - See performance metrics

### Student Management (⭐ KEY FOR EXAM)

1. **Add**: Enter details → "Add Student (O(1))"
2. **Find**: Enter ID → "Find by ID (O(1))" ← **MAIN DEMO!**
3. **Update**: Modify → "Update (O(1))"
4. **Delete**: Select → "Delete (O(1))"
5. **Performance**: Click "Show Performance" for comparison

### Pre-loaded Students:

- ID 1: Sok Pisey
- ID 2: Chan Dara
- ID 3: Meng Sreynit
- ID 4: Vann Sokha
- ID 5: Keo Malina

## 🎓 For Your Exam

### Demo Steps:

1. **Start** → Show login with O(1) indicator
2. **Login** → Demonstrate instant authentication
3. **Manage Students** → Open the form
4. **Find Student** → Enter ID 1, click Find → **Instant O(1) lookup!**
5. **Show Performance** → Display comparison chart
6. **Explain** → O(1) vs O(n) difference

### Key Points:

- Hash function: `index = key % capacity`
- Collision handling: Chaining with linked lists
- O(1) = Constant time (always same speed)
- O(n) = Linear time (slower with more data)
- Hash table is ~10,000x faster for 10,000 students!

### Real-World Example:

```
Finding Student ID 5000 among 10,000:

List: Check all 10,000 students = 10,000 operations
Hash Table: Calculate hash + check bucket = 2 operations

Result: 5,000x faster!
```

## 📁 Project Structure

```
sysHashing/
├── DataStructures/
│   └── MyHashTable.cs          ⭐ Hash table implementation
├── Forms/
│   ├── LoginForm.cs            Login with O(1) auth
│   ├── RegisterForm.cs         Registration
│   ├── MainDashboardForm.cs    Dashboard
│   └── StudentManagementForm.cs ⭐ Main demo form
├── Services/
│   ├── AuthenticationService.cs ⭐ O(1) authentication
│   └── StudentManager.cs        ⭐ O(1) student ops
├── Models/                      Data models
├── Program.cs                   Entry point
├── EXAM_PREPARATION.md          ⭐ Detailed exam guide
└── VISUAL_DIAGRAMS.md           ⭐ Visual explanations

⭐ = Most important for exam
```

## 💡 Hash Table Concepts

### How It Works:

```csharp
// Convert key to array index
int hash = key.GetHashCode();
int index = Math.Abs(hash) % capacity;

// Direct array access = O(1)!
return buckets[index];
```

### Collision Handling:

```
buckets[5] → [Student 1005] → [Student 1021] → null
             (Linked list at same index)
```

### Load Factor:

- Ratio: size / capacity
- Threshold: 0.75 (resize when 75% full)
- Maintains O(1) performance

## 🎯 Important for Exam

### Why O(1)?

1. Hash calculation: Fixed operations
2. Array access: Direct memory jump
3. No loops needed!

### When to Use Hash Tables?

✅ Frequent lookups by unique key (ID, username)
✅ Need fast insert/update/delete
✅ Have unique keys available

❌ Need sorted data
❌ Need range queries (find all IDs 100-200)
❌ Extremely limited memory

### Trade-offs:

- **Pro**: O(1) operations
- **Con**: Uses more memory
- **Con**: Not good for ordering/sorting

## 🔍 Test Credentials

| Username | Password |
| -------- | -------- |
| admin    | admin123 |
| john     | admin    |
| jane     | 1234     |

## 📝 Sample Answers for Exam Questions

**Q: Why use hash table instead of list?**
A: Hash table provides O(1) lookup vs O(n) for list. With 10,000 students, hash table checks ~2 entries while list checks up to 10,000. Performance gain is proportional to data size.

**Q: What if two keys have same hash?**
A: Collision! We use chaining - store both in linked list at same index. Still O(1) average because chains are short with good hash function.

**Q: What makes a good hash function?**
A: Uniform distribution (spreads keys evenly), fast computation, and minimizes collisions.

---

**Good luck with your exam! 🎓**
