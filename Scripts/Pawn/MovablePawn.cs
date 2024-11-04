using Godot;
using System;

public partial class MovablePawn : CharacterBody2D
{
    float _Speed = 5000f;
    public override void _PhysicsProcess(double delta)
    {
        double X = Input.GetAxis("ui_left", "ui_right");
        double Y = Input.GetAxis("ui_up", "ui_down");
        Velocity = new Vector2((float)(X*delta*_Speed), (float)(Y*delta*_Speed));
        MoveAndSlide();
        
    }
}
