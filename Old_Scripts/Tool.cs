using Godot;

public class Tool
{
    int Id = -1;
    double Durability = -1;

    public Tool(int id, double durability)
    {
        Id = id;
        Durability = durability;
    }

    public int GetId()
    {
        return Id;
    }

    public double GetDurability()
    {
        return Durability;
    }

    public double ChangeDurability(double change)
    {
        Durability += change;
        return Durability;
    }

}