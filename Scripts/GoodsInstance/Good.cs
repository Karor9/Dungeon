using System;
using Godot;

public partial class Good : Resource
{
    [Export] public int Amount;
    [Export] private string _Expiration; //To be deleted after build
    public DateTime? Expiration;
    // [Export] public Food FoodComponent;
    // [Export] public Fuel FuelComponent;

    public Good(int amount, DateTime? expiration = null)
    {
        Amount = amount;
        Expiration = expiration;
        // FoodComponent = food;
        // FuelComponent = fuel;

        SetupDebug();
    }

    private void SetupDebug()
    {
        _Expiration = Expiration.ToString();
    }
}