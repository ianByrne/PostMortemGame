using Godot;
using IanByrne.ResearchProject.Shared.Models;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Game
{
    public class ObjectivesHUD : VBoxContainer
    {
        private Label _objectivesLabel;
        private List<Objective> _objectives;

        public override void _Ready()
        {
            _objectivesLabel = GetNode<Label>("Objectives");
            _objectives = new List<Objective>();
        }

        public void AddObjective(Objective objective)
        {
            _objectives.Add(objective);

            ShowObjectives();
        }

        public void RemoveObjective(Objective objective)
        {
            _objectives.Remove(objective);

            ShowObjectives();
        }

        public void ClearObjectives()
        {
            _objectives.Clear();

            ShowObjectives();
        }

        private void ShowObjectives()
        {
            _objectivesLabel.Text = "";
            int i = 1;

            foreach(var objective in _objectives)
            {
                _objectivesLabel.Text += i++ + ". " + objective.Text + "\n";
            }
        }
    }
}