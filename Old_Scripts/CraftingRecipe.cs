using Godot;

[GlobalClass]
public partial class CraftingRecipe : RecipeBase
{
    [Export] public Godot.Collections.Dictionary<int, int> Products;
    [Export] public Godot.Collections.Dictionary<int, int> Results;

    public CraftingRecipe() : this("") {}

    public CraftingRecipe(string name) : base(name)
    {

    }
}
