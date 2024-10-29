using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Goods : Resource
{
    [Export] public string Name {get; set;}
    [Export] public Texture2D Icon {get; set;}
    [Export] public Fuels FuelComponent {get; set;} = null;

    [Export] public Foods FoodComponent {get; set;} = null;


    public Goods() : this("[BlankGoodsName]", null) {}

    public Goods(string name, Texture2D icon)
    {
        Name = name;
        Icon = icon;
    }
}