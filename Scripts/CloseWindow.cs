using Godot;
using System;

public partial class CloseWindow : Button
{
	[Export] CanvasItem window;
	
	public void Close()
	{
		window.Visible = false;
	}
}
