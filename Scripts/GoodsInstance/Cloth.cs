using System;
using Godot;

public partial class Cloth : Resource
{
    public int[] Slots;

    public Cloth(int[] slots)
    {
        Slots = slots;
    }
}