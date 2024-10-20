using Godot;

[GlobalClass]
public partial class Fuel : Resource
{
    [Export] public double EnergyTime;

    public Fuel() : this(0d) {}

    public Fuel(double energy)
    {
        EnergyTime = energy;
    }
}
