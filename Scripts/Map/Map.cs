using System;
using System.Runtime.Serialization;
using Godot;

public partial class Map : TileMapLayer
{
    FastNoiseLite moisture = new FastNoiseLite();
    FastNoiseLite temperature = new FastNoiseLite();
    FastNoiseLite altitude = new FastNoiseLite();


    int width = 120;
    int height = 70;

    float _WaterLevel = 0f;
    [ExportCategory("Camera")]
    [Export] Camera2D camera;
    [Export] float _CameraMovement = 10f;
    [Export] float _Speed = 5f;

    [ExportCategory("Map")]
    [Export] int land;
    [Export] int water;

    float _MoveCamera = 0f;
    Vector2 mousePosition;
    Vector2I screen;
    public override void _Ready()
    {
        _MoveCamera = _Speed * _CameraMovement;
        screen = DisplayServer.ScreenGetSize();
        SetupNoise();
        SetupCamera();
        GenerateMap();
    }

    public override void _Process(double delta)
    {
        mousePosition = GetViewport().GetMousePosition();
        if(mousePosition.X <= screen.X / 10 && camera.Position.X > camera.LimitLeft)
            camera.Position += new Vector2(-1 * _MoveCamera, 0);
        if(mousePosition.X >= screen.X - (screen.X / 10) && camera.Position.X < camera.LimitRight)
            camera.Position += new Vector2(_MoveCamera, 0);
        if(mousePosition.Y <= screen.Y / 10 && camera.Position.Y > camera.LimitTop)
            camera.Position += new Vector2(0, _MoveCamera * -1);
        if(mousePosition.Y >= screen.Y - (screen.Y / 10) && camera.Position.Y < camera.LimitBottom)
            camera.Position += new Vector2(0, _MoveCamera);
    }

    void SetupNoise()
    {
        moisture.Seed = (int)GD.Randi();
        temperature.Seed = (int)GD.Randi();
        altitude.Seed = (int)GD.Randi();

        altitude.Frequency = 0.001f;
    }

    void GenerateMap()
    {
        land = 0;
        water = 0;

        while(land / 3 <= water)
        {
            (land, water) = CheckWaterMap();
        }
        _WaterLevel += 0.005f;

        //Set default map
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SetMap(x, y);
            }
        }

        //Set forests
        float forestPerc = (float)GD.RandRange(0.2, 0.4);
        int maxTrees =  (int)(land * forestPerc);
        int _x = 0;
        int _y = 0;
        for (int i = 0; i < maxTrees; i++)
        {
            _x = GD.RandRange(0, width-1);
            _y = GD.RandRange(0, height-1);

            if(altitude.GetNoise2D(_x, _y) > _WaterLevel)
                SetCell(new Vector2I(_x, _y), 0, new Vector2I(2, 1));
        }

        //Set mountains
        float mountPerc = (float)GD.RandRange(0.1, 0.3);
        int maxMountains = (int)(land * mountPerc);
        _x = GD.RandRange(0, width-1);
        _y = GD.RandRange(0, height-1);
        while(altitude.GetNoise2D(_x, _y) < _WaterLevel)
        {
            _x = GD.RandRange(0, width-1);
            _y = GD.RandRange(0, height-1);
        }
        while(maxMountains > 0)
        {
            if(altitude.GetNoise2D(_x, _y) <= _WaterLevel)
            {
                maxMountains--;
                continue;
            }
            
            maxMountains--;
            SetCell(new Vector2I(_x, _y), 0, new Vector2I(2, 0));
            _x += (int)GD.RandRange(-1, 1);
            _y += (int)GD.RandRange(-1, 1);
            if(_x < 0)
                _x = 0;
            if(_x >= width)
                _x = width - 1;
            if(_y < 0)
                _y = 0;
            if(_y >= height)
                _y = height - 1;
        }
    }

    (int, int) CheckWaterMap()
    {
        int l = 0;
        int w = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(altitude.GetNoise2D(x, y) < _WaterLevel)
                    w += 1;
                else
                    l += 1;
            }
        }
        _WaterLevel -= 0.005f;
        return (l, w);
    }

    void SetMap(int x, int y)
    {
        float moist = moisture.GetNoise2D(x, y);
        float temp = temperature.GetNoise2D(x, y);
        float alt = altitude.GetNoise2D(x, y);

        // float xAtlas = 3 * (moist + 10)/ 20;
        float yAtlas = 3f * (temp + 1f)/2f;
        if(alt > _WaterLevel)
            // SetCell(new Vector2I(x, y), 0, new Vector2I(1, 2));
            SetLand(x, y);
            // SetCell(new Vector2I(x, y), 0, new Vector2I((int)xAtlas, (int)yAtlas));
        else
            SetWater(x, y, yAtlas);
    }

    void SetLand(int x, int y)
    {
        SetCell(new Vector2I(x, y), 0, new Vector2I(1, 2));
    }

    void SetWater(int x, int y, float yAtlas)
    {
        SetCell(new Vector2I(x, y), 0, new Vector2I(3, (int)yAtlas));
    }


    void SetupCamera()
    {
        camera.LimitRight = 16 * width;
        camera.LimitBottom = 16 * height;
    }
}