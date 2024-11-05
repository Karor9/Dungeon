using System;
using Godot;

public partial class Map : TileMapLayer
{
    FastNoiseLite altitude = new FastNoiseLite();
    FastNoiseLite forest = new FastNoiseLite();

    int width = 300;
    int height = 300;
    float _WaterLevel = 0f;

    float _MoveCamera = 0f;
    Vector2 mousePosition;

    [Export] PackedScene _DebugPawn;
    [Export] Node _DebugPawnsNode;
    [Export] PackedScene PathfindingScene;
    Pathfinding Pathfinding;
    public override void _Ready()
    {
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
        path.SetTerrain((TileMapLayer)this, 300, 300, new Vector2(16, 16));
        Pathfinding = path;
        Node parent = GetParent();
        parent.CallDeferred("add_child", path);
    }


    void SetMap()
    {
        altitude.Frequency = 0.003f;
        SetupForest();
        altitude.Seed = 4;
        // altitude.Seed = (int)GD.Randi();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SetCell(x, y);
            }
        }
        altitude.Dispose();
        forest.Dispose();
    }

    private void SetupForest()
    {
        forest.Seed = 4;
        // forest.Seed = (int)GD.Randi();
        forest.NoiseType = FastNoiseLite.NoiseTypeEnum.Cellular;
        forest.Frequency = 1f;
        forest.CellularDistanceFunction = FastNoiseLite.CellularDistanceFunctionEnum.Hybrid;
        forest.CellularReturnType = FastNoiseLite.CellularReturnTypeEnum.CellValue;
    }


    void SetCell(int x, int y)
    {
        switch(altitude.GetNoise2D(x, y))
        {
            case float z when z >= 0.625f:
                SetCell(new Vector2I(x, y), 0, new Vector2I(2, 0));
                return;
            case float z when z <= _WaterLevel:
                SetCell(new Vector2I(x, y), 0, new Vector2I(3, 3));
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
        float val = forest.GetNoise2D(x, y);
        if(val < 0.25f && val > 0f)
        {
            SetCell(new Vector2I(x, y), 0, new Vector2I(2, 1));
            return;
        }
        SetCell(new Vector2I(x, y), 0, new Vector2I(1, 2));
    }
}