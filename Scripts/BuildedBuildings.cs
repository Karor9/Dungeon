using System.Collections.Generic;
using System.Linq;
using Godot;

public class BuildedBuildings
{
    public int Id {get; set;}    
    public List<int> EffectiveSkills {get; private set;}
    public int MaxHumansInBuilding;

    public bool Destroyed = false;
    int Level = 0;

    List<int> idOfHumans = new List<int>();

    public BuildedBuildings(int id, Building building)
    {
        Id = id;
        EffectiveSkills = building.EffectiveSkills.ToList();
        MaxHumansInBuilding = building.MaxHumansInBuilding;
    }

    public string GetLevelString()
    {
        switch(Level)
        {
            case 0:
                return "[Basic]";
            case 10:
                return "[Max]";
            default:
                return $"[{Level}]";
        }
    }

    public void AddHumanToWork(int id)
    {
        if(MaxHumansInBuilding <= idOfHumans.Count)
            return;
        idOfHumans.Add(id);
    }
}