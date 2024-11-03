using System;
using Godot;

public partial class Map : TileMapLayer
{
    [Export] bool Generate;
    [Export] FastNoiseLite altitude = new FastNoiseLite();
    int width = 300;
    int height = 300;
    byte[,] MapArray;
    float _WaterLevel = 0f;

    float _MoveCamera = 0f;
    Vector2 mousePosition;
    public override void _Ready()
    {
        altitude.Frequency = 0.003f;
        MapArray = new byte[width, height];
        SetMap();
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
        altitude.Free();
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