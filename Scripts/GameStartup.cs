using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameStartup : Node
{
    [Export] PackedScene GameScene;

    public override void _Ready()
    {
        SetupPawns();
        Node node = GameScene.Instantiate();
        AddChild(node);
    }

    void SetupPawns()
    {
        Globals.Instance.AddPawn(new Pawn("Archibald", true));
        Globals.Instance.AddPawn(new Pawn("Anadriela", false));
    }
}
