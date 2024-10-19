using System.Xml.Serialization;
using Godot;
// public enum Quality
// {
//     Common,
//     Uncommon,
//     Rare,
//     Epic,
//     Legendary,
//     Mythic
// }

public class Tool
{
    public int Id {get; private set;}
    public double Durability {get; private set;}
    public byte Quality {get; private set;}

    public Tool(int id, double durability)
    {
        Id = id;
        Durability = durability;
        SetupQuality();
    }

    void SetupQuality()
    {
        Quality = (byte)GD.RandRange(0, 5);
    }
}