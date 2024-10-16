using Godot;

[GlobalClass]
public partial class ProductionRecipe : CraftingRecipe
{
    [Export] public new Godot.Collections.Array<int> Products;

    public ProductionRecipe() : this("") {}

    public ProductionRecipe(string name) : base(name)
    {
    }
}