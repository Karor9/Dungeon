using Godot;

[GlobalClass]
public partial class RecipeBase : Resource
{
    [Export] public string Name {get; private set;}

    public RecipeBase() : this("") {}

    public RecipeBase(string name)
    {
        Name = name;
    }
}