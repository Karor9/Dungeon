using Godot;
using System;

public partial class ClickableItem : Area2D
{
    public void Input(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if(@event is InputEventMouseButton button && button.ButtonIndex == MouseButton.Right && @event.IsPressed())
        {
            Globals.Instance.ItemClicked = int.TryParse(Name.ToString().Split("_")[0], out var result) ? result : -1;
        }
    }

    
}
