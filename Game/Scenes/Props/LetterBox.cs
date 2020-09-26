using Godot;
using IanByrne.ResearchProject.Shared.Models;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Game
{
	public class LetterBox : StaticBody2D
	{
		[Signal]
		public delegate void PlayerAtLetterBox();
		[Signal]
		public delegate void PlayerLeftLetterBox();
		[Signal]
		public delegate void NewObjectives(List<Objective> objectives);
		[Signal]
		public delegate void NewFacts(List<string> facts);

		private Label _notification;

        public override void _Ready()
        {
            base._Ready();

			_notification = GetNode<Label>("Notification");
        }

		public void ShowNotification()
        {
			_notification.Show();
        }

        private void _OnDetectionAreaBodyEntered(object body)
		{
			if (body is Player player)
			{
				_notification.Hide();

				EmitSignal(nameof(PlayerAtLetterBox));
			}
		}

		private void _OnDetectionAreaBodyExited(object body)
		{
			if (body is Player player)
			{
				EmitSignal(nameof(PlayerLeftLetterBox));
			}
		}
	}
}
