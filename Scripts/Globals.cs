using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class Globals : Node
{
	[Export] Array<Goods> Goods;
	[Export] Array<Good> Storage = new Array<Good>();

	public static Globals Instance {get; private set;}

	public override void _Ready()
	{
		Instance = this;
		Storage.Add(new Good(0, 10, null));
		Storage.Add(new Good(2, 10, new System.DateTime(1400, 12, 12, 12, 12, 12)));
	}

#nullable enable
	public Array<Goods>? GetGoods()
	{
		return Goods;
	}
#nullable disable
}
