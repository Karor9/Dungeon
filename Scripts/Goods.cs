using Godot;

[GlobalClass]
public partial class Goods : Resource
{
    [Export] public string Name {get; set;}
    [Export] public Texture2D Icon {get;set;}
    [Export] public Godot.Collections.Array<string> Tags {get; set;}

    public double Count {get; set;} = 0;

    public Goods() : this("[BlankGoodName]", null, new Godot.Collections.Array<string>()) {}

    public Goods(string name, Texture2D icon, Godot.Collections.Array<string> tags)
    {
        Name = name;
        Icon = icon;
        Tags = tags;
    }
}