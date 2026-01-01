using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using EducationSystemBackend.Repositories;
using EducationSystemBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Core services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
});

// 2️⃣ Firebase / Firestore
var firebaseSection = builder.Configuration.GetSection("Firebase");

var credentialsPath = firebaseSection["CredentialsPath"];
var projectId = firebaseSection["ProjectId"];

if (string.IsNullOrWhiteSpace(credentialsPath))
    throw new Exception("Firebase CredentialsPath is missing");

if (string.IsNullOrWhiteSpace(projectId))
    throw new Exception("Firebase ProjectId is missing");

if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(credentialsPath)
    });
}

var firestoreDb = FirestoreDb.Create(projectId);
builder.Services.AddSingleton(firestoreDb);

// 3️⃣ Dependency Injection (classic structure)

// Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IHomeworkRepository, HomeworkRepository>();


// Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IHomeworkService, HomeworkAppService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<OrganizationService>();
builder.Services.AddScoped<CourseService>();

var app = builder.Build();

// 4️⃣ Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");

app.MapControllers();

// 5️⃣ Health & test endpoints
app.MapGet("/", () => "OrangeLesson API is running (Firestore)");
app.MapGet("/health", () => new { status = "ok", database = "Firestore" });

app.Run();
