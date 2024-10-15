using Godot;

[GlobalClass]
public partial class UsableItem : Goods
{
    [Export] public double Durability;

    public UsableItem() : this(0) {}

    public UsableItem(double durability)
    {
        Durability = durability;
    }
}