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
    [Export] public double Durability {get; private set;}

    public Tool(int amount, Vector2 position, DateTime expiration, double durability) : base(amount, position, expiration)
    {
        Durability = durability;
    }

}