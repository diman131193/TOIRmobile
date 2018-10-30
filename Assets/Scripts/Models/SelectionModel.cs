public class SelectionModel
{
    private string Id;
    private string Name;
    
    public SelectionModel(string id, string name)
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


