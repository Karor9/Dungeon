using System;
using System.ComponentModel.DataAnnotations;
using Godot;

public class Human
{
    public string Name {get; private set;}
    public bool IsMale {get; private set;}
    public DateOnly Birthday {get; private set;}
    public DateOnly? Deathday {get; private set;} = null;
    public double[] JobStats {get; private set;}
    public int[] Stats {get; private set;}
    public byte[] ClassAlign {get; private set;}



    public Human(string name, bool isMale, int day, int month, int year)
    {
        Name = name;
        IsMale = isMale;
        SetupBirthday(day, month, year);
    }

    private void SetupBirthday(int day, int month, int year)
    {
        int m = Math.Max(1, month);
        m = Math.Min(12, m);
        int d = Math.Max(1, day);
        d = Math.Min(DateTime.DaysInMonth(year, m), d);
        Birthday = new DateOnly(year, m, d);
    }

    public bool IsAlive()
    {
        if(Deathday != null)
            return true;
        return false;
    }

}