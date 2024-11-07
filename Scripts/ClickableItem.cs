using Godot;
using System;

public partial class ClickableItem : Area2D
{
    public void Input(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if(@event is InputEventMouseButton button && button.ButtonIndex == MouseButton.Left && @event.IsPressed())
            GD.Print(Name);
    }

    
}
