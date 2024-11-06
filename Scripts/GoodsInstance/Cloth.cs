using System;
using Godot;

public partial class Cloth : Good
{
    [Export] public int[] Slots;

    public Cloth(int amount,  Vector2 position, int[] slots) : base(amount, position)
    {
        Slots = slots;
    }
}