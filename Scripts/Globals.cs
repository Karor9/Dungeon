using Godot;
using System;
using System.Collections.Generic;

public partial class Globals : Node
{
    public static Globals Instance {get; private set;}
    DateTime currentDate = new DateTime(1436, 5, 1);
    List<Human> Heroes = new List<Human>();
    [Export] Godot.Collections.Array<HumanClass> Classes = new Godot.Collections.Array<HumanClass>();
    
    public override void _Ready()
    {
        Instance = this;
        GD.Print(Classes.Count);
    }

    public void AddHero(Human human)
    {
        Heroes.Add(human);
    }

    public Godot.Collections.Array<HumanClass> GetClasses()
    {
        return Classes;
    }

    public HumanClass GetHumanClass(int id)
    {
        if(id > -1 && id < Classes.Count)
            return Classes[id];
        return null;
    }

    public List<Human> GetHeroes()
    {
        return Heroes;
    }

    public DateTime GetDate()
    {
        return currentDate;
    }
}