# Education System Backend

Bu proje, öğrencilerin, öğretmenlerin, kursların ve ödevlerin yönetildiği bir eğitim sistemi backend yapısıdır.

## Teknoloji Yığını
- **Framework:** .NET 9 (ASP.NET Core Web API)
- **Database:** Supabase (PostgreSQL - Bulut Tabanlı)
- **Architecture:** Repository Pattern, Service Layer & Multi-Tenancy (Multi-Org)

## Veritabanı Bağlantısı
Proje artık bulut tabanlı **Supabase (PostgreSQL)** veritabanını kullanmaktadır.

### Organizasyon Yapısı (Multi-Tenancy)
Sistem birden fazla kurumu (dershane/okul) destekleyecek şekilde tasarlanmıştır. Tüm veriler (Öğrenci, Öğretmen, Kurs, Ödev) bir `OrganizationId` ile ilişkilendirilmiştir.

### API Uç Noktaları (Genişletilmiş)

#### Organizasyon İşlemleri (`/api/organizations`)
- **POST `/api/organizations`**: Yeni bir kurum (dershane) kaydı oluşturur.
- **GET `/api/organizations`**: Mevcut tüm kurumları listeler.

### Öğrenci İşlemleri (`/api/students`)
- **POST `/api/students/register`**: Yeni bir öğrenci kaydı oluşturur.
- **GET `/api/students`**: Tüm öğrencileri listeler.
- **POST `/api/students/enroll`**: Öğrenciyi bir kursa kaydeder.

### Kurs İşlemleri (`/api/courses`)
- **GET `/api/courses`**: Tüm kursları listeler.
- **POST `/api/courses`**: Yeni bir kurs oluşturur.

### Öğretmen İşlemleri (`/api/teachers`)
- **GET `/api/teachers/me?email=...`**: Öğretmen profil ve ders bilgilerini getirir.
- **POST `/api/teachers/assign-course`**: Öğretmen bir kursa atanır (Her öğretmen sadece 1 kurs verebilir).

### Ödev İşlemleri (`/api/homeworks`)
- **POST `/api/homeworks`**: (Öğretmen) Yeni ödev oluşturur.
- **GET `/api/homeworks/teacher/{teacherId}`**: Öğretmenin verdiği ödevleri listeler.
- **GET `/api/homeworks/student/{studentId}`**: Öğrencinin kayıtlı olduğu kurslardaki ödevleri listeler.
- **POST `/api/homeworks/submit`**: (Öğrenci) Ödev yanıtı gönderir (Metin ve dosya desteği).
- **GET `/api/homeworks/{homeworkId}/submissions`**: (Öğretmen) Bir ödeve gelen tüm öğrenci yanıtlarını listeler.
- **DELETE `/api/homeworks/submission/{submissionId}`**: (Öğrenci) Kendi ödev yanıtını siler.

## Proje Yapısı
- **Models**: Veritabanı tablolarımızı temsil eden entity sınıfları.
- **Data**: Entity Framework DbContext ve Migration dosyaları.
- **Repositories**: Veritabanı erişim mantığını soyutlayan yapı (IRepository).
- **Services**: İş mantığının (business logic) bulunduğu katman.
- **Controllers**: API isteklerini karşılayan ve yanıt veren katman.
- **Requests/Responses**: DTO (Data Transfer Object) sınıfları.
