using Godot;

[GlobalClass]
public partial class Fuels : Resource
{
    [Export] public double EnergyTime;

    public Fuels() : this(0d) {}

    public Fuels(double energy)
    {
        EnergyTime = energy;
    }
}
