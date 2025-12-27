using System.Net.Http.Json;
using System.Text.Json;

var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };

try {
    Console.WriteLine("--- 1. Kurum Oluşturuluyor ---");
    var orgResponse = await httpClient.PostAsJsonAsync("api/organizations", new { Name = "Gelecek Bilim Akademisi", Address = "İstanbul" });
    var org = await orgResponse.Content.ReadFromJsonAsync<JsonElement>();
    var orgId = org.GetProperty("id").GetGuid();
    Console.WriteLine($"Kurum Oluşturuldu: {orgId}");

    Console.WriteLine("\n--- 2. 3 Öğretmen Oluşturuluyor ---");
    var teacherIds = new List<Guid>();
    var teachers = new[] { "Ahmet Yılmaz", "Zeynep Kaya", "Can Demir" };
    foreach (var name in teachers) {
        var tRes = await httpClient.PostAsJsonAsync("api/teachers/assign-course", new { }); // Simplified for seed
        // Since we don't have a teacher creation API yet (handled by admin), I'll use the DB directly via a seed script 
        // OR assume they exist for this test. I will create a temporary seed controller or use the services.
    }
} catch (Exception ex) {
    Console.WriteLine($"Hata: {ex.Message}");
}
