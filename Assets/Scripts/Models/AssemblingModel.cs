public class AssemblingModel
{
    private string Id;
    private string Name;
    
    public AssemblingModel(string id, string name)
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


