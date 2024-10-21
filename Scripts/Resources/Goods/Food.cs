using Godot;

[GlobalClass]
public partial class Food : Resource
{
    [Export] public double NutritionValue;

    public Food() : this(0d) {}

    public Food(double nv)
    {
        NutritionValue = nv;
    }
}