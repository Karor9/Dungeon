using Godot;

[GlobalClass]
public partial class ProductionRecipe : RecipeBase
{
    [Export] public Godot.Collections.Array<int> Products;
    [Export] public Godot.Collections.Dictionary<int, int> Results;

    public ProductionRecipe() : this("") {}

    public ProductionRecipe(string name) : base(name)
    {
    }
}