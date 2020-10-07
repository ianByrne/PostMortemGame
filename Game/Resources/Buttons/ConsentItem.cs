using Godot;
using System;

public class ConsentItem : HBoxContainer
{
    [Signal]
    public delegate void CheckboxClicked();

    [Export]
    public string LabelText { get; set; }

    public CheckBox CheckBox { get; private set; }

    public override void _Ready()
    {
        GetNode<Label>("Label").Text = LabelText;
        CheckBox = GetNode<CheckBox>("CheckBox");

        base._Ready();
    }

    private void _OnCheckboxPressed()
    {
        EmitSignal(nameof(CheckboxClicked));
    }
}
