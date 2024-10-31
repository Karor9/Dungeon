using Godot;
using System;

[GlobalClass]
public partial class Biome : Resource
{
    [Export] public string Name;
    [Export] public float WaterPercentage;
    [Export] public float MountainPercentage;
    [Export] public float ForestPercentage;
    [Export] public float PlainsPercentage;


    public Biome() : this("", 0, 0, 0, 1) {}

    public Biome(string n, float w, float m, float f, float p)
    {
        Name = n;
        WaterPercentage = w;
        MountainPercentage = m;
        ForestPercentage = f;
        PlainsPercentage = p;
    }
}
