using Godot;
using System;

[GlobalClass]
public partial class Clothes : Resource
{
    [Export] public int Slots;

    public Clothes() : this(0) {}

    public Clothes(int s)
    {
        Slots = s;
    }
}
