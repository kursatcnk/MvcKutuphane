# MVC Kütüphane

ASP.NET MVC 5 ve Entity Framework (Database First) ile yazdığım kütüphane yönetim sistemi. İki ayrı yüzü var: personelin kitap, üye ve ödünç işlemlerini yönettiği yönetim paneli ve üyelerin kendi kitaplarını, duyuruları ve mesajlarını gördüğü üye paneli.

Arayüz AdminLTE 3 üzerine kuruldu.

## Özellikler

- **Kitap, yazar, kategori yönetimi:** ekleme, güncelleme, listeleme; kitapların durumu (rafta / ödünçte) takip ediliyor.
- **Ödünç ve iade:** ödünç verme, iade alma ve ödünç kaydını güncelleme. Ödünçteki kitaplar yeni ödünç listesinde görünmüyor.
- **Üyeler ve personel:** sayfalı üye listesi (PagedList), üye ekleme / güncelleme, her üyenin kitap geçmişi ve personel yönetimi.
- **Üye paneli:** üye kendi bilgilerini güncelliyor, üzerindeki kitapları ve duyuruları görüyor, diğer üyelere mesaj gönderiyor.
- **İstatistikler ve grafikler:** üye, kitap, ödünçteki kitap, kategori ve toplam ceza sayaçları; en çok kitabı olan yazar (stored procedure) ve en çok kitabı olan yayınevi. Ayrıca LINQ ile hazırlanan özet kartları, grafik sayfası ve görsel yüklenen bir galeri.
- **Vitrin:** ziyaretçilere açık, kitapları gösteren ve iletişim formu olan bir sayfa.
- **Yetkilendirme:** Forms Authentication ile üye ve admin girişi, özel `RoleProvider` ile rol bazlı erişim.

## Çalıştırma

Gerekenler: Visual Studio (ASP.NET ve web geliştirme iş yükü), .NET Framework 4.7.2 ve SQL Server.

1. `DbMvcKutuphane.bak` yedeğini SQL Server Management Studio'dan geri yükle.
2. `MvcKutuphane/Web.config` içindeki `DbMvcKutuphaneEntities` bağlantı dizesinde sunucu adını kendi sunucunla değiştir.
3. `MvcKutuphane.sln` dosyasını açıp IIS Express ile çalıştır.

## Lisans

[MIT](LICENSE). `AdminLTE` klasörü kendi lisansıyla birlikte dağıtılıyor.
