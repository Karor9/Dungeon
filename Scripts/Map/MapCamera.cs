using Godot;

public partial class MapCamera : Camera2D
{
    [Export] float _CameraMovement = 10f;
    [Export] float _Speed = 5f;
    float _MoveCamera;
    Vector2I screen;
    Vector2 mousePosition;
    public override void _Ready()
    {
        SetupCamera();
        GD.Print("testup");
    }
    void SetupCamera()
    {
        screen = DisplayServer.ScreenGetSize();
        _MoveCamera = _Speed * _CameraMovement;
        LimitRight = 16 * 300;
        LimitBottom = 16 * 300;
        Position = new Vector2I(300 * 8, 300 * 8);
    }

    public override void _Process(double delta)
    {
        mousePosition = GetViewport().GetMousePosition();
        if(mousePosition.X <= screen.X / 10 && Position.X > LimitLeft && Position.X > screen.X/2 * 1/Zoom.X)
            Position += new Vector2(-1 * _MoveCamera, 0);
        if(mousePosition.X >= screen.X - (screen.X / 10) && Position.X < LimitRight  && Position.X < ((300 * 16) - screen.X/2))
            Position += new Vector2(_MoveCamera, 0);
        if(mousePosition.Y <= screen.Y / 10 && Position.Y > LimitTop && Position.Y > screen.Y/2 * 1/Zoom.Y)
            Position += new Vector2(0, _MoveCamera * -1);
        if(mousePosition.Y >= screen.Y - (screen.Y / 10) && Position.Y < LimitBottom && Position.Y < ((300 * 16) - screen.Y/2))
            Position += new Vector2(0, _MoveCamera);
    }

    void ZoomCamera(Vector2 val)
    {
        Vector2 mousePos = GetGlobalMousePosition();
        Zoom += val;
        Vector2 newMousePos = GetGlobalMousePosition();
        Position += mousePos - newMousePos;
        if(Position.X < screen.X/2)
            Position = new Vector2(screen.X/2, Position.Y);
        if(Position.Y < screen.Y/2)
            Position = new Vector2(Position.X, screen.Y/2);
        if(Position.X > (300*16) - screen.X/2)
            Position = new Vector2((300*16) - screen.X/2, Position.Y);
        if(Position.Y > (300*16) - screen.Y/2)
            Position = new Vector2(Position.X, (300*16) - screen.Y/2);
    }

    public override void _Input(InputEvent @event)
    {
        switch(@event)
        {
            case var _ when @event.IsActionPressed("zoom_in"):
                ZoomCamera(new Vector2(0.1f, 0.1f));
                break;
            case var _ when @event.IsActionPressed("zoom_out"):
                float zoom = Zoom.X;
                if(zoom - 0.1f <= 0f || 1/zoom * screen.X > LimitRight || 1/zoom * screen.Y > LimitBottom)
                    return;
                ZoomCamera(new Vector2(-0.1f, -0.1f));
                break;
        }
    }
}
