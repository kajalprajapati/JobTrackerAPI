# 📌 Job Tracker API

A clean, professional-grade .NET Core Web API to help track job applications — with search, status filtering, email notifications, and SQL Server integration.

---

## 📁 Features

- ✅ Add, edit, and delete job applications
- 🔍 Search by **Company**, **Role**, or **Status**
- 📬 Send email notification when job is applied
- 📦 EF Core integration with SQL Server
- 🧼 Clean MVC structure (Controller + Service + Model)
- ⚙️ Swagger UI for testing and documentation
-  Extensible design for WhatsApp or SMS integration
- Secure secret handling using User Secrets

---

## 🛠 Technologies Used

- ASP.NET Core 7 Web API
- Entity Framework Core
- SQL Server
- C#
- Swagger / Swashbuckle
- Visual Studio 2022
- Git + GitHub

---

## 🧪 API Endpoints (Examples)

| Method | Endpoint                | Description                   |
|--------|-------------------------|-------------------------------|
| GET    | `/jobs`                 | Get all job applications      |
| POST   | `/jobs`                 | Add a new job                 |
| PUT    | `/jobs/{id}`            | Update job by ID              |
| DELETE | `/jobs/{id}`            | Delete job by ID              |
| GET    | `/jobs/search`          | Filter by status, role, etc.  |

Test all endpoints in **Swagger UI**.

---

## 📬 Email Notification (Bonus Feature)

When a job is applied, an email is sent using a configured SMTP email account. (Use MailKit / SMTP in background service).

---

## 🧰 How to Run Locally

1. Clone this repo
   ```bash
   git clone https://github.com/kajalprajapati/JobTrackerAPI.git
   cd JobTrackerAPI
