namespace Todo;

public class InMemoryDatabase
{
    public Dictionary<Guid, TodoItem> ItemsById { get; set; } = new();

    public void AddItemAsync(TodoItem item)
    {
        ItemsById.Add(item.Id, item);
    }

    public void RemoveItemAsync(Guid id)
    {
        ItemsById.Remove(id);
    }

    public void UpdateItemAsync(TodoItem item)
    {
        ItemsById[item.Id] = item;
    }
}

public record TodoItem(Guid Id, string Name, bool IsComplete);