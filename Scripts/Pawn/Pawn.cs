using Godot;

public partial class Pawn : Resource
{
    string Name;
    bool Sex; //false - woman, true - man;

    public Pawn(string name, bool sex)
    {
        Name = name;
        Sex = sex;
    }
}