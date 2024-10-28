using Godot;

public partial class Fuel : Resource
{
    [Export] public double EnergyTime;

    public Fuel(double energy)
    {
        EnergyTime = energy;
    }
}