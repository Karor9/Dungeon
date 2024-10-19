using Godot;
using System;

public partial class BuildingPanel : Panel
{
	[Export] Container buildingPanel;
	[Export] Button buildingButton;
	[Export] Container ownedPanel;
	[Export] Button ownedButton;

	void OpenBuildingPanel(bool show)
	{
		buildingPanel.Visible = show;
		buildingButton.Disabled = show;

		ownedPanel.Visible = !show;
		ownedButton.Disabled = !show;
	}
}
