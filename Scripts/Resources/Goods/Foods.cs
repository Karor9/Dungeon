using Godot;

[GlobalClass]
public partial class Foods : Resource
{
    [Export] public double NutritionValue;

    public Foods() : this(0d) {}

    public Foods(double nv)
    {
        NutritionValue = nv;
    }
}