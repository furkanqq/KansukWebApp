# 📄 Kansuk Zimmet Yönetim Sistemi Proje Dökümantasyonu

**Hazırlayan:** Furkan İLHAN  
**Tarih:** 01.10.2025

---

## 1. Proje Özeti ve Kapsam

### 1.1 Proje Amacı

Bu proje, **Kansuk** bünyesindeki personel ve demirbaş (**zimmet**) yönetim süreçlerini **dijitalleştirmek**, manuel iş yükünü azaltmak ve verimliliği artırmak amacıyla geliştirilmiştir. Sistemin temel hedefi, hangi personelin hangi demirbaşa sahip olduğunu kolayca ve hatasız bir şekilde **takip edebilmektir**.

### 1.2 Kapsam

#### 1.2.1 İstenilen Temel Fonksiyonlar
* **Zimmet Yönetimi (Assignment CRUD):** Aktif personellere demirbaş (zimmet) atama, mevcut zimmetleri düzenleme, listeleme ve silme işlemleri.

#### 1.2.2 İş Kolaylaştırıcı Fonksiyonlar
* **Personel Yönetimi (User CRUD):** Yeni personel ekleme, mevcut personeli düzenleme, listeleme ve silme işlemleri.
* **Ana Sayfa (Dashboard):** Sistemdeki toplam **aktif personel sayısı** ve toplam **zimmet kaydı sayısı**nı gösteren özet görünüm.

---

## 2. Teknik Mimari ve Gereksinimler (Altyapı)

### 2.1 Teknolojiler

| Kategori | Teknoloji | Açıklama |
| :--- | :--- | :--- |
| **Sunucu Dili & Çatı** | C#, **ASP.NET Core 8.0 MVC** | Uygulamanın katmanlı ve ölçeklenebilir yapısını sağlayan temel altyapı. |
| **Veri Erişim Katmanı (ORM)** | **Entity Framework Core** | Veritabanı işlemlerini C# kodları üzerinden yönetme (**Code-First**) ve `migration` takibi. |
| **Veritabanı Motoru** | **Microsoft SQL Server Express** | Yerel geliştirme ortamlarında kolay kurulum ve yönetim sağlayan veritabanı. |
| **Ön Yüz Teknolojileri** | HTML5, CSS3 (**Bootstrap 5.x**), JavaScript | Kullanıcı arayüzü ve stil oluşturma. |

### 2.2 Proje Yapısı

Proje, standart bir ASP.NET Core MVC yapısını takip eder:

* **`Controllers`**: Kullanıcı isteklerini alır, iş mantığını yürütür ve `View`'a veri sağlar. (Örn: `HomeController`, `UserController`, `AssignmentController`)
* **`Models`**: Uygulama verilerini temsil eder ve veritabanı şemasıyla eşleşir. (Örn: `User.cs`, `Assignment.cs`, `AppDbContext.cs`)
* **`ViewModels`**: Belirli `View`'lar için özelleştirilmiş veri yapılarıdır. (Örn: `HomeVM.cs`, `AssignmentVM.cs`)
* **`Views`**: Kullanıcı arayüzünü (HTML) oluşturur.
* **`Migrations`**: Veritabanı şemasındaki değişiklikleri izler.
* **`wwwroot`**: Statik dosyaları (CSS, JavaScript, resimler) barındırır.

### 2.3 Veritabanı Şeması (Entity Framework Code-First)

| Tablo Adı | İçerik | İlişki |
| :--- | :--- | :--- |
| **`Users`** | Personel bilgileri (`Id`, `Name`, `Email`, `Status`) | - |
| **`Assignments`** | Zimmet bilgileri (`Id`, `ItemType`, `Description`, `CreatedAt`, `UserId`) | `Assignments` tablosu, `UserId` üzerinden `Users` tablosuna **Bire Çok** ilişki ile bağlıdır. (Bir personel birden fazla zimmete sahip olabilir.) |

### 2.4 Geliştirme Ortamı Ön Koşulları

Projenin yerel geliştirme ortamında çalıştırılabilmesi için aşağıdaki yazılımların yüklü olması gerekmektedir:

