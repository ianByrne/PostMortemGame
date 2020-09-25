using Godot;
using IanByrne.ResearchProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IanByrne.ResearchProject.Game
{
    public class Map : Node2D
    {
        private ObjectivesHUD _objectivesHUD;
        private LetterBox _letterBox;
        private DeadBody _reggie;
        private Cow _cow;
        private Barkeep _olive;
        private Farmer _clarence;

        // Blackboard stuff
        private PlayerLocation _playerLocation;
        private Dictionary<string, string> _facts;

        public override void _Ready()
        {
            _objectivesHUD = GetNode<ObjectivesHUD>("YSort/Player/Player/ObjectivesHUD");
            _letterBox = GetNode<LetterBox>("YSort/Buildings/LetterBox");
            _reggie = GetNode<DeadBody>("YSort/NPCs/DeadBody");
            _cow = GetNode<Cow>("YSort/NPCs/Cow");
            _olive = GetNode<Barkeep>("YSort/NPCs/Barkeep");
            _clarence = GetNode<Farmer>("YSort/NPCs/Farmer");

            // Connect to signals
            _letterBox.Connect("PlayerAtLetterBox", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _letterBox }));
            _letterBox.Connect("PlayerLeftLetterBox", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _letterBox }));
            _letterBox.Connect("NewObjectives", this, "NewObjectives");
            _reggie.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _reggie }));
            _reggie.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _reggie }));
            _reggie.Connect("NewObjectives", this, "NewObjectives");
            _cow.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _cow }));
            _cow.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _cow }));
            _cow.Connect("NewObjectives", this, "NewObjectives");
            _olive.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _olive }));
            _olive.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _olive }));
            _olive.Connect("NewObjectives", this, "NewObjectives");
            _clarence.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _clarence }));
            _clarence.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _clarence }));
            _clarence.Connect("NewObjectives", this, "NewObjectives");

            // Add first objective (go to letterbox)
            _objectivesHUD.ClearObjectives();
            _objectivesHUD.AddObjective(new Objective()
            {
                Target = typeof(LetterBox),
                Text = "Collect mail from letterbox"
            });
            _letterBox.ShowNotification();

            _playerLocation = PlayerLocation.Wandering;
        }

        // Blackboard
        private void HandleObjectives()
        {
            switch(_playerLocation)
            {
                case PlayerLocation.LetterBox:
                    if (_objectivesHUD.Objectives.Any(o => o.Target == typeof(LetterBox)))
                    {
                        _objectivesHUD.RemoveObjective(typeof(LetterBox));
                    }
                    break;

                case PlayerLocation.Reggie:
                    if (_objectivesHUD.Objectives.Any(o => o.Target == typeof(DeadBody)))
                    {
                        _objectivesHUD.RemoveObjective(typeof(DeadBody));
                    }
                    break;

                case PlayerLocation.Cow:
                    if (_objectivesHUD.Objectives.Any(o => o.Target == typeof(Cow)))
                    {
                        _objectivesHUD.RemoveObjective(typeof(Cow));
                    }
                    break;

                case PlayerLocation.Olive:
                    if (_objectivesHUD.Objectives.Any(o => o.Target == typeof(Barkeep)))
                    {
                        _objectivesHUD.RemoveObjective(typeof(Barkeep));
                    }
                    break;

                case PlayerLocation.Clarence:
                    if (_objectivesHUD.Objectives.Any(o => o.Target == typeof(Farmer)))
                    {
                        _objectivesHUD.RemoveObjective(typeof(Farmer));
                    }
                    break;
            }
        }

        // Signal handlers
        private void PlayerAtLocation(Godot.Object sender)
        {
            switch(sender)
            {
                case LetterBox letterBox:
                    _playerLocation = PlayerLocation.LetterBox;
                    break;

                case DeadBody reggie:
                    _playerLocation = PlayerLocation.Reggie;
                    break;

                case Cow cow:
                    _playerLocation = PlayerLocation.Cow;
                    break;

                case Barkeep olive:
                    _playerLocation = PlayerLocation.Olive;
                    break;

                case Farmer clarence:
                    _playerLocation = PlayerLocation.Clarence;
                    break;
            }

            HandleObjectives();
        }

        private void PlayerLeftLocation(Godot.Object sender)
        {
            _playerLocation = PlayerLocation.Wandering;

            HandleObjectives();
        }

        private void NewObjectives(List<Objective> objectives)
        {
            foreach (var objective in objectives)
            {
                _objectivesHUD.AddObjective(objective);
            }

            HandleObjectives();
        }
    }
}