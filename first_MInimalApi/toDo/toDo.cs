namespace first_MinimalApi;

public class toDo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Value { get; set; }
    public bool IsCompleted { get; set; }
}