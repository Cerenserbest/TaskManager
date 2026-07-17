using TaskManager.Enum;
using TaskManager.Services;
using TaskManager.Models;
using TodoTask = TaskManager.Models.Task;

TaskService taskService = new TaskService();
bool running = true;
while (running) 
{
	Console.WriteLine("--- GÖREV YÖNETİM SİSTEMİ ---");
	Console.WriteLine("1. Görev Ekle");
	Console.WriteLine("2. Görevleri Listele");
	Console.WriteLine("3. Görev Detayı Görüntüle");
	Console.WriteLine("4. Görev Güncelle");
	Console.WriteLine("5. Görevi Tamamla");
	Console.WriteLine("6. Görev Sil");
	Console.WriteLine("7. Görevleri Filtrele");
	Console.WriteLine("8. İstatistikleri Görüntüle");
	Console.WriteLine("9. Çıkış");
	Console.Write("Seçiminizi yapın: ");

	string input = Console.ReadLine();
	switch (input) 
	{
		case "1":
			Console.WriteLine("Görev Başlığı: ");
			string title = Console.ReadLine();
			Console.WriteLine("Görev Açıklaması: ");
			string description = Console.ReadLine();
			Console.WriteLine("Görev Önceliği (0-Low, 1-Medium, 2-High): ");
			string priorityInput = Console.ReadLine();
			Priority priority = (Priority)int.Parse(priorityInput);
			taskService.AddTask(new TodoTask { Title = title,
				Description = description,
				Priority = priority,
			});
			Console.WriteLine("Görev başarıyla eklendi.");
			break;
		case "2":
			var tasks = taskService.GetAllTasks();
			if (tasks.Count() == 0)
			{
				Console.WriteLine("Henüz eklenmiş bir görev bulunamadı.");
			}
			else
			{
				foreach (var task in tasks)
				{
					Console.WriteLine($"ID: {task.Id}, Başlık: {task.Title}, Öncelik: {task.Priority}, Durum: {task.Status}");
				}
			}
			break;
		case "3": {
				Console.WriteLine("Detayını Görmek İstediğiniz Görev ID: ");
				string idInput = Console.ReadLine();
				Guid taskId = Guid.Parse(idInput);
				var task = taskService.GetTaskById(taskId);
				if (task == null) {
					Console.WriteLine("Görev bulunamadı.");
				}
				else
				{
					Console.WriteLine($"ID: {task.Id}, Başlık: {task.Title}, Açıklama: {task.Description}, Öncelik: {task.Priority}, Durum: {task.Status}, Bitiş Tarihi: {task.DueDate}");
					if (task.CompletedAt.HasValue)
					{
						Console.WriteLine($"Tamamlanma Tarihi:  {task.CompletedAt}");
					}
				}
				break; }
		case "4":
			break;
		case "5":
			break;
		case "6":
			break;
		case "7":
			break;
		case "8":
			
			break;
		case "9":
			running = false;
			break;
		default:
			Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
			break;
	}

}