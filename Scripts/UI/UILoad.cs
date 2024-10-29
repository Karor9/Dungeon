using System;
using Godot;
using Godot.Collections;

public partial class UILoad : CanvasLayer
{
    [ExportCategory("Goods")]
    [Export] PackedScene _GoodPanel;
    [Export] Container _GoodContainer;

    public override void _Ready()
    {
        LoadUIElement();
    }

    void LoadUIElement()
    {
        Array<Goods> goods = Globals.Instance.GetGoods();
        if(goods == null)
        {
            GD.PrintErr("[Error #0001] Globals can't be null");
            return;
        }
        for (int i = 0; i < Globals.Instance.GetGoods().Count; i++)
        {
            Node g = _GoodPanel.Instantiate();
            Panel good = (Panel)g;
            RichTextLabel label = good.GetChild(1) as RichTextLabel;
            double amount = Math.Round(StorageSystem.Instance.GetAmountOfGood(i), 2);
            label.Text = "[center]" + Globals.Instance.GetGoods()[i].Name + "\n" + amount;

            _GoodContainer.AddChild(good);
        }
    }
}
