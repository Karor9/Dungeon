using Godot;
using System;

public partial class CloseWindow : Button
{
	[Export] CanvasItem window;
	[Export] CanvasItem[] toClose;
	
	void Close()
	{
		window.Visible = false;
	}

	void Open()
	{
		window.Visible = true;
		foreach (CanvasItem item in toClose)
		{
			item.Visible = false;
		}
	}
}
