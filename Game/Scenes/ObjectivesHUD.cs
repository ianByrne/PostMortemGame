using Godot;
using IanByrne.ResearchProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IanByrne.ResearchProject.Game
{
    public class ObjectivesHUD : VBoxContainer
    {
        private Label _objectivesLabel;
        
        public List<Objective> Objectives { get; private set; }

        public override void _Ready()
        {
            _objectivesLabel = GetNode<Label>("Objectives");
            Objectives = new List<Objective>();
        }

        public void AddObjective(Objective objective)
        {
            Objectives.Add(objective);

            ShowObjectives();
        }

        public void MarkObjectiveAsDone(Objective objective)
        {
            Objectives
                .Where(o => o == objective)
                .ToList()
                .ForEach(o => o.Done = true);

            ShowObjectives();
        }

        private void ShowObjectives()
        {
            _objectivesLabel.Text = "";
            int i = 1;

            foreach(var objective in Objectives)
            {
                if(!objective.Done)
                    _objectivesLabel.Text += i++ + ". " + objective.Text + "\n";
            }
        }
    }
}