using Godot;
using System;
using System.Collections.Generic;

public partial class Globals : Node
{
    public static Globals Instance {get; private set;}
    public DateTime currentDate = new DateTime(1436, 5, 1);
    public override void _Ready()
    {
        Instance = this;
    }
}