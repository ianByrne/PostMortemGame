using Godot;
using IanByrne.ResearchProject.Game;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;
using System;

public class ConsentScreen : Control
{
    Button _continueButton;

    public override void _Ready()
    {
        _continueButton = GetNode<Button>("ContinueButton");
        //_continueButton.Disabled = true;

        base._Ready();
    }

    private void _OnCheckBoxChecked()
    {
        var checkboxes = GetNode<VBoxContainer>("ConsentItems").GetChildren();

        bool disabled = false;

        foreach (var obj in checkboxes)
        {
            if (obj is ConsentItem consentItem && !consentItem.CheckBox.Pressed)
            {
                disabled = true;
                break;
            }
        }

        _continueButton.Disabled = disabled;
    }

    private void _OnContinueGameButtonPressed()
    {
        this.Hide();

        var rnd = new Random();
        var gameMode = (GameMode)rnd.Next(2);
        var userCookieGuid = Guid.NewGuid();

        var user = new User(userCookieGuid, gameMode);

        if (OS.HasFeature("JavaScript"))
        {
            string javaScript = @"
                var user = " + JsonConvert.SerializeObject(user) + @";

                parent.EnsureUserCreated(user);";

            JavaScript.Eval(javaScript);
        }
        else
        {
            var context = GetNode<Map>("/root/Map").Context;
            user.EnsureCreated(context);
        }

        var map = GetNode<Map>("/root/Map");
        map.User = user;
        map.StartGame();
    }
}
