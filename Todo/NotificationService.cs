using System.Collections.Concurrent;

namespace Todo;

public class NotificationService
{
    private readonly ConcurrentDictionary<string, Func<Task>> _subscribers = new();

    public async Task AddSubscriberWithCallBack(string subscriberId, Func<Task> callback)
    {
        _subscribers.TryAdd(subscriberId, callback);
        await callback();
    }

    public void RemoveSubscriber(string subscriberId)
    {
        _subscribers.Remove(subscriberId, out _);
    }

    public void NotifySubscribers()
    {
        foreach (var callback in _subscribers.Values)
        {
            Task.Run(async () =>
            {
                try
                {
                    await callback();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Subscriber notification failed: {ex.Message}");
                }
            });
        }
    }
}