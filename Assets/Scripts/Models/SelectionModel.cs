public class SelectionModel
{
    public string ID;
    public string DESCRIPTIVE_TEXT;
    
    public SelectionModel(string id, string name)
    {
        this.ID = id;
        this.DESCRIPTIVE_TEXT = name;      
    }

    public string getId()
    {
        return this.ID;
    }

    public string getName()
    {
        return this.DESCRIPTIVE_TEXT;
    }
}


