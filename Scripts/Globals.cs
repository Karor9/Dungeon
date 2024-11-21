using System;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class Globals : Node
{
	[Export] Array<Goods> Goods;

	public static Globals Instance {get; private set;}

	public DateTime Today = new DateTime(1430, 7, 12, 8, 0, 0);

	public int ItemClicked = -1;
	[Export] public Dictionary<int, MovablePawn> PawnRegistered = new Dictionary<int, MovablePawn>();
	[Export] public Dictionary<int, Pawn> Pawns = new Dictionary<int, Pawn>();

	public override void _Ready()
	{
		Instance = this;
	}
	public void RegisterPawn(int key, MovablePawn pawn)
	{
		PawnRegistered[key] = pawn;
		
        Sprite2D sprite = pawn.GetChild(0) as Sprite2D;
        sprite.Modulate = Colors.Red;
		pawn.Active = true;
	}

	public void DeregisterPawns(int key)
	{
		if(PawnRegistered.Count <= 0)
			return;
		foreach (int item in PawnRegistered.Keys)
		{
			if(item != key)
			{
				MovablePawn pawn = PawnRegistered[item];
				Sprite2D sprite = pawn.GetChild(0) as Sprite2D;
				pawn.Active = false;
				sprite.Modulate = Colors.White;
				PawnRegistered.Remove(item);
			}
		}
	}

	public void AddPawn(Pawn pawn)
	{
		if(Pawns.Keys.Count <= 0)
        {
			Pawns[0] = pawn;
            return;
        }
		int key = Pawns.Keys.Max() + 1;
		Pawns[key] = pawn;
	}

#nullable enable
	public Array<Goods>? GetGoods()
	{
		return Goods;
	}

	public Goods GetGood(int id)
	{
		return Goods[id];
	}
#nullable disable

}
