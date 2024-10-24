using Godot;
using Godot.Collections;

public partial class Globals : Node
{
	[Export] Array<Goods> Goods;

	public static Globals Instance {get; private set;}

	public override void _Ready()
	{
		Instance = this;
	}

#nullable enable
	public Array<Goods>? GetGoods()
	{
		return Goods;
	}
#nullable disable
}
