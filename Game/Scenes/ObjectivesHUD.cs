using Godot;
using IanByrne.ResearchProject.Shared.Models;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Game
{
    public class ObjectivesHUD : VBoxContainer
    {
        private Label _objectivesLabel;
        private Timer _timer;
        private List<string> _objectives;

        public override void _Ready()
        {
            _objectivesLabel = GetNode<Label>("Objectives");
            _objectives = new List<string>();
            _timer = new Timer();

            _timer.Connect("timeout", this, "ShowObjectives");
            _timer.OneShot = true;
            AddChild(_timer);

            ShowObjectives();
        }

        public void AddObjective(string objective, float delay = 0)
        {
            _objectives.Add(objective);

            if (delay > 0)
            {
                _timer.Start(delay);
            }
            else
            {
                ShowObjectives();
            }
        }

        public void MarkObjectiveAsDone(string objective)
        {
            _objectives.RemoveAll(o => o == objective);

            ShowObjectives();
        }

        private void ShowObjectives()
        {
            _objectivesLabel.Text = "";
            int i = 1;

            foreach (var objective in _objectives)
            {
                _objectivesLabel.Text += i++ + ". " + objective + "\n";
            }
        }
    }
}