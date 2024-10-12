using Godot;
using System;
using System.Collections.Generic;

public partial class Globals : Node
{
    public static Globals Instance {get; private set;}
    DateTime currentDate = new DateTime(1436, 5, 1);
    List<Human> Heroes = new List<Human>();
    [Export] Godot.Collections.Array<HumanClass> Classes = new Godot.Collections.Array<HumanClass>();
    [Export] Godot.Collections.Array<Goods> Goods = new Godot.Collections.Array<Goods>();
    [Export] Godot.Collections.Array<Building> Buildings = new Godot.Collections.Array<Building>();
    [Export] Godot.Collections.Dictionary<int, int> BuildedBuildings = new Godot.Collections.Dictionary<int, int>();
    
    public override void _Ready()
    {
        Instance = this;
    }


    public void AddHero(Human human)
    {
        Heroes.Add(human);
    }

    public Godot.Collections.Array<HumanClass> GetClasses()
    {
        return Classes;
    }

    
    public Godot.Collections.Array<Goods> GetGoods()
    {
        return Goods;
    }

    public HumanClass GetHumanClass(int id)
    {
        if(id > -1 && id < Classes.Count)
            return Classes[id];
        return null;
    }

    public Goods GetGood(int id)
    {
        if(id > -1 && id < Goods.Count)
            return Goods[id];
        return null;
    }

    public List<Human> GetHeroes()
    {
        return Heroes;
    }

    public Human GetHero(int id)
    {
        return Heroes[id];
    }

    public void ChangeStats(int heroId, int id, int val)
    {
        Heroes[heroId].Stats[id] = val;
    }

    public DateTime GetDate()
    {
        return currentDate;
    }

    public void AddGoods(int x)
    {
        for (int i = 0; i < Goods.Count; i++)
        {
            Goods[i].Count += x;
        }
        GetTree().Root.GetChild(2).GetChild(0).Call("ChangeCountGoods");
    }

    public void AddGood(int id, int val)
    {
        Goods[id].Count += val;
        (GetTree().Root.GetChild(2).GetChild(0) as SetupUI).ChangeCountGoods();

    }

    public void AddHero(Human h, int id)
    {
        (GetTree().Root.GetChild(2).GetChild(0) as SetupUI).AddHeroUIElement(h, Heroes.Count-1);
    }

    public void KillHero(int id)
    {
        Heroes[id].IsAlive = false;
        (GetTree().Root.GetChild(2).GetChild(0) as SetupUI).DeleteHero(id);
    }

    public Godot.Collections.Array<Building> GetBuildings()
    {
        return Buildings;
    }

    public Building GetBuilding(int x)
    {
        return Buildings[x];
    }

    public int AddBuilding(int key, int value)
    {
        if(BuildedBuildings.ContainsKey(key))
            BuildedBuildings[key] += value;
        else
            BuildedBuildings[key] = value;
        return BuildedBuildings[key];
    }

    public Godot.Collections.Dictionary<int, int> GetBuildedBuildings()
    {
        return BuildedBuildings;
    }

    public int GetBuildedBuildingCount(int id)
    {
        return BuildedBuildings[id];
    }
}