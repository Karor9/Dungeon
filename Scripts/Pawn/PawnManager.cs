using Godot;
using System;

public partial class PawnManager : Node2D
{
    [Export] PackedScene Pawn;
    [Export] Panel ItemContextPanel;
    [Export] Map Map;
    [Export] Node PawnsNode;
    public Pathfinding Pathfinding;


    public void SpawnPawn(Vector2 position)
    {
        Node node = Pawn.Instantiate();
        MovablePawn pawn = node as MovablePawn;
        pawn.Position = position;
        pawn.terrain = Map;
        pawn.ItemContextPanel = ItemContextPanel;
        pawn.Name = "0";
        pawn.pathfinding = Pathfinding;
        PawnsNode.CallDeferred("add_child", pawn);
    }
}
