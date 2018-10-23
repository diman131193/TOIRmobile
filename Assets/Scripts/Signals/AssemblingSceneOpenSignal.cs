using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct AssemblingSceneOpenSignal
{
    private string Id;
    private string Name;

    public AssemblingSceneOpenSignal(string id, string name)
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

