using System.Linq;
using Godot;

public partial class MovablePawn : CharacterBody2D
{
    float _Speed = 50f;

    [Export] public Pathfinding pathfinding;
    [Export] public Map terrain;

    Vector2[] path = {};

    public override void _PhysicsProcess(double delta)
    {
        if(Input.IsActionJustPressed("left_mouse_click") && !Input.IsActionJustPressed("append_path")) //To be clearded
        {
            Vector2 pos = Position / terrain.RenderingQuadrantSize;
            Vector2 targetPos = GetGlobalMousePosition() / terrain.RenderingQuadrantSize;

            path = pathfinding.RequestPath((Vector2I)pos, (Vector2I)targetPos);
        }
        if(Input.IsActionJustPressed("append_path"))
        {
            GD.Print("appended");
            Vector2 pos = path[path.Length-1] / terrain.RenderingQuadrantSize;
            Vector2 targetPos = GetGlobalMousePosition() / terrain.RenderingQuadrantSize;
            path = path.Concat(pathfinding.RequestPath((Vector2I)pos, (Vector2I)targetPos)).ToArray();
        }
        if(path.Length > 0) 
        {
            Vector2 dir = GlobalPosition.DirectionTo(path[0]);
            float terrainDiff = pathfinding.GetTerrainDifficulty((Vector2I)Position / terrain.RenderingQuadrantSize);

            Velocity = dir * _Speed * (1/ terrainDiff);

            if(Position.DistanceTo(path[0]) < _Speed * delta)
            {
                path = path.Where((source, index) => index != 0).ToArray();
            }
        }
        else if(Velocity != Vector2.Zero)
        {
            Velocity = Vector2.Zero;
        }
        MoveAndSlide();
    }

    public override void _Ready()
    {
        GD.Print("Pawn Ready");
    }

    // public override void _PhysicsProcess(double delta)
    // {
    //     double X = Input.GetAxis("ui_left", "ui_right");
    //     double Y = Input.GetAxis("ui_up", "ui_down");
    //     Velocity = new Vector2((float)(X*delta*_Speed), (float)(Y*delta*_Speed));
    //     MoveAndSlide();

    // }

}
