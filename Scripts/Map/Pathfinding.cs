using Godot;
using Godot.Collections;
using System;
[Tool]
public partial class Pathfinding : Node2D
{
    [Export] TileMapLayer terrain;
    AStarGrid2D astarGrid = new AStarGrid2D();
    Vector2[] path = {};

    [Export] bool calculate;
    [Export] Vector2I start = new Vector2I(0, 0);
    [Export] Vector2I end = new Vector2I(50, 50);

    public override void _Ready()
    {
        InitPathfinding();
        GD.Print("Pathfinding Ready");
    }

    public void SetTerrain(TileMapLayer map)    
    {
        terrain = map;
    }

    // public override void _Input(InputEvent @event)
    // {
    //     if(@event.IsActionPressed("rmb"))
    //         InitPathfinding();
    //     if(@event.IsActionPressed("lmb"))
    //         FindPath();
    // }

    public Vector2[] RequestPath(Vector2I s, Vector2I e)
    {
        path = astarGrid.GetPointPath(s, e);
        for (int i = 0; i < path.Length; i++)
        {
            path[i] += new Vector2(8, 8);
        }
        QueueRedraw();
        return path;
    }

    public override void _Draw()
    {
        if(path.Length > 0)
        {
            for (int i = 0; i < path.Length-1; i++)
            {
                DrawLine(path[i], path[i+1], Colors.Red);
            }
        }
    }

    public override void _Process(double delta)
    {
        if(calculate)
        {
            calculate = false;
            FindPath();
        }
    }

    private void FindPath()
    {
        path = astarGrid.GetPointPath(start, end);
        QueueRedraw();
    }


    void InitPathfinding()
    {
        astarGrid.Region = new Rect2I(0, 0, 300, 300);
        astarGrid.CellSize = new Vector2(16, 16);
        astarGrid.DiagonalMode = AStarGrid2D.DiagonalModeEnum.OnlyIfNoObstacles;   
        astarGrid.Update();
        for (int x = 0; x < 300; x++)
        {
            for (int y = 0; y < 300; y++)
            {
                float diff = GetTerrainDifficulty(new Vector2I(x, y));
                if(diff > 0.2f)
                    astarGrid.SetPointWeightScale(new Vector2I(x, y), diff);
                else
                    astarGrid.SetPointSolid(new Vector2I(x, y));
            }
        }
    }

    public float GetTerrainDifficulty(Vector2I coords)
    {
        TileData tileData = terrain.GetCellTileData(coords);
        float result = (float)tileData.GetCustomData("terrainDifficulty");

        return result;
    }

}
