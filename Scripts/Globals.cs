using System;
using Godot;
using Godot.Collections;

public partial class Globals : Node
{
	[Export] Array<Goods> Goods;

	public static Globals Instance {get; private set;}

	public DateTime Today = new DateTime(1430, 7, 12, 8, 0, 0);

	public int ItemClicked = -1;
	public Dictionary<int, MovablePawn> PawnRegister = new Dictionary<int, MovablePawn>();

	public override void _Ready()
	{
		Instance = this;
	}
	public int RegisterPawn(int key, MovablePawn pawn)
	{
		return -1;
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
