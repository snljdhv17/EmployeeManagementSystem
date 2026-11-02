# Employee Management System

A full-stack Employee Management System built using **Angular, ASP.NET Core Web API, Entity Framework Core, and Microsoft SQL Server**.

This application provides a centralized interface for managing employee information and related master data.

## 🚀 Technologies Used

### Frontend
- Angular (Standalone Components)
- TypeScript
- HTML5
- CSS3

### Backend
- ASP.NET Core Web API
- C#
- Entity Framework Core

### Database
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)

## ✨ Features

- Employee management
- Department management
- Designation management
- Employee listing and forms
- Dashboard
- Login page
- RESTful API integration
- SQL Server database integration

*Note: Keep only features that are implemented and working in your application.*

## 📁 Project Structure

```text
EmployeeManageSystem/
│
├── Database/
│   └── DatabaseSchemaAndData.sql
│
├── employee_manage_app/
│   ├── src/
│   ├── angular.json
│   ├── package.json
│   └── package-lock.json
│
├── employee.api/
│   ├── employee.api/
│   │   ├── Controllers/
│   │   ├── Models/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   └── employee.api.sln
││
├── screenshots/
│   ├── dashboard.png
│   ├── employee-list.png
│   ├── employee-form.png
│   └── swagger-api.png
├── .gitignore
└── README.md
```

## ⚙️ Prerequisites

Make sure you have installed:

- Node.js and npm
- Angular CLI (if required)
- .NET SDK compatible with the backend project
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Visual Studio Code or Visual Studio

## 🗄️ Database Setup

1. Open SQL Server Management Studio.
2. Connect to your SQL Server instance.
3. Open the `Database/DatabaseSchemaAndData.sql` file.
4. Review the script and execute it to create the database and its objects.
5. Verify that the `employeeManageDB` database is available.

## 🔧 Backend Setup

1. Open the project folder in VS Code.
2. Open a terminal in the main project directory.
3. Configure the database connection string in:

   `employee.api/employee.api/appsettings.json`

4. Update the SQL Server instance name to match your local installation.

Example:

```json
"ConnectionStrings": {
  "EmployeeCon": "Server=localhost\\SQLEXPRESS;Database=employeeManageDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

5. Restore the backend dependencies:

```bash
dotnet restore employee.api/employee.api/employee.api.csproj
```

6. Run the backend:

```bash
dotnet run --project employee.api/employee.api/employee.api.csproj
```

The API will run using the configured launch profile and application settings.

## 💻 Frontend Setup

1. Open a new terminal.
2. Navigate to the Angular project:

```bash
cd employee_manage_app
```

3. Install dependencies:

```bash
npm install
```

4. Start the Angular development server:

```bash
npx ng serve
```

5. Open the local URL displayed in your terminal, usually:

```text
http://localhost:4200
```

## 🔗 Application Integration

The Angular frontend communicates with the ASP.NET Core Web API.

Make sure the API base URL and CORS configuration match your local backend setup.

## 📸 Screenshots

### Dashboard
![Dashboard](screenshots/dashboard.png)

### Employee List
![Employee List](screenshots/employee-list.png)

### Employee Form
![Employee Form](screenshots/employee-form.png)

### Department Management
![Department Management](screenshots/department.png)

### Designation Management
![Designation Management](screenshots/designation.png)

### Backend API - Swagger

![Swagger API](screenshots/swagger-api-dept-desig.png)
### Backend API - Swagger

![Swagger API](screenshots/swagger-api-employeeMaster.png)

## 👨‍💻 Author

**GitHub:** [snljdhv17](https://github.com/snljdhv17)

## 📌 Repository

[Employee Management System – GitHub](https://github.com/snljdhv17/EmployeeManagementSystem)

---

⭐ If you find this project interesting, feel free to explore the repository!