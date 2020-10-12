using Godot;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IanByrne.ResearchProject.Game
{
    public partial class Map : Node2D
    {
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
                // If player is at objective's target and has all the required facts...
                if (_playerLocation == objective.Target
                    && (objective.RequiredFacts == null || objective.RequiredFacts.All(Facts.Contains)))
                {
                    _objectivesHUD.MarkObjectiveAsDone(objective);

                    /// Objectives
                    if (objective.Target == MapLocation.LetterBox
                        && objective.Text == "Collect mail from letterbox"
                        && Facts.Contains("CollectedFirstDelivery")
                        && Facts.Contains("CollectedSecondDelivery")
                        && !Facts.Contains("CollectedThirdDelivery"))
                    {
                        Facts.Add("CollectedThirdDelivery");

                        _objectivesHUD.AddObjective(new Objective()
                        {
                            Target = MapLocation.Clarence,
                            Text = "Deliver letter to Clarence"
                        });
                    }

                    if (objective.Target == MapLocation.LetterBox
                        && objective.Text == "Collect mail from letterbox"
                        && Facts.Contains("CollectedFirstDelivery")
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
                        && !Facts.Contains("CollectedFirstDelivery"))
                    {
                        Facts.Add("CollectedFirstDelivery");

                        _objectivesHUD.AddObjective(new Objective()
                        {
                            Target = MapLocation.Clarence,
                            Text = "Deliver welcome pamphlet to Clarence"
                        });
                    }
                }
            }

            if (Facts.Contains("AmPostman")
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

                if (!_objectivesHUD.Objectives.Any(o => o.Text == "Collect mail from letterbox" && !o.Done))
                {
                    _objectivesHUD.AddObjective(objective, 3);
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

                if (!_objectivesHUD.Objectives.Any(o => o.Text == "Collect mail from letterbox" && !o.Done))
                {
                    _objectivesHUD.AddObjective(objective, 3);
                }
            }

            if (Facts.Contains("HasLetterFromClarence"))
            {
                var objective = new Objective()
                {
                    Text = "Find out how to deliver Clarence's letter"
                };

                if (!_objectivesHUD.Objectives.Any(o => o.Text == "Find out how to deliver Clarence's letter" && !o.Done))
                {
                    _objectivesHUD.AddObjective(objective);
                }
            }

            if (Facts.Contains("CanSendOutgoingMail") && Facts.Contains("HasLetterFromClarence") && _playerLocation == MapLocation.LetterBox)
            {
                _objectivesHUD.Objectives
                    .Where(o => o.Text == "Find out how to deliver Clarence's letter" && !o.Done)
                    .ToList()
                    .ForEach(o => _objectivesHUD.MarkObjectiveAsDone(o));

                // End game
                if (OS.HasFeature("JavaScript"))
                {
                    string javaScript = "parent.GetUserFromCookieId();";

                    string jsResponse = JavaScript.Eval(javaScript)?.ToString();

                    User = string.IsNullOrWhiteSpace(jsResponse) ? User : JsonConvert.DeserializeObject<User>(jsResponse);

                    javaScript = @"
                        var user = " + JsonConvert.SerializeObject(User) + @";

                        parent.GameOver(user);
                        ";

                    jsResponse = JavaScript.Eval(javaScript)?.ToString();

                    User = string.IsNullOrWhiteSpace(jsResponse) ? null : JsonConvert.DeserializeObject<User>(jsResponse);
                }
                else
                {
                    User.WinDateTime = DateTime.UtcNow;
                    User.Save(Context);
                }

                var objective = new Objective()
                {
                    Text = "Thanks for playing Post Mortem!"
                };

                if (!_objectivesHUD.Objectives.Any(o => o.Text == "Thanks for playing Post Mortem!" && !o.Done))
                {
                    _objectivesHUD.AddObjective(objective);
                }
            }

            // Remove duplicate facts
            Facts = Facts.Distinct().ToList();
        }
    }
}