using Godot;

[GlobalClass]
public partial class Building : Resource
{
    [Export] public string Name {get; private set;}
    [Export] public Godot.Collections.Dictionary<int, Resource> CostToBuild {get; private set;}

    public Building() : this("") {}

    public Building(string name)
    {
        Name = name;
    }
}