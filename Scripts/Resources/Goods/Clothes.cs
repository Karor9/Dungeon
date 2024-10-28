using Godot;
using System;

[GlobalClass]
public partial class Clothes : Resource
{
    [Export] public int[] Slots;

    public Clothes() : this(new int[] { }) {}

    public Clothes(int[] s)
    {
        Slots = s;
    }
}
