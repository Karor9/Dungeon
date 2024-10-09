using System;
using System.Linq;
using Godot;

public static class Utils
{
    public static int NormalRandomNumber(int from, int to)
    {
        Random rand = new Random();
        double mean = (from + to) / 2.0;
        double stdDev = (to - from) / 6.0;

        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();

        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0*Math.PI*u2);

        double randNormal = mean + stdDev * randStdNormal;

        int result = (int) Math.Round(randNormal);
        return Math.Max(from, Math.Min(to, result));
    }

    public static void GenerateRandomPerson(string[] namesbase = null)
    {
        Human newHero = new Human("[WrongNamebase]", 
                GD.RandRange(0, Globals.Instance.GetClasses().Count - 1));
        if(namesbase != null)
            newHero.ChangeName(namesbase[GD.RandRange(0, namesbase.Length-1)]);
        Globals.Instance.AddHero(newHero);
    }
}