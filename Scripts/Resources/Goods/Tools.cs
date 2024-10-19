using Godot;

public enum Quality
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic
}


[GlobalClass]
public partial class Tools : Goods
{
    [Export] public double Durability {get; set;}
    [Export] public Quality Quality {get; set;}

}