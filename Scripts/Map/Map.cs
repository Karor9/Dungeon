using System;
using Godot;

public partial class Map : TileMapLayer
{
    FastNoiseLite altitude = new FastNoiseLite();
    int width = 300;
    int height = 300;
    byte[,] MapArray;
    float _WaterLevel = 0f;

    float _MoveCamera = 0f;
    Vector2 mousePosition;

    [Export] PackedScene _DebugPawn;
    [Export] Node _DebugPawnsNode;
    [Export] PackedScene PathfindingScene;
    Pathfinding Pathfinding;
    public override void _Ready()
    {
        altitude.Frequency = 0.003f;
        MapArray = new byte[width, height];
        SetMap();
        GD.Print("Map Ready");
        SetupPathfinding();
        SetupPawn();
    }

    private void SetupPawn()
    {
        Node node = _DebugPawn.Instantiate();
        MovablePawn _pawn = node as MovablePawn;
        _pawn.Position = new Vector2I(8 ,8);
        _pawn.terrain = this;
        _pawn.pathfinding = Pathfinding;
        _DebugPawnsNode.CallDeferred("add_child", _pawn);
    }

    void SetupPathfinding()
    {
        Node node = PathfindingScene.Instantiate();
        Pathfinding path = (Pathfinding)node;
        path.SetTerrain((TileMapLayer)this);
        Pathfinding = path;
        Node parent = GetParent();
        parent.CallDeferred("add_child", path);
        // parent.AddChild(path);
    }


    void SetMap()
    {
        altitude.Seed = (int)GD.Randi();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SetCell(x, y);
            }
        }
        altitude.Dispose();
    }

    void SetCell(int x, int y)
    {
        switch(altitude.GetNoise2D(x, y))
        {
            case float z when z >= 0.625f:
                SetCell(new Vector2I(x, y), 0, new Vector2I(2, 0));
                MapArray[x, y] = 2;
                return;
            case float z when z <= _WaterLevel:
                SetCell(new Vector2I(x, y), 0, new Vector2I(3, 3));
                MapArray[x, y] = 15;
                return;
            case float z when z > _WaterLevel && z < 0.0425:
                SetCell(new Vector2I(x, y), 0, new Vector2I(0, 2));
                return;
            default:
                SetGround(x, y);
                return;
        }
    }

    private void SetGround(int x, int y)
    {
        if(GD.Randf() < 0.25)
        {
            SetCell(new Vector2I(x, y), 0, new Vector2I(2, 1));
            MapArray[x, y] = 6;
            return;
        }
        SetCell(new Vector2I(x, y), 0, new Vector2I(1, 2));
        MapArray[x, y] = 9;
    }
}