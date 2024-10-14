using System.Collections.Generic;
using System.Linq;
using Godot;

public class BuildedBuildings
{
    public int Id {get; set;}    
    public List<int> EffectiveSkills {get; private set;}
    public int MaxHumansInBuilding;

    public bool Destroyed = false;

    public BuildedBuildings(int id, Building building)
    {
        Id = id;
        EffectiveSkills = building.EffectiveSkills.ToList();
        MaxHumansInBuilding = building.MaxHumansInBuilding;
    }
}