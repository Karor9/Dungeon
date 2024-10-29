using System;
using Godot;

public partial class Cloth : Good
{
    [Export] public int[] Slots;

    public Cloth(int amount,  int[] slots) : base(amount)
    {
        Slots = slots;
    }
}