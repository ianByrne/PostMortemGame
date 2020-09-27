using Godot;
using IanByrne.ResearchProject.Shared.Models;
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
        private MapLocation _playerLocation;

        public List<string> Facts { get; private set; }

        public override void _Ready()
        {
            Facts = new List<string>();
            Facts.Add("Testing123");
            _objectivesHUD = GetNode<ObjectivesHUD>("YSort/Player/Player/ObjectivesHUD");
            _letterBox = GetNode<LetterBox>("YSort/Buildings/LetterBox");
            _reggie = GetNode<DeadBody>("YSort/NPCs/DeadBody");
            _cow = GetNode<Cow>("YSort/NPCs/Cow");
            _olive = GetNode<Barkeep>("YSort/NPCs/Barkeep");
            _clarence = GetNode<Farmer>("YSort/NPCs/Farmer");

            // Connect to signals
            _letterBox.Connect("PlayerAtLetterBox", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _letterBox }));
            _letterBox.Connect("PlayerLeftLetterBox", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _letterBox }));
            _letterBox.Connect("NewFacts", this, "NewFacts");
            _reggie.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _reggie }));
            _reggie.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _reggie }));
            _reggie.Connect("NewFacts", this, "NewFacts");
            _cow.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _cow }));
            _cow.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _cow }));
            _cow.Connect("NewFacts", this, "NewFacts");
            _olive.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _olive }));
            _olive.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _olive }));
            _olive.Connect("NewFacts", this, "NewFacts");
            _clarence.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _clarence }));
            _clarence.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _clarence }));
            _clarence.Connect("NewFacts", this, "NewFacts");

            _playerLocation = MapLocation.Wandering;
        }

        /// <summary>
        /// Blackboard:
        /// I have heard the term before but clearly have very little
        /// understanding of what it is or how to implement one
        /// </summary>
        private void HandleObjectives()
        {
            var unfinishedObjectives = _objectivesHUD.Objectives.Where(o => !o.Done).ToList();

            foreach (var objective in unfinishedObjectives)
            {
                if (_playerLocation == objective.Target
                    && (objective.RequiredFacts == null || objective.RequiredFacts.All(Facts.Contains)))
                {
                    _objectivesHUD.MarkObjectiveAsDone(objective);

                    /// Objectives
                    if (objective.Target == MapLocation.LetterBox
                        && objective.Text == "Collect mail from letterbox"
                        && !Facts.Contains("CollectedFirstDelivery"))
                    {
                        Facts.Add("CollectedFirstDelivery");

                        _objectivesHUD.AddObjective(new Objective()
                        {
                            Target = MapLocation.Clarence,
                            Text = "Deliver welcome pamphlet to Clarence"
                        });
                    }

                    if (objective.Target == MapLocation.LetterBox
                        && objective.Text == "Collect mail from letterbox"
                        && !Facts.Contains("CollectedFirstDelivery")
                        && !Facts.Contains("CollectedSecondDelivery"))
                    {
                        Facts.Add("CollectedSecondDelivery");

                        _objectivesHUD.AddObjective(new Objective()
                        {
                            Target = MapLocation.Clarence,
                            Text = "Deliver flyer to Clarence"
                        });

                        _objectivesHUD.AddObjective(new Objective()
                        {
                            Target = MapLocation.Olive,
                            Text = "Deliver parcel to Olive"
                        });
                    }

                    if (objective.Target == MapLocation.LetterBox
                        && objective.Text == "Collect mail from letterbox"
                        && !Facts.Contains("CollectedFirstDelivery")
                        && !Facts.Contains("CollectedSecondDelivery")
                        && !Facts.Contains("CollectedThirdDelivery"))
                    {
                        Facts.Add("CollectedThirdDelivery");

                        _objectivesHUD.AddObjective(new Objective()
                        {
                            Target = MapLocation.Clarence,
                            Text = "Deliver letter to Clarence"
                        });
                    }
                }
            }

            if(Facts.Contains("AmPostman")
                && !Facts.Contains("CollectedFirstDelivery"))
            {
                var objective = new Objective()
                {
                    Target = MapLocation.LetterBox,
                    Text = "Collect mail from letterbox"
                };

                if (!_objectivesHUD.Objectives.Any(o => o.Text == "Collect mail from letterbox" && !o.Done))
                {
                    _objectivesHUD.AddObjective(objective);
                    _letterBox.ShowNotification();
                }
            }

            if (!Facts.Contains("CollectedSecondDelivery")
                && _objectivesHUD.Objectives.Any(o => o.Text == "Deliver welcome pamphlet to Clarence" && o.Done))
            {
                var objective = new Objective()
                {
                    Target = MapLocation.LetterBox,
                    Text = "Collect mail from letterbox"
                };

                if (!_objectivesHUD.Objectives.Contains(objective))
                {
                    _objectivesHUD.AddObjective(objective, 10);
                    _letterBox.ShowNotification();
                }
            }

            if (!Facts.Contains("CollectedThirdDelivery")
                && _objectivesHUD.Objectives.Any(o => o.Text == "Deliver flyer to Clarence" && o.Done)
                && _objectivesHUD.Objectives.Any(o => o.Text == "Deliver parcel to Olive" && o.Done))
            {
                var objective = new Objective()
                {
                    Target = MapLocation.LetterBox,
                    Text = "Collect mail from letterbox"
                };

                if (!_objectivesHUD.Objectives.Contains(objective))
                {
                    _objectivesHUD.AddObjective(objective, 10);
                    _letterBox.ShowNotification();
                }
            }

            if (Facts.Contains("DeliveredClarencesLetter"))
            {
                // End game
                GD.Print("Game over, man!");
            }
        }

        // Signal handlers
        private void PlayerAtLocation(Godot.Object sender)
        {
            switch (sender)
            {
                case LetterBox letterBox:
                    _playerLocation = MapLocation.LetterBox;
                    break;

                case DeadBody reggie:
                    _playerLocation = MapLocation.Reggie;
                    break;

                case Cow cow:
                    _playerLocation = MapLocation.Cow;
                    break;

                case Barkeep olive:
                    _playerLocation = MapLocation.Olive;
                    break;

                case Farmer clarence:
                    _playerLocation = MapLocation.Clarence;
                    break;
            }

            HandleObjectives();
        }

        private void PlayerLeftLocation(Godot.Object sender)
        {
            _playerLocation = MapLocation.Wandering;

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

        private void NewFacts(string[] facts)
        {
            Facts.AddRange(facts);

            HandleObjectives();
        }
    }
}