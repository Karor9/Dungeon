using Godot;
using System;
using System.Collections.Generic;

public partial class SetipUI : CanvasLayer
{
	[ExportCategory("HeroesPanel")]
	[Export] PackedScene heroButton;
	[Export] VBoxContainer heroBox;



	public override void _Ready()
	{
        for (int i = 0; i < 5; i++)
        {
            Utils.GenerateRandomPerson(new string[] {"Reynald", "Junia", "Paracelsus", "Audrey", "Tardif", "Boudica", "Alhazred", "Baldwin", "Barristan"});
        }

		SetupUIElement<Human, Button>(Globals.Instance.GetHeroes(), heroButton, heroBox,
		(hero) => hero.Name + "\n" + hero.GetClassName()); //TBD Show Hero Script
		GD.Print(Globals.Instance.GetClasses().Count);
	}

	void SetupUIElement<TItem, TNode>(List<TItem> items, PackedScene sceneToInit, Container container,
	Func<TItem, string> getText, Action<int> onPressed = null, Texture2D iconButton = null) where TNode : Control
	{
		for (int i = 0; i < items.Count; i++)
		{
			Node node = sceneToInit.Instantiate();
			TNode element = (TNode)node;
			element.Name = i.ToString();

			switch(element)
			{
				case Button button:
					button.Text = getText(items[i]);
					if(onPressed != null)
					{
						int id = i;
						button.Pressed += () => onPressed(id);
					}
					break;
				case Panel panel:
					RichTextLabel jobName = (RichTextLabel)panel.GetChild(0);
					jobName.Text = getText(items[i]);
					break;
				default:
					GD.PrintErr($"ERROR 1: Type not supported: {element.GetType()}");
					break;
			}
			container.AddChild(element);
		}
	}
}
