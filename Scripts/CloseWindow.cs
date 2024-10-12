using Godot;
using System;

public partial class CloseWindow : Button
{
	[Export] CanvasItem window;
	
	void Close()
	{
		window.Visible = false;
	}

	void Open()
	{
		window.Visible = true;
	}
}
