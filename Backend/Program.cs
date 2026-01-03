using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using EducationSystemBackend.Repositories;
using EducationSystemBackend.Services;
using EducationSystemBackend.Data;

var builder = WebApplication.CreateBuilder(args);

/* ============================
 * 1️⃣ CORE SERVICES
 * ============================ */
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

/* ============================
 * 2️⃣ FIREBASE / FIRESTORE
 * ============================ */
var firebaseSection = builder.Configuration.GetSection("Firebase");

var credentialsPath = firebaseSection["CredentialsPath"];
var projectId = firebaseSection["ProjectId"];

if (string.IsNullOrWhiteSpace(credentialsPath))
    throw new Exception("Firebase CredentialsPath is missing");

if (string.IsNullOrWhiteSpace(projectId))
    throw new Exception("Firebase ProjectId is missing");

// 🔐 Credential YÜKLE
var credential = GoogleCredential.FromFile(credentialsPath);

// 🔥 Firebase Admin
if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = credential
    });
}

// 🔥 Firestore (⚠️ ADC HATASI BURADA ÇÖZÜLÜYOR)
var firestoreDb = new FirestoreDbBuilder
{
    ProjectId = projectId,
    Credential = credential
}.Build();

builder.Services.AddSingleton(firestoreDb);

/* ============================
 * 3️⃣ DEPENDENCY INJECTION
 * ============================ */

// Repositories
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IHomeworkRepository, HomeworkRepository>();

// Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IHomeworkService, HomeworkService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<OrganizationService>();
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

/* ============================
 * 4️⃣ MIDDLEWARE
 * ============================ */
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.MapControllers();

/* ============================
 * 5️⃣ HEALTH ENDPOINTS
 * ============================ */
app.MapGet("/", () => "OrangeLesson API is running (Firestore)");
app.MapGet("/health", () => new { status = "ok", database = "Firestore" });

/* ============================
 * 6️⃣ DATABASE INIT (LOG’LU)
 * ============================ */
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("🔥 DB INIT STARTED 🔥");
Console.ResetColor();


Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("🚀 API STARTED SUCCESSFULLY");
Console.ResetColor();

app.Run();
