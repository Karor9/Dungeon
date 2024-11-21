    using Godot;
using System;

public partial class GameManager : Node
{
    [Export] Map map;
    [Export] MapCamera mapCamera;
    [Export] Pathfinding pathfinding;
    [Export] PackedScene pathfindingScene;
    [Export] PawnManager pawnManager;
    public override void _Ready()
    {
        //Map Init
        map.SetMap();
        GD.Print("Map Ready");

        //Camera Init
        mapCamera.SetupCamera();
        GD.Print("Camera Ready");

        //Pathfinding Init
        SetupPathfinding();

        //Pawn init
        pawnManager.SpawnPawn(new Vector2(8 ,8));
        
    }


    void SetupPathfinding()
    {
        Node node = pathfindingScene.Instantiate();
        pathfinding = (Pathfinding)node;
        pathfinding.SetTerrain(map, 300, 300, new Vector2(16, 16));
        Node parent = GetChild(0);
        pawnManager.Pathfinding = pathfinding;
        parent.CallDeferred("add_child", pathfinding);
        
    }
}
