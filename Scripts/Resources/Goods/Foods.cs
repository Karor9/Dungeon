using Godot;

[GlobalClass]
public partial class Foods : Resource
{
    [Export] public double NutritionValue;
    [Export] public double HydrationValue;

    public Foods() : this(0d, 0d) {}

    public Foods(double nv, double hv)
    {
        NutritionValue = nv;
        HydrationValue = hv;
    }
}