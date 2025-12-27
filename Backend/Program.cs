using EducationSystemBackend.Data;
using EducationSystemBackend.Repositories;
using EducationSystemBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

// CORS Configuration for Frontend access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
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
// In Azure, Swagger is useful for testing even in production
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrangeLesson API V1");
    c.RoutePrefix = "swagger"; // Swagger at /swagger
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Ensure wwwroot exists for static files if needed
if (!Directory.Exists(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")))
{
    Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "wwwroot"));
}

app.UseStaticFiles(); // Default wwwroot

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

// Default root route to check if API is alive (Azure suggestion)
app.MapGet("/", () => "OrangeLesson Education System Backend API is running... Check /swagger for documentation.");

// Seed database on startup
using (var scope = app.Services.CreateScope())
{
    try 
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Ensure migrations are applied in production
        if (!app.Environment.IsDevelopment())
        {
            context.Database.Migrate();
        }
        await DbInitializer.SeedAsync(context);
        await TestDataSeeder.SeedAllAsync(context);
    }
    catch (Exception ex)
    {
        // Log seeding error but don't crash the app
        Console.WriteLine($"Seeding Error: {ex.Message}");
    }
}

app.Run();
