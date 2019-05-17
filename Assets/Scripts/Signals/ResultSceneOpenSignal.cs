using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct ResultSceneOpenSignal
{
    private string Name;

    public ResultSceneOpenSignal(string name)
    {
        this.Name = name;
    }

    public string getName()
    {
        return this.Name;
    }
}
