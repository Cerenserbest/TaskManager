Proje Açıklaması
Ekiplerin günlük görevlerini dağınık notlar veya sözlü iletişim yerine tek bir yerden takip edebilmesi için geliştirilmiş, .NET 8 tabanlı bir konsol uygulamasıdır. Kullanıcılar görev oluşturabilir, güncelleyebilir, tamamlandı olarak işaretleyebilir, duruma/önceliğe göre filtreleyebilir ve genel istatistikleri görüntüleyebilir.

Kullanılan .NET Sürümü ve Ön Koşullar
.NET 8 SDK (LTS)
Visual Studio 2022/2026 veya dotnet CLI
## Derleme ve Çalıştırma

1. Depoyu klonlayın veya indirin.
2. Proje klasörüne girin:
cd TaskManager
3. Bağımlılıkları geri yükleyin ve derleyin:
dotnet build
4. Uygulamayı çalıştırın:
dotnet run
## Desteklenen Özellikler

- Yeni görev oluşturma
- Görevleri listeleme
- Görev detayı görüntüleme
- Görev güncelleme (başlık, açıklama, öncelik, teslim tarihi)
- Görevi tamamlandı olarak işaretleme
- Görev silme
- Görevleri duruma, önceliğe, gecikme durumuna veya anahtar kelimeye göre filtreleme
- Görev istatistiklerini görüntüleme (toplam, duruma/önceliğe göre dağılım, gecikmiş görev sayısı, tamamlanma oranı)

## Örnek Kullanım Akışı

1. Uygulama açıldığında menüden `1` seçilerek yeni bir görev eklenir (başlık, açıklama, öncelik, bitiş tarihi girilir).
2. `2` seçilerek tüm görevler listelenir ve eklenen görevin ID'si görülür.
3. `4` seçilip ilgili ID girilerek görev güncellenebilir.
4. `5` seçilip ID girilerek görev tamamlandı olarak işaretlenir.
5. `8` seçilerek genel istatistikler (toplam görev, tamamlanma oranı vb.) görüntülenir.
6. `9` seçilerek uygulamadan çıkılır.

## Bilinen Kısıtlar ve Varsayımlar

- Veriler yalnızca uygulama çalıştığı sürece bellekte tutulur; uygulama kapatıldığında tüm görevler kaybolur.
- Proje şartnamesi .NET 8 hedeflemeyi istemektedir; geliştirme ortamında .NET 10 SDK kullanılmıştır, ancak kod .NET 8 ile uyumludur.