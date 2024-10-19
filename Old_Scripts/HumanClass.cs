using Godot;

[GlobalClass]
public partial class HumanClass : Resource
{
    [Export] public string ClassName {get; set;}
    public HumanClass() : this("[Input Name Class]") {}
    public HumanClass(string name)
    {
        ClassName = name;
    }
}