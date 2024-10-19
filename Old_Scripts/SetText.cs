using Godot;
using System;

public partial class SetText : RichTextLabel
{
    [Export] string[] translateInput;
    [Export] string bbCodeStart;
    [Export] string bbCodeEnd;

    public override void _Ready()
    {
        Text = bbCodeStart;
        foreach (string item in translateInput)
        {
            Text += Tr(item);
        }
        Text += bbCodeEnd;
    }
}
