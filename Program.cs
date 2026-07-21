using TaskManager.Enum;
using TaskManager.Services;
using TaskManager.Models;
using TodoTask = TaskManager.Models.Task;
using System.Linq.Expressions;
using System.Text;

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
		case "1":{
			Console.WriteLine("Görev Başlığı: ");
			string title = Console.ReadLine();
			Console.WriteLine("Görev Açıklaması: ");
			string description = Console.ReadLine();
			Console.WriteLine("Görev Önceliği (0-Low, 1-Medium, 2-High): ");
			string priorityInput = Console.ReadLine();
				if(!int.TryParse(priorityInput, out int priorityValue) || priorityValue < 0 || priorityValue > 2)
				{
					Console.WriteLine("Geçersiz öncelik değeri. Lütfen 0, 1 veya 2 girin.");
					break;
				}
				Priority priority = (Priority)int.Parse(priorityInput);
			Console.WriteLine("Görev Bitiş Tarihi (yyyy.mm.dd): ");
			string dueDateInput = Console.ReadLine();
			DateTime parsedDueDate = DateTime.Parse(dueDateInput);

				try
				{
					taskService.AddTask(new TodoTask
					{
						Title = title,
						Description = description,
						Priority = priority,
						DueDate = parsedDueDate,
					});
					Console.WriteLine("Görev başarıyla eklendi.");
				}
				catch(Exception ex)
				{
					
					Console.WriteLine($"Hata: {ex.Message}");
				}
				
			break;
				}
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
				
				if(!Guid.TryParse(idInput, out Guid taskId))
				{
					Console.WriteLine("Geçersiz ID formatı.");
					break;
				}
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
		case "4": {
				Console.WriteLine("Güncellemek İstediğiniz Görev ID: ");
				string idInput = Console.ReadLine();
				if (!Guid.TryParse(idInput, out Guid taskId))
				{
					Console.WriteLine("Geçersiz ID formatı.");
					break;
				}
				var task = taskService.GetTaskById(taskId);
				if (task == null)
				{
					Console.WriteLine("Görev bulunamadı.");
				}
				else
				{ 
					Console.WriteLine($"Mevcut Başlık: {task.Title}, Yeni Başlık: ");
					string updateTitle = Console.ReadLine();
					Console.WriteLine($"Mevcut Açıklama: {task.Description}, Yeni Açıklama: ");
					string updateDescription = Console.ReadLine();
					Console.WriteLine($"Mevcut Öncelik: {task.Priority}, Yeni Öncelik (0-Low, 1-Medium, 2-High): ");
					string updatePriorityInput = Console.ReadLine();
					Console.WriteLine($"Mevcut Durum: {task.Status}, Yeni Durum (0-Pending, 1-InProgress, 2-Completed): ");
					string updateStatusInput = Console.ReadLine();
					Console.WriteLine($"Mevcut Bitiş Tarihi: {task.DueDate}, Yeni Bitiş Tarihi (yyyy-MM-dd): ");
					string updateDueDateInput = Console.ReadLine();
					Priority updatePriority = (Priority)int.Parse(updatePriorityInput);
					Priority updateStatus = (Priority)int.Parse(updateStatusInput);

				}
					break; }
		case "5":
			{ 
				Console.WriteLine("Tamamlamak İstediğiniz Görev ID: ");
				string idInput = Console.ReadLine();
				if (!Guid.TryParse(idInput, out Guid taskId))
				{
					Console.WriteLine("Geçersiz ID formatı.");
					break;
				}
				var task = taskService.GetTaskById(taskId);
				if (task == null)
				{
					Console.WriteLine("Görev bulunamadı.");
				}
				else
				{
					if (task.Status == Status.Completed)
					{
						Console.WriteLine("Hata: Bu görev zaten daha önce tamamlanmış!");
						break; 
					}

					task.Status = Status.Completed;
					task.CompletedAt = DateTime.Now;
					Console.WriteLine("Görev başarıyla tamamlandı.");
					}

				break;}
		case "6":
			{
				Console.WriteLine("Silmek İstediğiniz Görev ID: ");
				string idInput = Console.ReadLine();
				if (!Guid.TryParse(idInput, out Guid taskId))
				{
					Console.WriteLine("Geçersiz ID formatı.");
					break;
				}
				var task = taskService.GetTaskById(taskId);
				if (task == null)
				{
					Console.WriteLine("Görev bulunamadı.");
				}
				else
				{
					taskService.DeleteTask(taskId);
					Console.WriteLine("Görev başarıyla silindi.");
				}

				break;
			}
		case "7":
			{
				Console.WriteLine("Filtreleme Kriteri (1-Durum, 2-Öncelik, 3-Gecikme, 4-Kelimeye Göre Arama): ");
				string filterInput = Console.ReadLine();
				if (filterInput == "1")
				{
					Console.WriteLine("Durum (0-Pending, 1-InProgress, 2-Completed): ");
					string statusInput = Console.ReadLine();
					Status status = (Status)int.Parse(statusInput);
					var filteredTasks = taskService.GetTasksByStatus(status);
					foreach (var task in filteredTasks)
					{
						Console.WriteLine($"ID: {task.Id}, Başlık: {task.Title}, Öncelik: {task.Priority}, Durum: {task.Status}");
					}
				}
				else if (filterInput == "2")
				{
					Console.WriteLine("Öncelik (0-Low, 1-Medium, 2-High): ");
					string filterPriorityInput = Console.ReadLine();
					Priority filterPriority = (Priority)int.Parse(filterPriorityInput);
					var filteredTasks = taskService.GetTasksByPriority(filterPriority);
					foreach (var task in filteredTasks)
					{
						Console.WriteLine($"ID: {task.Id}, Başlık: {task.Title}, Öncelik: {task.Priority}, Durum: {task.Status}");
					}
				}
				else if (filterInput == "3") { 
				 var overDueTasks = taskService.GetAllTasks().Where(t => t.DueDate.HasValue && t.DueDate.Value < DateTime.Now && t.Status != Status.Completed);
					foreach (var task in overDueTasks)
					{
						Console.WriteLine($"ID: {task.Id}, Başlık: {task.Title}, Öncelik: {task.Priority}, Durum: {task.Status}, Bitiş Tarihi: {task.DueDate}");
					}
				}
				else if(filterInput == "4")
				{
					Console.WriteLine("Aranacak Kelime: ");
					string keyword = Console.ReadLine();
					var filteredTasks = taskService.SearchTasks(keyword);
					foreach (var task in filteredTasks)
					{
						Console.WriteLine($"ID: {task.Id}, Başlık: {task.Title}, Açıklama: {task.Description}, Öncelik: {task.Priority}, Durum: {task.Status}");
					}


				}
				else
				{
					Console.WriteLine("Geçersiz filtreleme kriteri.");
				}
			
			break;}
		case "8":
			{

				int toplamGorev = taskService.GetAllTasks().Count();
				int TamamlananGorev = taskService.GetTasksByStatus(Status.Completed).Count();
				Console.WriteLine("Toplam Görev Sayısı: " + taskService.GetAllTasks().Count());
				Console.WriteLine("Tamamlanan Görev Sayısı: " + taskService.GetTasksByStatus(Status.Completed).Count());
				Console.WriteLine("Tamamlanmamış Görev Sayısı: " + taskService.GetTasksByStatus(Status.Pending).Count());
				Console.WriteLine("Yüksek Öncelikli Görev Sayısı: " + taskService.GetTasksByPriority(Priority.High).Count());
				Console.WriteLine("Orta Öncelikli Görev Sayısı: " + taskService.GetTasksByPriority(Priority.Medium).Count());
				Console.WriteLine("Düşük Öncelikli Görev Sayısı: " + taskService.GetTasksByPriority(Priority.Low).Count());
				Console.WriteLine("Tamamlanma Oranı: " + (taskService.GetTasksByStatus(Status.Completed).Count() * 100.0 / taskService.GetAllTasks().Count()) + "%");
				double oran = (taskService.GetTasksByStatus(Status.Completed).Count() * 100.0 / taskService.GetAllTasks().Count());
				break;
			}
		case "9":
			running = false;
			break;
		default:
			Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
			break;
	}

}