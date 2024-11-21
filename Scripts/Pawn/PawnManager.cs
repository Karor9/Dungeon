using Godot;
using System;

public partial class PawnManager : Node2D
{
    [Export] PackedScene Pawn;
    [Export] Panel ItemContextPanel;
    [Export] Map Map;
    [Export] Node PawnsNode;
    int min = 0;
    int max = 40;
    public Pathfinding Pathfinding;


    public void SetupPawns()
    {
        //Position offset new Vector2(8, 8)
        foreach (int item in Globals.Instance.Pawns.Keys)
        {
            Vector2? position = GenerateSpawn();
            if(position != null)
                SpawnPawn((Vector2)position + new Vector2(8,8), item.ToString());
        }
    }

    private Vector2? GenerateSpawn()
    {
        Vector2? result = null;
        int tries = 0;
        while(result == null && tries < 20)
        {
            int x = GD.RandRange(min, max - 1);
            int y = GD.RandRange(min, max - 1);
            if(Pathfinding.GetSpawnable(new Vector2I(x, y)))
            {
                result = new Vector2(x*16, y*16);
            }
            tries++;
        }
        if(tries < 20)
            return (Vector2)result;
        GD.PrintErr("Failed to spawn pawn");
        return null;
    }


    void SpawnPawn(Vector2 position, string Name)
    {
        Node node = Pawn.Instantiate();
        MovablePawn pawn = node as MovablePawn;
        pawn.Position = position;
        pawn.terrain = Map;
        pawn.ItemContextPanel = ItemContextPanel;
        pawn.Name = Name;
        pawn.pathfinding = Pathfinding;
        PawnsNode.CallDeferred("add_child", pawn);
    }
}
