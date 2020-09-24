using Godot;
using IanByrne.ResearchProject.Shared.Models;
using System;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Game
{
    public class Map : Node2D
    {
        private ObjectivesHUD _objectivesHUD;
        private LetterBox _letterBox;

        public override void _Ready()
        {
            _objectivesHUD = GetNode<ObjectivesHUD>("YSort/Player/Player/ObjectivesHUD");
            _letterBox = GetNode<LetterBox>("YSort/Buildings/LetterBox");

            // Connect to signals
            _letterBox.Connect("PlayerAtLetterBox", this, "HandleSignal", new Godot.Collections.Array(new[] { _letterBox }));

            // Add first objective (go to letterbox)
            _objectivesHUD.ClearObjectives();
            _objectivesHUD.AddObjective(new Objective()
            {
                Target = typeof(LetterBox),
                Text = "Collect mail from letterbox"
            });
            _letterBox.ShowNotification();
        }

        private void HandleSignal(Godot.Object sender)
        {
            switch(sender)
            {
                case LetterBox letterBox:
                    GD.Print("Woop");
                    break;
            }
        }
    }
}