using System;
using Godot;

public partial class Good : Resource
{
    [Export] public int Amount;
    [Export] private string _Expiration; //To be deleted after build
    [Export] public Vector2 Position;
    bool OnGround;
    public DateTime? Expiration;
    // [Export] public Food FoodComponent;
    // [Export] public Fuel FuelComponent;

    public Good(int amount, Vector2 position, DateTime? expiration = null)
    {
        Amount = amount;
        Position = position;
        Expiration = expiration;
        OnGround = true;
        // FoodComponent = food;
        // FuelComponent = fuel;

        SetupDebug();
    }

    private void SetupDebug()
    {
        _Expiration = Expiration.ToString();
    }
}