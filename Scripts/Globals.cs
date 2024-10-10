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

    public void AddHero(Human h, int id)
    {
        (GetTree().Root.GetChild(2).GetChild(0) as SetupUI).AddHeroUIElement(h, id);
    }
}