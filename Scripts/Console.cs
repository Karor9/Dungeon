using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Console : Control
{
	[Export] bool _DEBUGMODE = true;
	[Export] CanvasLayer console;
	[Export] Button confirm; 	

	[Export] RichTextLabel history;
	[Export] LineEdit inputText;
    public override void _Input(InputEvent @event)
    {
        if(_DEBUGMODE)
        {
			switch (@event)
			{
				case var _ when @event.IsActionPressed("command"):
					if (console.Visible)
					{
						Engine.TimeScale = 1f;
						console.Visible = false;
					}
					else
					{
						Engine.TimeScale = 0f;
						console.Visible = true;
					}
					break;

				case var _ when @event.IsAction("confirmCommand"):
					PressedConfirm();
					break;
			}
        }
    }

    public override void _Ready()
    {
		confirm.Pressed += () => PressedConfirm();
    }

	void PressedConfirm()
	{
		string command = inputText.Text;
		if(command != "")
		{;
			inputText.Text = "";
			string result = CheckCommand(command);
			
			history.Text += " " + result + "\n";
		}
	}

	string CheckCommand(string command)
	{
		List<string> commandsPart = command.Split(" ").ToList();
		string result = "";
		switch(commandsPart[0])
		{
			case "goods":
				int count = 1000;
				if(commandsPart.Count == 1)
					Globals.Instance.AddGoods(count);
				else if(commandsPart.Count == 2)
				{
					count = int.TryParse(commandsPart[1], out var parsedCount) ? parsedCount : 0;
					Globals.Instance.AddGoods(count); 
				}
				result = $"Added [/color][color=blue]{count}[/color][color=red] goods!";
				break;
			default:
				result = "Unknown command!";
				break;
		}
		return result;
	}
}
