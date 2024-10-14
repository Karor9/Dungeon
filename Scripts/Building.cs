using Godot;

[GlobalClass]
public partial class Building : Resource
{
    [Export] public string Name {get; private set;}
    [Export] public Godot.Collections.Dictionary<int, int> CostToBuild {get; private set;}
    [Export] public Godot.Collections.Array<int> EffectiveSkills {get; private set;}
    [Export] public int MaxHumansInBuilding;
    [Export] public Godot.Collections.Array<CraftingRecipe> CraftingRecipes {get; private set;}

    public Building() : this("") {}

    public Building(string name)
    {
        Name = name;
    }
}