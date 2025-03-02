namespace first_MinimalApi;

public interface ItoDoService
{
    void Create(toDo ToDo);
    void Delete(Guid id);
    List<toDo> GetAll();
    toDo GetById(Guid id);
    void Update(toDo ToDo);
}

public class toDoService : ItoDoService
{
    public toDoService()
    {
        var sampleToDo = new toDo {Value = "First MinimalApi"};
        _toDOs[sampleToDo.Id] = sampleToDo;
    }
    private readonly Dictionary<Guid, toDo> _toDOs = new();
    public void Create(toDo ToDo)
    {
        if(ToDo == null)
        {
            return;
        }
        _toDOs[ToDo.Id] = ToDo;
    }

    public void Delete(Guid id)
    {
        _toDOs.Remove(id);
    }

    public List<toDo> GetAll()
    {
        return _toDOs.Values.ToList();
    }

    public toDo GetById(Guid id)
    {
        return _toDOs.GetValueOrDefault(id);
    }

    public void Update(toDo ToDo)
    {
        var existingToDo = GetById(ToDo.Id);
        if(existingToDo == null)
        {
            return;
        }
        _toDOs[ToDo.Id] = ToDo;
    }

}
