using Godot;

[GlobalClass]
public partial class JobSkill : Resource
{
    [Export] public string Name {get; private set;}

    public JobSkill() : this("") {}

    public JobSkill(string name)
    {
        Name = name;
    }
}