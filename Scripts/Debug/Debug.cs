using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Debug : Node
{

	[Export] Godot.Collections.Array<Test> test = new Godot.Collections.Array<Test>();
	public override void _Ready()
	{
        var xx = Task.Run(async delegate
            {
                await Task.Delay(1000);
            });
		for (int i = 0; i < 1000000; i++)
		{
            var x = Task.Run(async delegate
            {
                await Task.Delay(10);
            });
			Test t = new Test(i, new DateTime(1400, 12, 12, 10, 10, 10), 100d, 100d);
			test.Add(t);
		}
	}

	public override void _Input(InputEvent @event)
	{
	}
}

public partial class Test : Resource
{
	public int id;
	public DateTime rotTime;
	public double EnergyTime;
	public double FuelTIme;

	public Test(int i, DateTime rotTime, double e, double f)
	{
		id = i;
		this.rotTime = rotTime;
		EnergyTime = e;
		FuelTIme = f;
	}
}
