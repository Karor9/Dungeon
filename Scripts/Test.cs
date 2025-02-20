using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class Test : Node
{
	[Export] FastNoiseLite fastNoiseLite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		List<int> x = new List<int> {0, 0, 0, 0};
		string xy = "";
		for (int i = 0; i < 1000; i++)
		{
			for (int j = 0; j < 100; j++)
			{
				float val = fastNoiseLite.GetNoise2D(i, j);
				xy += val.ToString() + "\n";
				// if(val < -0.25)
				// 	x[0] += 1;
				// else if(val >= -0.25 && val < 0)
				// 	x[1] += 1;
				// else if(val >= 0 && val < 0.25)
				// 	x[2] += 1;
				// else if(val >= 0.25)
				// 	x[3] += 1;
				
			}
		}
	


		File.WriteAllText("C:/Users/gersk/Desktop/Gra/dungeon/Dungeon/ConceptArt/test2.txt", xy);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
