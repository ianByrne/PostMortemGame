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
            if (User.Objectives == null)
            {
                User.Objectives = new List<ObjectiveEntity>();
            }

            var unfinishedObjectives = User
            .Objectives
            .Where(o => !o.Done).ToList();

            var facts = User?.Facts?.Split(',').ToList() ?? new List<string>();

            foreach (var objective in unfinishedObjectives)
            {
                // If player is at objective's target and has all the required facts...
                if (_playerLocation == objective.Target
                    && (string.IsNullOrWhiteSpace(objective.RequiredFacts) || new Objective(objective).RequiredFacts.All(facts.Contains)))
                {
                    objective.Done = true;
                    _objectivesHUD.MarkObjectiveAsDone(objective.Text);

                    /// Objectives
                    if (objective.Target == MapLocation.LetterBox
                        && objective.Text == "Collect mail from letterbox"
                        && facts.Contains("CollectedFirstDelivery")
                        && facts.Contains("CollectedSecondDelivery")
                        && !facts.Contains("CollectedThirdDelivery"))
                    {
                        facts.Add("CollectedThirdDelivery");

                        var newObjective = new Objective()
                        {
                            Target = MapLocation.Clarence,
                            Text = "Deliver letter to Clarence"
                        };

                        User.Objectives.Add(new ObjectiveEntity(newObjective));
                        _objectivesHUD.AddObjective(newObjective.Text);
                    }

                    if (objective.Target == MapLocation.LetterBox
                        && objective.Text == "Collect mail from letterbox"
                        && facts.Contains("CollectedFirstDelivery")
                        && !facts.Contains("CollectedSecondDelivery"))
                    {
                        facts.Add("CollectedSecondDelivery");

                        var newObjective = new Objective()
                        {
                            Target = MapLocation.Clarence,
                            Text = "Deliver flyer to Clarence"
                        };

                        User.Objectives.Add(new ObjectiveEntity(newObjective));
                        _objectivesHUD.AddObjective(newObjective.Text);

                        newObjective = new Objective()
                        {
                            Target = MapLocation.Olive,
                            Text = "Deliver parcel to Olive"
                        };

                        User.Objectives.Add(new ObjectiveEntity(newObjective));
                        _objectivesHUD.AddObjective(newObjective.Text);
                    }

                    if (objective.Target == MapLocation.LetterBox
                        && objective.Text == "Collect mail from letterbox"
                        && !facts.Contains("CollectedFirstDelivery"))
                    {
                        facts.Add("CollectedFirstDelivery");

                        var newObjective = new Objective()
                        {
                            Target = MapLocation.Clarence,
                            Text = "Deliver welcome pamphlet to Clarence"
                        };

                        User.Objectives.Add(new ObjectiveEntity(newObjective));
                        _objectivesHUD.AddObjective(newObjective.Text);
                    }
                }
            }

            if (facts.Contains("AmPostman")
                && !facts.Contains("CollectedFirstDelivery"))
            {
                var newObjective = new Objective()
                {
                    Target = MapLocation.LetterBox,
                    Text = "Collect mail from letterbox"
                };

                if (!User.Objectives.Any(o => o.Text == "Collect mail from letterbox" && !o.Done))
                {
                    User.Objectives.Add(new ObjectiveEntity(newObjective));
                    _objectivesHUD.AddObjective(newObjective.Text);
                }
            }

            if (!facts.Contains("CollectedSecondDelivery")
                && User.Objectives.Any(o => o.Text == "Deliver welcome pamphlet to Clarence" && o.Done))
            {
                var newObjective = new Objective()
                {
                    Target = MapLocation.LetterBox,
                    Text = "Collect mail from letterbox"
                };

                if (!User.Objectives.Any(o => o.Text == "Collect mail from letterbox" && !o.Done))
                {
                    User.Objectives.Add(new ObjectiveEntity(newObjective));
                    _objectivesHUD.AddObjective(newObjective.Text, 3);
                }
            }

            if (!facts.Contains("CollectedThirdDelivery")
                && User.Objectives.Any(o => o.Text == "Deliver flyer to Clarence" && o.Done)
                && User.Objectives.Any(o => o.Text == "Deliver parcel to Olive" && o.Done))
            {
                var newObjective = new Objective()
                {
                    Target = MapLocation.LetterBox,
                    Text = "Collect mail from letterbox"
                };

                if (!User.Objectives.Any(o => o.Text == "Collect mail from letterbox" && !o.Done))
                {
                    User.Objectives.Add(new ObjectiveEntity(newObjective));
                    _objectivesHUD.AddObjective(newObjective.Text, 3);
                }
            }

            if (facts.Contains("HasLetterFromClarence"))
            {
                var newObjective = new Objective()
                {
                    Text = "Find out how to deliver Clarence's letter",
                    Target = MapLocation.None
                };

                if (!User.Objectives.Any(o => o.Text == "Find out how to deliver Clarence's letter"))
                {
                    User.Objectives.Add(new ObjectiveEntity(newObjective));
                    _objectivesHUD.AddObjective(newObjective.Text);
                }
            }

            if (facts.Contains("CanSendOutgoingMail") && facts.Contains("HasLetterFromClarence") && _playerLocation == MapLocation.LetterBox)
            {
                var objectivesToComplete = User
                    .Objectives
                    .Where(o => o.Text == "Find out how to deliver Clarence's letter" && !o.Done);

                foreach (var objective in objectivesToComplete)
                {
                    objective.Done = true;
                    _objectivesHUD.MarkObjectiveAsDone(objective.Text);
                }

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

                var newObjective = new Objective()
                {
                    Text = "Thanks for playing Post Mortem!",
                    Target = MapLocation.None
                };

                if (!User.Objectives.Any(o => o.Text == "Thanks for playing Post Mortem!" && !o.Done))
                {
                    User.Objectives.Add(new ObjectiveEntity(newObjective));
                    _objectivesHUD.AddObjective(newObjective.Text);
                }
            }

            // Remove duplicate facts
            User.Facts = string.Join(",", facts.Where(f => !string.IsNullOrWhiteSpace(f)).Distinct().ToList());

            // Save user
            if (OS.HasFeature("JavaScript"))
            {
                string javaScript = @"
						var user = " + JsonConvert.SerializeObject(User) + @";

						parent.SaveUser(user);";

                JavaScript.Eval(javaScript);

                javaScript = "parent.GetUserFromCookieId();";

                string jsResponse = JavaScript.Eval(javaScript)?.ToString();

                User = string.IsNullOrWhiteSpace(jsResponse) ? User : JsonConvert.DeserializeObject<User>(jsResponse);
            }
            else
            {
                User.Save(Context);
            }
        }
    }
}