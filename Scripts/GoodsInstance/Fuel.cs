using Godot;

public class Fuel
{
    [Export] public double EnergyTime;

    public Fuel(double energy)
    {
        EnergyTime = energy;
    }
}