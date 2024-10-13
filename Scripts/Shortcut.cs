using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Shortcut : Control
{
	
	[Export] CanvasItem[] window;

	void Close()
	{
		List<CanvasItem> order = window.ToList();
		order.Reverse();
		for (int i = 0; i < order.Count; i++)
		{
			if(order[i].Visible)
			{
				order[i].Visible = false;
				return;
			}
		}
	}

	

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("ui_cancel"))
			Close();
    }
}
