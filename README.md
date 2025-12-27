# OrangeLesson Education Management System

A comprehensive educational platform designed to manage multiple organizations, teachers, students, courses, and homework assignments. This project follows a modern monorepo structure with a .NET 9 Backend and a React + Vite Frontend.

## 🌍 Live Deployment
- **Frontend (Netlify):** [https://orange-lesson.netlify.app](https://orange-lesson.netlify.app)
- **Backend (Azure):** [https://orange-lesson-cehvaydjbab4e3ej.eastus-01.azurewebsites.net](https://orange-lesson-cehvaydjbab4e3ej.eastus-01.azurewebsites.net)
- **API Documentation (Swagger):** [https://orange-lesson-cehvaydjbab4e3ej.eastus-01.azurewebsites.net/swagger](https://orange-lesson-cehvaydjbab4e3ej.eastus-01.azurewebsites.net/swagger)

## 🛠 Technology Stack
- **Backend:** .NET 9 (ASP.NET Core Web API)
- **Database:** Supabase (PostgreSQL - Cloud Based)
- **Frontend:** React + Vite, Framer Motion, Lucide React
- **Architecture:** Monorepo, Repository Pattern, Service Layer, Multi-Tenancy (Multi-Org support)

## 📂 Project Structure
- `/Backend`: .NET 9 Web API (Azure Deployment Target)
- `/Frontend`: React + Vite (Netlify Deployment Target)

## 🚀 Local Setup

### 1. Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js (v18 or later)](https://nodejs.org/)

### 2. Backend Setup
```bash
cd Backend
dotnet build
dotnet run
```
*The API will be available at `http://localhost:5011`*

### 3. Frontend Setup
```bash
cd Frontend
npm install
npm run dev
```
*The Web App will be available at `http://localhost:5173`*

## 📖 API Endpoints

### Organizations (`/api/organizations`)
- `POST /api/organizations`: Create a new organization.
- `GET /api/organizations`: List all visible organizations.

### Students (`/api/students`)
- `POST /api/students/register`: Register a new student.
- `GET /api/students`: List all students.
- `POST /api/students/enroll`: Enroll a student in a course.

### Teachers (`/api/teachers`)
- `GET /api/teachers/me?email=...`: Get teacher profile and assigned course.
- `POST /api/teachers/assign-course`: Assign a teacher to a specific course.

### Courses (`/api/courses`)
- `GET /api/courses`: List all courses.
- `POST /api/courses`: Create a new course.

### Homework (`/api/homeworks`)
- `POST /api/homeworks`: Create a new homework assignment (Teacher).
- `GET /api/homeworks/teacher/{teacherId}`: Get homeworks assigned by a teacher.
- `GET /api/homeworks/student/{studentId}`: Get homeworks for a student's enrolled courses.
- `POST /api/homeworks/submit`: Submit a homework (Student).
- `GET /api/homeworks/{homeworkId}/submissions`: List all student submissions for a specific homework.

## 🔒 Database Configuration
The system uses **Supabase (PostgreSQL)**. For local development, the connection string is managed via `appsettings.json` or .NET User Secrets.

---
Developed with ❤️ by Eylül Özatman
