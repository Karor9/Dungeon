using System.Linq;
using Godot;
// [Tool]
public partial class Pathfinding : Node2D
{
    [Export] TileMapLayer terrain;
    int width;
    int height;
    Vector2 cellSize;


    AStarGrid2D astarGrid = new AStarGrid2D();
    Vector2[] path = {};


    [Export] bool calculate;
    [Export] Vector2I start;
    [Export] Vector2I end;

    public override void _Ready()
    {
        InitPathfinding();
        GD.Print("Pathfinding Ready");
    }

    public void SetTerrain(TileMapLayer map, int w, int h, Vector2 cs)    
    {
        terrain = map;
        width = w;
        height = h;
        cellSize = cs;
    }

    public Vector2[] RequestPath(Vector2I s, Vector2I e)
    {
        path = astarGrid.GetPointPath(s, e);
        for (int i = 0; i < path.Length; i++)
        {
            path[i] += cellSize/2;
        }
        // QueueRedraw();
        return path;
    }

    public Vector2[] RequestSomePath(Vector2I s, Vector2I e)
    {
        Vector2[] p = astarGrid.GetPointPath(s, e);
        for (int i = 0; i < p.Length; i++)
        {
            p[i] += cellSize/2;
        }
        // QueueRedraw();
        return p;
    }

    public Vector2[] RequestAdditionalPath(Vector2I s, Vector2I e, Vector2[] p)
    {
        int oldPathLength = p.Length;
        Vector2[] addPath = astarGrid.GetPointPath(s, e);
        if(addPath.Length == 0)
            return path;
        path = p.Concat(addPath).ToArray();
        for (int i = oldPathLength; i < path.Length; i++)
        {
            path[i] += cellSize/2;
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

    public void Redraw(Vector2[] p)
    {
        path = p;
        QueueRedraw();
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
        astarGrid.Region = new Rect2I(0, 0, width, height);
        astarGrid.CellSize = cellSize;
        astarGrid.DiagonalMode = AStarGrid2D.DiagonalModeEnum.OnlyIfNoObstacles;   
        astarGrid.Update();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
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

    public bool GetSpawnable(Vector2I coords)
    {
        TileData tileData = terrain.GetCellTileData(coords);
        return (bool)tileData.GetCustomData("SpawnableTerrain");
    }

}
