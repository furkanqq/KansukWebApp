# Kansuk Zimmet Yönetim Sistemi

📄 **Proje Dökümantasyonu**  
👨‍💻 Hazırlayan: Furkan İLHAN  
📅 Tarih: 01.10.2025  

---

## 📌 Bölüm 1: Proje Özeti ve Kapsam

### 1.1 Proje Amacı
Bu proje, **Kansuk** bünyesindeki personel ve demirbaş (zimmet) yönetim süreçlerini dijitalleştirmek, manuel iş yükünü azaltmak ve verimliliği artırmak amacıyla geliştirilmiştir.  
Sistemin temel hedefi: **Hangi personelin hangi demirbaşa sahip olduğunu kolayca takip edebilmek**.

### 1.2 Kapsam

#### 1.2.1 İstenilen Özellikler
- **Zimmet Yönetimi (Assignment CRUD)**  
  - Aktif personellere demirbaş (zimmet) atama  
  - Mevcut zimmetleri düzenleme  
  - Listeleme ve silme işlemleri  

#### 1.2.2 İş Kolaylaştırıcı Kısımlar
- **Personel Yönetimi (User CRUD):** Personel ekleme, düzenleme, listeleme ve silme  
- **Ana Sayfa (Dashboard):**  
  - Toplam aktif personel sayısı  
  - Toplam zimmet kaydı sayısı  

---

## ⚙️ Bölüm 2: Teknik Mimari ve Gereksinimler (Altyapı)

### 2.1 Kullanılan Teknolojiler
- **Backend:** C#, ASP.NET Core 8.0 (MVC mimarisi)  
- **ORM:** Entity Framework Core (Code-First + Migration desteği)  
- **Veritabanı:** Microsoft SQL Server Express  
- **Frontend:** HTML5, CSS3 (Bootstrap 5.x), JavaScript  

### 2.2 Proje Yapısı
- **Controllers:** İş mantığını yönetir (`HomeController`, `UserController`, `AssignmentController`)  
- **Models:** Veri yapıları (`User.cs`, `Assignment.cs`, `AppDbContext.cs`)  
- **ViewModels:** Özel View veri taşıyıcıları (`HomeVM.cs`, `AssignmentVM.cs`)  
- **Views:** Kullanıcı arayüzleri (`Views/Home/Index.cshtml`, `Views/User/Index.cshtml`, `Views/Assignment/Upsert.cshtml`)  
- **Migrations:** Veritabanı değişiklik yönetimi  
- **wwwroot:** Statik dosyalar (CSS, JS, resimler)  

### 2.3 Veritabanı Şeması
- **Users Tablosu:** `Id, Name, Email, Status`  
- **Assignments Tablosu:** `Id, ItemType, Description, CreatedAt, UserId`  
- **İlişkiler:**  
  - Bir **User** birden fazla **Assignment** sahibi olabilir (1-n ilişki)  

### 2.4 Geliştirme Ortamı Ön Koşulları
- Visual Studio 2022  
- .NET SDK 8.0  
- SQL Server Express  
- SQL Server Management Studio (SSMS)  

---

## 🚀 Bölüm 3: Kurulum ve Dağıtım Kılavuzu

### 3.1 Kaynak Kodun Edinilmesi
```bash
git clone https://github.com/furkanqq/KansukWebApp.git
