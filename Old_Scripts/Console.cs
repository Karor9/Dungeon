using System.Collections.Generic;
using System.Linq;
using Godot;
using System.Text.RegularExpressions;

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
					if(inputText.HasFocus())
						inputText.ReleaseFocus();
					if (console.Visible)
					{
						Engine.TimeScale = 1f;
						console.Visible = false;
						inputText.Text = "";
					}
					else
					{
						Engine.TimeScale = 0f;
						console.Visible = true;
						inputText.GrabFocus();
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
				int count = 0;
				if(commandsPart.Count == 1)
				{
					count = 1000;
				}
				else if(commandsPart.Count >= 2)
				{
					count = int.TryParse(commandsPart[1], out var parsedCount) ? parsedCount : 0;
				}
				Globals.Instance.AddGoods(count);
				result = $"Added [/color][color=blue]{count}[/color][color=red] goods!";
				break;
			case "hero":
				int id = 0;
				Human h = null;
				string name = "";
				switch(commandsPart.Count)
				{
					case 1:
						(h, id) = Utils.AddRandomPerson(new string[]{"Andriej", "Janush"});
						break;
					case 2:
						if(commandsPart[1] == "-h")
							result = $"Usage of command\n hero [name] [classId]";
						else
							(h, id) = Utils.AddRandomPerson(new string[]{Utils.ToUpper(commandsPart[1])});
						break;
					case 3:
						name = Utils.ToUpper(commandsPart[1]);
						id = int.TryParse(commandsPart[2], out var parsed) ? parsed : 0;
						h = new Human(name, id);
						break;
					default:
						result = $"Usage of command\n hero [name] [classId]";
						break;
				}
				if(h != null)
				{
					Globals.Instance.AddHero(h, id);
					result = $"Added Hero[/color][color=lime] {h.Name} [/color][color=red] with class [/color][color=lime] {h.GetClassName()} [/color][color=red]";
				}
				break;
			case "setstat":
				switch(commandsPart.Count)
				{
					case 2:
						if(commandsPart[1] == "-h")
							result = $"Usage of command\n setstat [heroId] [statId] [value]";
						break;
					case 4:
						int heroId = int.TryParse(commandsPart[1], out var res) ? res : -1;
						if(heroId <= -1)
						{
							result = $"Usage of command\n setstat [heroId] [statId] [value]";
							break;
						}
						int statId = int.TryParse(commandsPart[2], out res) ? res : -1;
						if(statId <= -1 || statId >= 6)
						{
							result = $"Usage of command\n setstat [heroId] [statId] [value]";
							break;
						}
						int statValue = int.TryParse(commandsPart[3], out res) ? res : -1;
						if(statValue <= -1)
						{
							result = $"Usage of command\n setstat [heroId] [statId] [value]";
							break;
						}
						Globals.Instance.ChangeStats(heroId, statId, statValue);
						result = $"Changed Stat [/color][color=lime] {statId} [/color][color=red] of [/color][color=lime] {Globals.Instance.GetHero(heroId).Name} [/color][color=red] to [/color][color=lime] {statValue} [/color][color=red]";
				
						break;
					default:
						result = $"Usage of command\n setstat [heroId] [statId] [value]";
						break;
				}
				break;
			case "kill":
				int deletedHero;
				switch(commandsPart.Count)
				{
					case 2:
						if(commandsPart[1] == "-h")
							result = $"Usage of command\n kill [heroId]";
						else
						{
							deletedHero = int.TryParse(commandsPart[1], out var res) ? res : -1;
							if(deletedHero >= -1)
							{
								result = $"Killed [/color][color=lime] {Globals.Instance.GetHero(deletedHero).Name} [/color][color=red]";
								Globals.Instance.KillHero(deletedHero);
							}

						}
						break;
					default:
						result = $"Usage of command\n kill [heroId]";
						break;
				}
				break;
			default:
				result = "Unknown command!";
				break;
		}
		return result;
	}

	public void ValidateText(string newText)
	{
		string clennedText = RegexConsole(newText);
		if(newText != clennedText)
			inputText.Text = clennedText;
	}

	string RegexConsole(string input)
	{
		Regex regex = new Regex(@"[`~]");
		return regex.Replace(input, "");
	}
}
