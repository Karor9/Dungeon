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

    public Tool(int amount, DateTime expiration, double durability) : base(amount, expiration)
    {
        Durability = durability;
    }

}