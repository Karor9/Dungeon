using Godot;

[GlobalClass]
public partial class CraftingRecipe : Resource
{
    [Export] public string Name {get; private set;}
    [Export] public Godot.Collections.Dictionary<int, int> Products;
    [Export] public Godot.Collections.Dictionary<int, int> Results;

    public CraftingRecipe() : this("") {}

    public CraftingRecipe(string name)
    {
        Name = name;
    }
}
