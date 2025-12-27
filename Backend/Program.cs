using EducationSystemBackend.Data;
using EducationSystemBackend.Repositories;
using EducationSystemBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

// CORS Configuration for Frontend access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Database Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IHomeworkService, HomeworkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Seed database on startup
using (var scope = app.Services.CreateScope())
{
    try 
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await DbInitializer.SeedAsync(context);
        await TestDataSeeder.SeedAllAsync(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Seeding Error: {ex.Message}");
    }
}

app.Run();
