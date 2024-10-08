using Godot;
using System;
using System.Linq;

public class Human
{
    public string Name {get; private set;}
    public DateTime Birth {get; private set;}
    public int ClassId {get; private set;}
    public int[] Stats {get; private set;}

    public Human(string name, int classId)
    {
        Name = name;
        ClassId = classId;
        SetStats();
        SetBirthday();
    }

    private void SetStats()
    {
        Stats = Enumerable.Repeat(0, 6).ToArray();
        for (int i = 0; i < 6; i++)
        {
            int s = Utils.NormalRandomNumber(0, 20);
            Stats[i] = s;
        }
    }

    private void SetBirthday()
    {
        int year = Utils.NormalRandomNumber(1380, 1418);
        int month = GD.RandRange(1, 12);
        int maxDays = DateTime.DaysInMonth(year, month);
        int day = GD.RandRange(1, maxDays);

        Birth = new DateTime(year, month, day);
    }
}
