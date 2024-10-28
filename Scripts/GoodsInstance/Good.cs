using System;
using Godot;

public partial class Good : Resource
{
    public int Id;
    public int Amount;
    public DateTime? Expiration;
    public Food FoodComponent;
    public Fuel FuelComponent;

    public Good(int id, int amount, DateTime? expiration, Food food = null, Fuel fuel = null)
    {
        Id = id;
        Amount = amount;
        Expiration = expiration;
        FoodComponent = food;
        FuelComponent = fuel;
    }
}