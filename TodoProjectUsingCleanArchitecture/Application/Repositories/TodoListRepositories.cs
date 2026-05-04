using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TodoProjectUsingCleanArchitecture.Application.Models;

namespace TodoProjectUsingCleanArchitecture.Application.Repositories
{
    public class TodoListRepositories : ITodoListRepositories
    {
        // Path: Application/Database/Demo/tasks.json  (relative to the project root)
        private readonly string _filePath;

        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public TodoListRepositories()
        {
            // Resolve relative to the directory that contains the running assembly
            var baseDir = AppContext.BaseDirectory;
            _filePath = Path.GetFullPath(
                Path.Combine(baseDir, "..", "..", "..", "Application", "Database", "Demo", "tasks.json"));
        }

        // ── helpers ──────────────────────────────────────────────────────────

        private async Task<List<TaskItem>> ReadAllAsync()
        {
            if (!File.Exists(_filePath))
                return new List<TaskItem>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonConvert.DeserializeObject<List<TaskItem>>(json, _jsonSettings) ?? new List<TaskItem>();
        }

        private async Task WriteAllAsync(List<TaskItem> tasks)
        {
            var json = JsonConvert.SerializeObject(tasks, _jsonSettings);
            await File.WriteAllTextAsync(_filePath, json);
        }

        // ── interface implementation ──────────────────────────────────────────

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await ReadAllAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            var tasks = await ReadAllAsync();
            var existing = tasks.FirstOrDefault(t => t.Id == id);
            return existing;
        }

        public async Task<bool> CreateAync(TaskItem taskItem)
        {
            var tasks = await ReadAllAsync();

            // Prevent duplicate IDs
            if (tasks.Any(t => t.Id == taskItem.Id))
                return false;

            tasks.Add(taskItem);
            await WriteAllAsync(tasks);
            return true;
        }

        public async Task<bool> UpdateAync(TaskItem taskItem)
        {
            var tasks = await ReadAllAsync();
            var index = tasks.FindIndex(t => t.Id == taskItem.Id);

            if (index == -1)
                return false;

            tasks[index] = taskItem;
            await WriteAllAsync(tasks);
            return true;
        }
        public async Task<bool> DeleteAync(Guid id)
        {
            var tasks = await ReadAllAsync();
            var existing = tasks.FirstOrDefault(t => t.Id == id);

            if (existing is null)
                return false;

            tasks.Remove(existing);
            await WriteAllAsync(tasks);
            return true;
        }
    }
}

