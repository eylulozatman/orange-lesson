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
*The API will be available at `http://localhost:5000`*

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
- `POST /api/students/register`: Register a new student. Request body: `StudentRegisterRequest` ({ organizationId, fullName, email, password, city, grade }).
- `POST /api/students/login`: Login a student. Request body: `StudentLoginRequest` ({ email, password }). Returns `AuthResponse` ({ userId, role }).
- `POST /api/students/submit-homework`: Submit homework. Request body: `SubmitHomeworkRequest` ({ homeworkId, courseId, studentId, content }).

### Teachers (`/api/teachers`)
- `POST /api/teachers/register`: Register a teacher. Request body: `TeacherRegisterRequest` ({ organizationId, fullName, email, password, city, courseId }). Returns `AuthResponse`.
- `POST /api/teachers/login`: Login a teacher. Request body: `TeacherLoginRequest` ({ email, password }). Returns `AuthResponse`.
- `POST /api/teachers/homeworks`: Create a homework (teacher creates a `Homework` object in body).
- `GET /api/teachers/{teacherId}/homeworks`: Get homeworks created by a teacher.

### Courses (`/api/courses`)
- `GET /api/courses/by-organization/{organizationId}`: List courses for an organization.
- `GET /api/courses/by-student/{studentId}`: List courses a student is enrolled in.
- `GET /api/courses/by-teacher/{teacherId}`: List courses a teacher teaches.
- `GET /api/courses/id-by-name?organizationId=...&courseName=...`: Get a course id by name for a given organization.
- `POST /api/courses`: Create a new course. Request body: `CreateCourseRequest`.
- `POST /api/courses/enroll`: Enroll a student in a course. Request body: `EnrollStudentRequest` ({ studentId, courseId }).

### Homework (`/api/homeworks`)
- `POST /api/homeworks`: Create a new homework assignment (Teacher). Request body: `CreateHomeworkRequest` ({ courseId, teacherId, title, description, dueDate }).
- `GET /api/homeworks/teacher/{teacherId}`: Get homeworks assigned by a teacher.
- `GET /api/homeworks/student/{studentId}`: Get homeworks for a student's enrolled courses.
- `POST /api/homeworks/submit`: Submit a homework (Student). Accepts `multipart/form-data` (fields from `SubmitHomeworkRequest` and optional file upload). Returns created `HomeworkSubmission`.
- `GET /api/homeworks/{homeworkId}/submissions`: List all student submissions for a specific homework.

## 🔒 Database Configuration
The backend is configured to use **Google Firestore** (Firebase Admin SDK). Provide a Firebase credentials JSON and set `Firebase:CredentialsPath` and `Firebase:ProjectId` in `appsettings.json` or environment variables. The app also seeds collections when `FirestoreSeeder` runs.

---
Developed with ❤️ by Eylül Özatman
