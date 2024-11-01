using Godot;

public partial class Map : TileMapLayer
{
    [Export] bool Generate;
    [Export] FastNoiseLite altitude = new FastNoiseLite();
    int width = 300;
    int height = 300;
    byte[,] MapArray;
    float _WaterLevel = 0f;
    [ExportCategory("Camera")]
    [Export] Camera2D camera;
    [Export] float _CameraMovement = 10f;
    [Export] float _Speed = 5f;

    float _MoveCamera = 0f;
    Vector2 mousePosition;
    Vector2I screen;
    public override void _Ready()
    {
        altitude.Frequency = 0.003f;
        SetMap();
        SetupCamera();
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
                if(GD.Randf() < 0.25)
                    SetCell(new Vector2I(x, y), 0, new Vector2I(2, 1));
                else
                    SetCell(new Vector2I(x, y), 0, new Vector2I(1, 2));
                return;
        }
    }

    public override void _Process(double delta)
    {
        // if(Generate)
        // {
        //     Generate = !Generate;
        //     SetMap();
        // }
        mousePosition = GetViewport().GetMousePosition();
        if(mousePosition.X <= screen.X / 10 && camera.Position.X > camera.LimitLeft && camera.Position.X > screen.X/2)
            camera.Position += new Vector2(-1 * _MoveCamera, 0);
        if(mousePosition.X >= screen.X - (screen.X / 10) && camera.Position.X < camera.LimitRight  && camera.Position.X < (width * 16) - screen.X/2)
            camera.Position += new Vector2(_MoveCamera, 0);
        if(mousePosition.Y <= screen.Y / 10 && camera.Position.Y > camera.LimitTop && camera.Position.Y > screen.Y/2)
            camera.Position += new Vector2(0, _MoveCamera * -1);
        if(mousePosition.Y >= screen.Y - (screen.Y / 10) && camera.Position.Y < camera.LimitBottom && camera.Position.Y < (height * 16) - screen.Y/2)
            camera.Position += new Vector2(0, _MoveCamera);
    }


    void SetupCamera()
    {
        screen = DisplayServer.ScreenGetSize();
        _MoveCamera = _Speed * _CameraMovement;
        camera.LimitRight = 16 * width;
        camera.LimitBottom = 16 * height;
        camera.Position = new Vector2I(width * 8, height * 8);
        // camera.Offset = new Vector2I(-1 * screen.X / 2, -1*screen.Y / 2);
    }


}