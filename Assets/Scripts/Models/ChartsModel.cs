using System;

[Serializable]
public class ChartsModel
{
    public string INSTANCE_NUMBER;
    public string URL;
    
    public ChartsModel(string id, string url)
    {
        this.INSTANCE_NUMBER = id;
        this.URL = url;      
    }

    public string getId()
    {
        return this.INSTANCE_NUMBER;
    }

    public string getUrl()
    {
        return this.URL;
    }
}


