using Godot;
using System;
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
				yAtlas = OceanTemperature(tempVal);
				break;
			case float f when f > -0.18:
				xAtlas = 0;
				tempVal = temperature.GetNoise2D(x, y);
				yAtlas = ShelfTemperature(tempVal);
				break;
		}

		if(xAtlas > -1 && yAtlas > -1)
			SetCell(new Vector2I(x, y), 0, new Vector2I(xAtlas, yAtlas));
    }

    private int ShelfTemperature(float tempVal)
    {
        switch(tempVal)
		{
			case float f when f <= -0.34:
				return 0;
			case float f when f > -0.34 && f <= -0.11:
				return 1;
			case float f when f > -0.11 && f <= 0.11:
				return 2;
			case float f when f > 0.11 && f <= 0.34:
				return 3;
			case float f when f > 0.34:
				return 4;
			default:
				return -1;
		}
    }


    private int OceanTemperature(float tempVal)
    {
        switch(tempVal)
		{
			case float f when f <= -0.18:
				return 0;
			case float f when f > -0.18 && f <= 0.18:
				return 1;
			case float f when f > 0.18:
				return 2;
			default:
				return -1;
		}
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
				xAtlas = BorealHumidity(humVal);
				break;
			case float f when f > -0.08 && f <= 0.08:
				yAtlas = 3;
				xAtlas = CoolHumidity(humVal);
				break;
			case float f when f > 0.08 && f <= 0.24:
				yAtlas = 4;
				xAtlas = WarmHumidity(humVal);
				break;
			case float f when f > 0.24 && f <= 0.41:
				yAtlas = 5;
				xAtlas = WarmHumidity(humVal);
				break;
			case float f when f > 0.41:
				yAtlas = 6;
				xAtlas = TropicalHumidity(humVal);
				break;
		}

		if(xAtlas > -1 && yAtlas > -1)
			SetCell(new Vector2I(x, y), 1, new Vector2I(xAtlas, yAtlas));
    }

    private int WarmHumidity(float humVal)
    {
        switch(humVal)
		{
			case float f when f <= -0.41:
				return 0;
			case float f when f > -0.41 && f <= -0.24:
				return 1;
			case float f when f > -0.24 && f <= -0.08:
				return 2;
			case float f when f > -0.08 && f <= 0.08:
				return 3;
			case float f when f > 0.08 && f <= 0.24:
				return 4;
			case float f when f > 0.24 && f <= 0.41:
				return 5;
			case float f when f > 0.41:
				return 6;
			default:
				return -1;
		}
    }

	private int TropicalHumidity(float humVal)
    {
        switch(humVal)
		{
			case float f when f <= -0.43:
				return 0;
			case float f when f > -0.43 && f <= -0.28:
				return 1;
			case float f when f > -0.28 && f <= -0.14:
				return 2;
			case float f when f > -0.14 && f <= 0:
				return 3;
			case float f when f > 0 && f <= 0.14:
				return 4;
			case float f when f > 0.14 && f <= 0.28:
				return 5;
			case float f when f > 0.28 && f <= 0.43:
				return 6;
			case float f when f > 0.43:
				return 7;
			default:
				return -1;
		}
    }


    private int CoolHumidity(float humVal)
    {
        switch(humVal)
		{
			case float f when f <= -0.38:
				return 0;
			case float f when f > -0.38 && f <= -0.18:
				return 1;
			case float f when f > -0.18 && f <= 0:
				return 2;
			case float f when f > 0 && f <= 0.18:
				return 3;
			case float f when f > 0.18 && f <= 0.38:
				return 4;
			case float f when f > 0.38:
				return 5;
			default:
				return -1;
		}
    }


    private int BorealHumidity(float humVal)
    {
        switch(humVal)
		{
			case float f when f <= -0.34:
				return 0;
			case float f when f > -0.34 && f <= -0.11:
				return 1;
			case float f when f > -0.11 && f <= 0.11:
				return 2;
			case float f when f > 0.11 && f <= 0.34:
				return 3;
			case float f when f > 0.34:
				return 4;
			default:
				return -1;
		}
    }
}
