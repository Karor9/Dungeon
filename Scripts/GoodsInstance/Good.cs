using System;
using Godot;

public class Good
{
    public int Id;
    public int Amount;
    public DateTime Expiration;

    public Good(int id, int amount, DateTime expiration)
    {
        Id = id;
        Amount = amount;
        Expiration = expiration;
    }
}