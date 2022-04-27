namespace auction.Data;

public class TalkService
{
    public TalkService()
    {
        history = new List<string>();
    }

    public Action<string> OnChange { get; set; }


    // inform all users of new message
    public Task SendAsync(string message)
    {
        // add to history
        history.Add(message);
        // ensure only last 10 shown
        if (history.Count > 10) history.RemoveAt(0);
        OnChange.Invoke(message);
        return Task.FromResult(0);
    }

    private readonly List<string> history;

    public IReadOnlyList<string> GetHistory() => history;
}