using System;
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
        if(Input.IsActionJustPressed("RightMouseClick") && !Input.IsActionJustPressed("append_path"))
        {
            SinglePath();
            if(path.Length == 0)
                ObstacleClick();
        }
        if(path.Length == 0 && Input.IsActionJustPressed("append_path"))
        {
            SinglePath();
            if(path.Length == 0)
                ObstacleClick();
        }
        if(Input.IsActionJustPressed("append_path") && path.Length > 0)
        {
            int pathLength = path.Length;
            AdditionalPath();
            if(path.Length == pathLength)
            {
                AdditionalObstacleClick();

            }
                // return;
        }

        if(path.Length > 0) 
        {
            Vector2 dir = GlobalPosition.DirectionTo(path[0]);
            float terrainDiff = pathfinding.GetTerrainDifficulty((Vector2I)Position / terrain.RenderingQuadrantSize);
            if(terrainDiff == 0)
                terrainDiff = 0.0001f;
            Velocity = dir * _Speed * (1/ terrainDiff);

            if(Position.DistanceTo(path[0]) < _Speed * delta)
            {
                path = path.Where((source, index) => index != 0).ToArray();
                pathfinding.Redraw(path);
            }
            QueueRedraw();
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

    void SinglePath()
    {
        Vector2 pos = Position / terrain.RenderingQuadrantSize;
        Vector2 targetPos = GetGlobalMousePosition() / terrain.RenderingQuadrantSize;
        path = pathfinding.RequestPath((Vector2I)pos, (Vector2I)targetPos);     
    }

    void AdditionalPath()
    {
        Vector2 pos = path[path.Length-1] / terrain.RenderingQuadrantSize;
        Vector2 targetPos = GetGlobalMousePosition() / terrain.RenderingQuadrantSize;
        path = pathfinding.RequestAdditionalPath((Vector2I)pos, (Vector2I)targetPos, path);
    }

    void ObstacleClick()
    {
        Vector2[] offsets = {
            new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1),
            new Vector2(1, 0), new Vector2(1, 1),
            new Vector2(0, 1), new Vector2(-1, 1), new Vector2(-1, 0)
        };
        Vector2 pos = Position / terrain.RenderingQuadrantSize;
        Vector2 obstacle = GetGlobalMousePosition() / terrain.RenderingQuadrantSize;
        int minInd = -1;
        int minVal = int.MaxValue;
        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2[] p = pathfinding.RequestPath((Vector2I)pos, (Vector2I)(obstacle + offsets[i]));
            if(p.Length != 0 && p.Length < minVal )
            {
                minInd = i;
                minVal = p.Length;
            }
        }
        if(minInd != -1)
            path = pathfinding.RequestAdditionalPath((Vector2I)pos, (Vector2I)(obstacle + offsets[minInd]), path);
    }

    void AdditionalObstacleClick()
    {
        Vector2[] offsets = {
            new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1),
            new Vector2(1, 0), new Vector2(1, 1),
            new Vector2(0, 1), new Vector2(-1, 1), new Vector2(-1, 0)
        };
        Vector2 pos = path[path.Length - 1] / terrain.RenderingQuadrantSize;
        Vector2 obstacle = GetGlobalMousePosition() / terrain.RenderingQuadrantSize;
        int minInd = -1;
        int minVal = int.MaxValue;
        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2[] p = pathfinding.RequestSomePath((Vector2I)pos, (Vector2I)(obstacle + offsets[i]));
            if(p.Length != 0 && p.Length < minVal )
            {
                minInd = i;
                minVal = p.Length;
            }
        }
        if(minInd != -1)
            path = pathfinding.RequestAdditionalPath((Vector2I)pos, (Vector2I)(obstacle + offsets[minInd]), path);
    }
}
