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

	[ExportCategory("BuildingDetail")]
	[Export] Control buildingDetail;

	[ExportCategory("OwnedBuilding")]
	[ExportGroup("Craftings")]
	[Export] Container craftingBox;
	[Export] PackedScene craftingPanel;
	
	[ExportGroup("Production")]
	[Export] PackedScene productionPanel;

	int ChoosenBuildingToBuild = -1;
	int ChoosenBuildingId = -1;
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
		(good) => good.Name + "\n" + good.Count, 
		getIcon: (good) => good.Icon);

		SetupUIElement<Building, Button>(Globals.Instance.GetBuildings().ToList(), buildingButton, buildingBox,
		(building) => building.Name,
		(id) => ChooseBuilding(id));

		SetupUIElement<BuildedBuildings, Button>(Globals.Instance.GetBuildingList(), buildedButton, buildedBox,
		(building) => Globals.Instance.GetBuilding(building.Id).Name,
		(id) => ChooseBuldingOption(id));

		SetupUIElement<JobSkill, Panel>(Globals.Instance.GetJobSkills().ToList(), jobSkillButton, jobSkillBox,
		(skill) => skill.Name);

		// SetupUIElement<JobSkill, Button>(Globals.Instance.GetJobSkills().ToList(), jobButton, jobBox,
		// (job) => job.Name);
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
	Func<TItem, string> getText, Action<int> onPressed = null, Func<TItem, Texture2D> getIcon = null) where TNode : Control
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
					if(getIcon != null)
					{
						var icon = getIcon(item);
						button.Icon = icon;
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
			if (h >= d)
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
		if(count >= 1)
		{
			Node n = buildedButton.Instantiate();
			Button button = (Button)n;
			button.Name = id.ToString() + "_" + Globals.Instance.GetBuildingList().Count;
			Building building = Globals.Instance.GetBuilding(id);
			string val = building.Name;
			button.Text = val;
			button.Pressed += () => ChooseBuldingOption(id);
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

	void ChooseBuldingOption(int id)
	{
		buildingPanel.Visible = false;
		buildingDetail.Visible = true;

		RichTextLabel label = (RichTextLabel) buildingDetail.GetChild(0).GetChild(0);
		label.Name = id.ToString(); // Pass id to script TBD
		Building building = Globals.Instance.GetBuilding(id);
		string text = $"[center]{Tr(building.Name)}[/center]";
		label.Text = text;
		SetupRecipes(building);
	}

	void SetupRecipes(Building building)
	{
		ClearContainer(craftingBox);
		SetupCrafting(building);
		SetupProduction(building);
	}

	void SetupCrafting(Building building)
	{
		for (int i = 0; i < building.CraftingRecipes.Count; i++)
		{
			Node node = craftingPanel.Instantiate();
			CraftingRecipe item = building.CraftingRecipes[i];
			RichTextLabel label = (RichTextLabel)node.GetChild(0);
			string text = "[center]" + Tr(item.Name) + "[/center]";
			label.Text = text;
			node.Name = "c_" + i.ToString();
			craftingBox.AddChild(node);
		}
		return;
	}

	void SetupProduction(Building building)
	{
		for (int i = 0; i < building.Productions.Count; i++)
		{
			Node node = productionPanel.Instantiate();
			ProductionRecipe item = building.Productions[i];

			//Setup Text
			RichTextLabel label = (RichTextLabel)node.GetChild(0);
			string text = "[center]" + Tr(item.Name) + "[/center]";
			label.Text = text;
			node.Name = "p_" + i.ToString();

			//Setup Button
			// TextureButton worker = (TextureButton)node.GetChild(1).GetChild(0);
			// worker.Pressed += () => AssignWorker();

			//Setup Products
			Godot.Collections.Array<int> prods = building.Productions[i].Products;
			for (int j = 0; j < prods.Count; j++)
			{
				int p = prods[j];
				TextureRect prod = node.GetChild(1).GetChild(j + 1) as TextureRect;
				prod.Texture = Globals.Instance.GetGood(p).Icon;
			}

			//Setup Results
			List<int> results = building.Productions[i].Results.Keys.ToList();
			for (int j = 0; j < results.Count; j++)
			{
				int r = results[j];
				TextureRect res = node.GetChild(1).GetChild(j + 6) as TextureRect;
				res.Texture = Globals.Instance.GetGood(r).Icon;
			}

			//Add to viewport
			craftingBox.AddChild(node);
		}
	}

	void ClearContainer(Container container)
	{
		foreach (var item in container.GetChildren())
		{
			item.QueueFree();
		}
	}

	void AssignWorker()
	{
		GD.Print("TBD");
	}
}
