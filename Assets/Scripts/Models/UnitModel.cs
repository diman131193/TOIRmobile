public class UnitModel
{
    private string Id;
    private string Name;
    
    public UnitModel(string id, string name)
    {
        this.Id = id;
        this.Name = name;      
    }

    public string getId()
    {
        return this.Id;
    }

    public string getName()
    {
        return this.Name;
    }
}


