using EducationSystemBackend.Models;
using EducationSystemBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationSystemBackend.Data
{
    public static class TestDataSeeder
    {
        public static async Task SeedAllAsync(AppDbContext context)
        {
            if (await context.Organizations.AnyAsync(o => o.Name == "Gelecek Bilim Akademisi")) return;

            // 1. Create Organization
            var org = new Organization { Name = "Gelecek Bilim Akademisi", Address = "İstanbul" };
            context.Organizations.Add(org);
            await context.SaveChangesAsync();

            // 2. Create 3 Teachers
            var teachers = new List<Teacher>();
            for (int i = 1; i <= 3; i++)
            {
                var teacher = new Teacher
                {
                    FullName = $"Öğretmen {i}",
                    Email = $"teacher{i}@gelecek.com",
                    Password = "password",
                    City = "İstanbul",
                    OrganizationId = org.Id,
                    Role = UserRole.Teacher
                };
                teachers.Add(teacher);
                context.Teachers.Add(teacher);
            }
            await context.SaveChangesAsync();

            // 3. Create 3 Courses and Assign to Teachers
            var courses = new List<Course>();
            string[] courseNames = { "Matematik", "Fizik", "Kimya" };
            for (int i = 0; i < 3; i++)
            {
                var course = new Course
                {
                    CourseName = courseNames[i],
                    Grade = 10,
                    OrganizationId = org.Id
                };
                courses.Add(course);
                context.Courses.Add(course);
                await context.SaveChangesAsync();

                context.TeacherCourseInfos.Add(new TeacherCourseInfo { TeacherId = teachers[i].Id, CourseId = course.Id });
            }
            await context.SaveChangesAsync();

            // 4. Create 10 Students and Enroll
            var students = new List<Student>();
            for (int i = 1; i <= 10; i++)
            {
                var student = new Student
                {
                    FullName = $"Öğrenci {i}",
                    Email = $"student{i}@gelecek.com",
                    Password = "password",
                    City = "İstanbul",
                    OrganizationId = org.Id,
                    Grade = 10,
                    Section = "A",
                    Role = UserRole.Student
                };
                students.Add(student);
                context.Students.Add(student);
                await context.SaveChangesAsync();

                // Enroll student in all 3 courses
                foreach (var course in courses)
                {
                    context.StudentCourseInfos.Add(new StudentCourseInfo { StudentId = student.Id, CourseId = course.Id });
                }
            }
            await context.SaveChangesAsync();

            // 5. Create 1 Homework for each course
            var homeworks = new List<Homework>();
            for (int i = 0; i < 3; i++)
            {
                var hw = new Homework
                {
                    Title = $"{courseNames[i]} Ödevi #1",
                    Description = "Lütfen tüm soruları yanıtlayın.",
                    CourseId = courses[i].Id,
                    TeacherId = teachers[i].Id,
                    OrganizationId = org.Id,
                    DueDate = DateTime.UtcNow.AddDays(7)
                };
                homeworks.Add(hw);
                context.Homeworks.Add(hw);
            }
            await context.SaveChangesAsync();

            // 6. Each student submits 1 homework (e.g. they all submit to Math/Matematik)
            var mathHw = homeworks[0];
            foreach (var student in students)
            {
                context.HomeworkSubmissions.Add(new HomeworkSubmission
                {
                    HomeworkId = mathHw.Id,
                    StudentId = student.Id,
                    Content = $"Öğrenci {student.FullName} tarafından Matematik ödevi yanıtı.",
                    SubmittedAt = DateTime.UtcNow
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