* **Visual Studio 2022**
* **.NET SDK 8.0**
* **SQL Server Express**
* SQL Server Management Studio (SSMS) (Opsiyonel, ancak önerilir)

---

## 3. Kurulum ve Dağıtım Kılavuzu

### 3.1 Kaynak Kodun Edinilmesi

1.  Proje kaynak kodunu Git deposundan klonlayın:
    ```bash
    git clone [https://github.com/furkanqq/KansukWebApp.git](https://github.com/furkanqq/KansukWebApp.git)
    ```

### 3.2 Veritabanı Kurulumu

1.  **`appsettings.json`** dosyasını düzenleyin: Projenin ana dizinindeki bu dosyayı açın ve `ConnectionStrings` bölümündeki `DefaultConnection` değerini kendi **SQL Server kurulumunuza göre** güncelleyin.
    * `Server` adresinin (`YOUR_SERVER_NAME\SQLEXPRESS` yerine kendi sunucu adınız) doğru olduğundan emin olun.
    * `TrustServerCertificate=True` parametresi yerel bağlantılar için kritik öneme sahiptir.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME\\SQLEXPRESS;Database=KansukAssignDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    }
    ```

2.  **Veritabanını Oluşturun/Güncelleyin (Migration):** Visual Studio'yu açın ve **Package Manager Console** penceresinde (`Tools` > `NuGet Package Manager` > `Package Manager Console`) aşağıdaki komutu çalıştırın:
    ```powershell
    Update-Database
    ```
    * Bu komut, **KansukAssignDB** adlı veritabanını oluşturacak ve tabloları şemaya göre ekleyecektir.

### 3.3 Uygulamanın Çalıştırılması

1.  Projeyi Visual Studio'da açın.
2.  **F5** tuşuna basarak projeyi **Debug** modunda başlatın veya **Ctrl + F5** ile Debug yapmadan çalıştırın.
3.  Uygulama varsayılan tarayıcınızda açılacaktır.

---

## 4. Uygulama Kullanım ve Yönetim Kılavuzu

### 4.1 Ana Sayfa (Dashboard)

Uygulama açıldığında ilk görünen özet sayfadır.
* **Kayıtlı Aktif Personel Sayısı:** Şu anda sistemde aktif olan personel sayısını gösterir.
* **Toplam Zimmet Kaydı:** Sistemde atanmış tüm demirbaş zimmetlerinin toplam sayısını gösterir.

### 4.2 Personel Yönetimi (`/User`)

Personel (Kullanıcı) ekleme, güncelleme, silme ve listeleme işlemlerinin yapıldığı bölümdür.

1.  **Personel Listesi:** `/User` adresinden mevcut personelleri görüntüleyin.
2.  **Yeni Personel Ekleme:** "Yeni Personel Ekle" butonu ile adı, e-posta adresi ve durumu (**Aktif/Pasif**) girilerek kayıt oluşturulur.
3.  **Personel Bilgilerini Güncelleme:** Liste görünümündeki "Güncelle" butonu ile mevcut bilgileri düzenleyip kaydedin.
4.  **Personel Silme:** "Sil" butonu ile onay isteminin ardından personel kaydı silinir.

### 4.3 Zimmet Yönetimi (`/Assignment`)

Demirbaşlara zimmet atama, güncelleme, silme ve listeleme işlemlerinin yapıldığı temel bölümdür.

1.  **Zimmet Listesi:** `/Assignment` adresinden mevcut zimmetleri görüntüleyin.
2.  **Yeni Zimmet Girişi:** "Yeni Zimmet Girişi Yap" butonu ile;
    * Sadece **aktif personeller** arasından zimmet ataması yapılacak personel seçilir.
    * Zimmet Türü (örn: Telefon, Tablet, Hat) ve Açıklaması girilir.
    * `CreatedAt` alanı, kaydetme anındaki sistem tarihini otomatik olarak alır.
3.  **Zimmet Bilgilerini Güncelleme:** Liste görünümündeki "Güncelle" butonu ile kayıt bilgileri düzenlenir. (`CreatedAt` tarihi korunur.)
4.  **Zimmet Silme:** "Zimmeti Sil" butonu ile onay isteminin ardından zimmet kaydı silinir.
