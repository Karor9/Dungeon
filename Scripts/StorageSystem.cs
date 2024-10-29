using System;
using Godot;
using Godot.Collections;

public partial class StorageSystem : Node
{
    [Export] Dictionary<int, Array<Good>> Storage = new Dictionary<int, Array<Good>>();

    public static StorageSystem Instance;

    public override void _Ready()
    {
        Instance = this;

        
		AddToStorage(0, new Good(35));
		AddToStorage(0, new Good(25));
		AddToStorage(1, new Good(5));
        AddToStorage(3, new Good(25, 
            expiration: Globals.Instance.Today.AddDays(7)));
    }

    public void AddToStorage(int key, Good good)
    {
        if(!Storage.ContainsKey(key))
            Storage[key] = new Array<Good>();
        Storage[key].Add(good);
    }

    public Dictionary<int, Array<Good>> GetStorage()
    {
        return Storage;
    }

    public Array<Good> GetAllOfGood(int id)
    {
        return Storage[id];
    }

    public double GetAmountOfGood(int id)
    {
        if(!Storage.ContainsKey(id))
            return 0;
        double amount = 0d;
        foreach (Good item in Storage[id])
        {
            amount += item.Amount;
        }

        return amount;
    }
}
