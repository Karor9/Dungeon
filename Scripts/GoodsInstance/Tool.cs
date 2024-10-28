using System;
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

public partial class Tool : Good
{
    public double Durability {get; private set;}
    public byte Quality {get; private set;}

    public Tool(int id, int amount, DateTime expiration, double durability) : base(id, amount, expiration)
    {
        Durability = durability;
        SetupQuality();
    }

    void SetupQuality()
    {
        Quality = (byte)GD.RandRange(0, 5);
    }
}