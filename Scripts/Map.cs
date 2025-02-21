using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public partial class Map : TileMapLayer
{
	int xSize = 300;
	int ySize = 300;
	[Export] FastNoiseLite height = new FastNoiseLite();
	[Export] FastNoiseLite temperature = new FastNoiseLite();
	[Export] FastNoiseLite humidity = new FastNoiseLite();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		height.Seed = (int)GD.Randi();
		temperature.Seed = (int)GD.Randi();
		temperature.Frequency = 0.0001f;
		humidity.Seed = (int)GD.Randi();
		humidity.Frequency = 0.0001f;
		SetupTerrain();
	}

	void SetupTerrain()
	{
		for (int i = 0; i < xSize; i++)
		{
			for (int j = 0; j < ySize; j++)
			{
				float val = height.GetNoise2D(i, j);
				if(val < 0)
					SetupWaterTile(i, j, val);
				else
					SetupLandTile(i, j);
			}
		}
	}

    private void SetupWaterTile(int x, int y, float val)
    {
		int xAtlas = -1;
		int yAtlas = -1;
		float tempVal;
		switch(val)
		{
			case float f when f <= -0.38:
				SetCell(new Vector2I(x, y), 0, new Vector2I(2, 0));
				return;
			case float f when f > -0.38 && f <= -0.18:
				xAtlas = 1;
				tempVal = temperature.GetNoise2D(x, y);
				yAtlas = NoiseClassifier(3, tempVal);
				break;
			case float f when f > -0.18:
				xAtlas = 0;
				tempVal = temperature.GetNoise2D(x, y);
				yAtlas = NoiseClassifier(5, tempVal);
				break;
		}

		if(xAtlas > -1 && yAtlas > -1)
			SetCell(new Vector2I(x, y), 0, new Vector2I(xAtlas, yAtlas));
    }



    private void SetupLandTile(int x, int y)
    {
		int yAtlas = -1;
		int xAtlas = -1;
		float tempVal = temperature.GetNoise2D(x, y);
		float humVal = humidity.GetNoise2D(x, y);
		switch(tempVal)
		{
			case float f when f <= -0.41:
				SetCell(new Vector2I(x, y), 1, new Vector2I(0, 0));
				return;
			case float f when f > -0.41 && f <= -0.24:
				SetCell(new Vector2I(x, y), 1, new Vector2I(0, 1));
				return;
			case float f when f > -0.24 && f <= -0.08:
				yAtlas = 2;
				xAtlas = NoiseClassifier(5, humVal);
				break;
			case float f when f > -0.08 && f <= 0.08:
				yAtlas = 3;
				xAtlas = NoiseClassifier(6, humVal);
				break;
			case float f when f > 0.08 && f <= 0.24:
				yAtlas = 4;
				xAtlas = NoiseClassifier(7, humVal);
				break;
			case float f when f > 0.24 && f <= 0.41:
				yAtlas = 5;
				xAtlas = NoiseClassifier(7, humVal);
				break;
			case float f when f > 0.41:
				yAtlas = 6;
				xAtlas = NoiseClassifier(8, humVal);
				break;
		}

		if(xAtlas > -1 && yAtlas > -1)
			SetCell(new Vector2I(x, y), 1, new Vector2I(xAtlas, yAtlas));
    }


	private int NoiseClassifier(int count, float noiseValue)
	{
		var ranges = new Dictionary<int, float[]>()
		{
			{3, new float[] { -0.18f, 0,18f }},
			{5, new float[] { -0.34f, -0.11f, 0.11f, 0.34f }},
			{6, new float[] { -0.38f, -0.18f, 0f, 0.18f, 0.38f }},
			{7, new float[] { -0.41f, -0.24f, -0.08f, 0.08f, 0.24f, 0.41f }},
			{8, new float[] { -0.43f, -0.28f, -0.14f, 0f, 0.14f, 0.28f, 0.43f }},
		};

		if(!ranges.TryGetValue(count, out var range))
		{
			throw new ArgumentException("Invalid range");
		}

		for (int i = 0; i < range.Length; i++)
		{
			if(noiseValue <= range[i])
				return i;
		}

		return range.Length;
	}
}
