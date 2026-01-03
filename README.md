# OrangeLesson Education Management System

A comprehensive educational platform designed to manage multiple organizations, teachers, students, courses, and homework assignments. This project follows a modern monorepo structure with a .NET 9 Backend and a React + Vite Frontend.

## 🌍 Live Deployment
- **Frontend (Netlify):** [https://orange-lesson.netlify.app](https://orange-lesson.netlify.app)
- **Backend (Azure):** [https://orange-lesson-cehvaydjbab4e3ej.eastus-01.azurewebsites.net](https://orange-lesson-cehvaydjbab4e3ej.eastus-01.azurewebsites.net)
- **API Documentation (Swagger):** [https://orange-lesson-cehvaydjbab4e3ej.eastus-01.azurewebsites.net/swagger](https://orange-lesson-cehvaydjbab4e3ej.eastus-01.azurewebsites.net/swagger)

## 🛠 Technology Stack
- **Backend:** .NET 9 (ASP.NET Core Web API)
- **Database:** Google Cloud Firestore (NoSQL)
- **Frontend:** React + Vite, Framer Motion, Lucide React
- **Architecture:** Monorepo, Repository Pattern, Service Layer, Multi-Tenancy (Multi-Org support)

## 📂 Project Structure
- `/Backend`: .NET 9 Web API (Azure Deployment Target)
- `/Frontend`: React + Vite (Netlify Deployment Target)

## 🚀 Local Setup

### 1. Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js (v18 or later)](https://nodejs.org/)
- Google Firebase Project Service Account Key (`orangelessondb-firebase-adminsdk-fbsvc-6c0305a00f.json`)

### 2. Backend Setup
The backend requires a Firebase Service Account key to connect to Firestore. 
1. Place your `orangelessondb-firebase-adminsdk-fbsvc-6c0305a00f.json` in the `Backend/` directory.
2. Ensure `appsettings.json` points to it:
   ```json
   "Firebase": {
     "CredentialsPath": "orangelessondb-firebase-adminsdk-fbsvc-6c0305a00f.json",
     "ProjectId": "orangelessondb"
   }
   ```
3. Run the backend:
```bash
cd Backend
dotnet build
dotnet run
```
*The API will be available at `http://localhost:5000`*

### 3. Frontend Setup
```bash
cd Frontend
npm install
npm run dev
```
*The Web App will be available at `http://localhost:5173`*

## 🔑 Test Accounts (Seeded Data)
When you run the backend locally, the database is automatically seeded with these accounts:

| Role | Name | Email | Password | Org |
|------|------|-------|----------|-----|
| **Admin** | Eylül Özatman | eylul@ozatman.com | admin | OrangeLesson |
| **Teacher** | Ali Yılmaz | ali@blueschool.com | 123 | BlueSchool |
| **Student** | Can Yıldız | can@blueschool.com | 123 | BlueSchool |

## 📖 API Endpoints

### Organizations (`/api/organizations`)
- `POST /api/organizations`: Create a new organization.
- `GET /api/organizations`: List all visible organizations.

### Students (`/api/students`)
- `POST /api/students/register`: Register a new student. Request body: `StudentRegisterRequest`.
- `POST /api/students/login`: Login a student. Request body: `StudentLoginRequest`.
- `POST /api/students/submit-homework`: Submit homework (metadata).

### Teachers (`/api/teachers`)
- `POST /api/teachers/register`: Register a teacher. Request body: `TeacherRegisterRequest`.
- `POST /api/teachers/login`: Login a teacher. Request body: `TeacherLoginRequest`.
- `POST /api/teachers/homeworks`: Create a homework.
- `GET /api/teachers/{teacherId}/homeworks`: Get homeworks created by a teacher.

### Courses (`/api/courses`)
- `GET /api/courses/by-organization/{organizationId}`: List courses for an organization.
- `GET /api/courses/by-student/{studentId}`: List courses a student is enrolled in.
- `GET /api/courses/by-teacher/{teacherId}`: List courses a teacher teaches.
- `GET /api/courses/id-by-name`: Get a course id by name.
- `POST /api/courses`: Create a new course.
- `POST /api/courses/enroll`: Enroll a student in a course.

### Homework (`/api/homeworks`)
- `POST /api/homeworks`: Create a new homework assignment.
- `GET /api/homeworks/teacher/{teacherId}`: Get homeworks assigned by a teacher.
- `GET /api/homeworks/student/{studentId}`: Get homeworks for a student.
- `POST /api/homeworks/submit`: Submit a homework (File + Data).
- `GET /api/homeworks/{homeworkId}/submissions`: List submissions.

## 🔒 Database Notes
The seed logic runs on startup if the database is detected as empty or if explicitly requested. It clears existing collections (`Organizations`, `Teachers`, `Students`, `Courses`) and repopulates them to ensure a fresh state for development.

---
Developed with ❤️ by Eylül Özatman
