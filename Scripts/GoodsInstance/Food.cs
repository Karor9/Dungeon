using Godot;

public partial class Food : Resource
{
    public double Nutrition;

    public Food(double nutrition)
    {
        Nutrition = nutrition;
    }
}