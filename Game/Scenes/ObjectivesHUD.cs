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
        private Timer _timer;
        
        public List<Objective> Objectives { get; private set; }

        public override void _Ready()
        {
            _objectivesLabel = GetNode<Label>("Objectives");
            _timer = new Timer();
            Objectives = new List<Objective>();

            _timer.Connect("timeout", this, "ShowObjectives");
            _timer.OneShot = true;
            AddChild(_timer);

            Objectives.Clear();
            ShowObjectives();
        }

        public void AddObjective(Objective objective, float delay = 0)
        {
            Objectives.Add(objective);

            if (delay > 0)
            {
                _timer.Start(delay);
            }
            else
            {
                ShowObjectives();
            }
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