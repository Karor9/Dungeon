using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SetupUI : CanvasLayer
{
	[ExportCategory("HeroesPanel")]
	[Export] PackedScene heroButton;
	[Export] VBoxContainer heroBox;
	[Export] Control heroPanel;
	[Export] Control statsPanel;

	[ExportCategory("GoodsPanel")]
	[Export] PackedScene goodsButton;
	[Export] VBoxContainer goodsBox;

	[ExportCategory("Buildings")]
	[Export] PackedScene buildingButton;
	[Export] Container buildingBox;
	[Export] Control buildingMonit;
	[Export] CanvasItem buildingPanel;

	[ExportCategory("BuildedBuildings")]
	[Export] PackedScene buildedButton;
	[Export] Container buildedBox;

	[ExportCategory("JobSkills")]
	[Export] PackedScene jobSkillButton;
	[Export] Container jobSkillBox;

	int ChoosenBuildingToBuild = -1;
	public override void _Ready()
	{
        for (int i = 0; i < 5; i++)
        {
            Utils.GenerateRandomPerson(new string[] {"Reynald", "Junia", "Paracelsus", "Audrey", "Tardif", "Boudica", "Alhazred", "Baldwin", "Barristan"});
        }

		SetupUIElement<Human, Button>(Globals.Instance.GetHeroes(), heroButton, heroBox,
		(hero) => hero.Name + "\n" + hero.GetClassName(),
		(id) => ChooseHero(id));

		SetupUIElement<Goods, Button>(Globals.Instance.GetGoods().ToList(), goodsButton, goodsBox,
		(good) => good.Name + "\n" + good.Count);

		SetupUIElement<Building, Button>(Globals.Instance.GetBuildings().ToList(), buildingButton, buildingBox,
		(building) => building.Name,
		(id) => ChooseBuilding(id));

		SetupUIElement<int, Button>(Globals.Instance.GetBuildedBuildings().Keys.ToList(), buildedButton, buildedBox,
		(building) => Globals.Instance.GetBuilding(building).Name + "\n[Builded]: " +
					Globals.Instance.GetBuildedBuildingCount(building).ToString());

		SetupUIElement<JobSkill, Panel>(Globals.Instance.GetJobSkills().ToList(), jobSkillButton, jobSkillBox,
		(skill) => skill.Name);
	}

    private void ChooseHero(int id)
    {
        Human hero = Globals.Instance.GetHero(id);
		heroPanel.Visible = true;
		RichTextLabel name = (RichTextLabel)heroPanel.GetChild(0).GetChild(0);
		name.Text = hero.Name;
		RichTextLabel className = (RichTextLabel)heroPanel.GetChild(1).GetChild(0);
		className.Text = hero.GetClassName();
		RichTextLabel age = (RichTextLabel)heroPanel.GetChild(2).GetChild(0);
		age.Text = hero.GetAge().ToString();
		SetupStats(hero.Stats);
		SetupJobSkills(hero);
		buildingPanel.Visible = false;
    }

	void SetupStats(int[] stats)
	{
		for (int i = 0; i < stats.Length; i++)
		{
			RichTextLabel statValue = (RichTextLabel)statsPanel.GetChild(i).GetChild(1);
			statValue.Text = stats[i].ToString();
		}
	}

    public void AddHeroUIElement(Human hero, int id)
	{
		if(!hero.IsAlive)
			return;
		Node node = heroButton.Instantiate();
		Button button = (Button)node;
		button.Name = id.ToString();
		button.Text = hero.Name + "\n" + hero.GetClassName();

		button.Pressed += () => ChooseHero(id);
		heroBox.AddChild(button);
	}

	public void DeleteHero(int id)
	{
		Node node = heroBox.GetNode(id.ToString());
		node.QueueFree();
	}


	void SetupUIElement<TItem, TNode>(List<TItem> items, PackedScene sceneToInit, Container container,
	Func<TItem, string> getText, Action<int> onPressed = null, Texture2D iconButton = null) where TNode : Control
	{
		for (int i = 0; i < items.Count; i++)
		{
			Node node = sceneToInit.Instantiate();
			TNode element = (TNode)node;
			element.Name = i.ToString();
			TItem item = items[i];
			bool isAlive = true;
			switch(element)
			{
				case Button button:
					if(item is Human hero)
					{
						isAlive = hero.IsAlive;
					}
					if(isAlive)
					{
						button.Text = getText(item);
						if(onPressed != null)
						{
							int id = i;
							button.Pressed += () => onPressed(id);
						}
					}
					break;
				case Panel panel:
					RichTextLabel jobName = (RichTextLabel)panel.GetChild(0);
					jobName.Text = getText(item);
					break;
				default:
					GD.PrintErr($"ERROR 1: Type not supported: {element.GetType()}");
					break;
			}
			if(isAlive)
				container.AddChild(element);
		}
	}

	public void ChangeGoodText(int i)
	{
		Button label = (Button)goodsBox.GetChild(i);
		string val = label.Text;
		val = val.Split("\n")[0];
		val += "\n" + Globals.Instance.GetGood(i).Count.ToString();
		label.Text = val;
	}

	public void ChangeCountGoods()
	{
		for (int i = 0; i < Globals.Instance.GetGoods().Count; i++)
		{
			ChangeGoodText(i);
		}
	}

	void ChooseBuilding(int id)
	{
		bool dontHaveProducts = false;
		ShowHideMonit(true);
		RichTextLabel text = (RichTextLabel)buildingMonit.GetChild(3);
		Building building = Globals.Instance.GetBuilding(id);
		string formattedText = "[BuildRequest]" + building.Name + "?\n";
		List<int> keys = building.CostToBuild.Keys.ToList();
		
		for (int i = 0; i < keys.Count; i++)
		{
			formattedText += Globals.Instance.GetGood(keys[i]).Name + ":\n";
		}

		text.Text = formattedText;

		RichTextLabel demand = (RichTextLabel)buildingMonit.GetChild(4);
		string demandText = "[right]\n";
		double h = 0;
		double d = 0;
		for (int i = 0; i < keys.Count; i++)
		{
			h = Globals.Instance.GetGood(keys[i]).Count;
			d = building.CostToBuild[keys[i]];
			if (h > d)
				demandText += "[color=green]";
			else{
				demandText += "[color=red]";
				dontHaveProducts = true;
			}
			demandText += h + "/" + d + "[/color]\n";
		}
		demandText += "[/right]";
		demand.Text = demandText;
		
		Button b = (Button)buildingMonit.GetChild(0);
		ChoosenBuildingToBuild = id;
		b.Disabled = dontHaveProducts;
	}

	void ShowHideMonit(bool show)
	{
		buildingMonit.Visible = show;
	}

	public void BuildBuilding()
	{
		if(ChoosenBuildingToBuild <= -1)
			return;
		Building building = Globals.Instance.GetBuilding(ChoosenBuildingToBuild);
		foreach (int item in building.CostToBuild.Keys)
		{
			Globals.Instance.AddGood(item, -1 * building.CostToBuild[item]);
		}

		int count = Globals.Instance.AddBuilding(ChoosenBuildingToBuild, 1);
		AddBuildedBuilding(count, ChoosenBuildingToBuild);
		ChoosenBuildingToBuild = -1;
		ShowHideMonit(false);
	}

	void AddBuildedBuilding(int count, int id)
	{
		if(count > 1)
		{
			Button label = (Button)buildedBox.GetNode(id.ToString());
			string val = label.Text;
			val = val.Split(" ")[0];
			val += " " + Globals.Instance.GetBuildedBuildingCount(id).ToString();
			label.Text = val;
		} else
		{
			Node n = buildedButton.Instantiate();
			Button button = (Button)n;
			button.Name = id.ToString();
			string val = Globals.Instance.GetBuilding(id).Name + "\n[Builded]: " +
						Globals.Instance.GetBuildedBuildingCount(id).ToString();
			button.Text = val;
			buildedBox.AddChild(n);
		}
	}

	void SetupJobSkills(Human h)
	{
		double[] jobSkills = h.JobStats;
		for (int i = 0; i < jobSkills.Length; i++)
		{
			RichTextLabel jobStatName = (RichTextLabel)jobSkillBox.GetChild(i).GetChild(1);
			double val = jobSkills[i];
			int skillVal = (int)val;
			jobStatName.Text = skillVal.ToString();

			ProgressBar progress = (ProgressBar)jobSkillBox.GetChild(i).GetChild(2);
			progress.Value = val - skillVal;
		}
	}
}
