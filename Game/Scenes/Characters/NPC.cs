using Godot;
using IanByrne.ResearchProject.Shared.Models;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Game
{
	public class NPC : Character
	{
		[Export]
		public string BotName { get; set; }

		[Signal]
		public delegate void PlayerAtNpc();
		[Signal]
		public delegate void PlayerLeftNpc();
		[Signal]
		public delegate void NewObjectives(List<Objective> objectives);
		[Signal]
		public delegate void NewFacts(List<string> facts);

		private Console _console;
		private Player _player;

		public override void _Ready()
		{
			base._Ready();

			_console = GetNode<Console>("HUD/Console");
			_console.BotName = BotName;
		}

		public override void _Process(float delta)
		{
			if(_player != null)
			{
				var direction = Position.DirectionTo(_player.Position);

				AnimationTree.Set("parameters/Run/blend_position", direction);
				AnimationTree.Set("parameters/Idle/blend_position", direction);
			}
		}

		private void _OnDetectionAreaBodyEntered(object body)
		{
			if (body is Player player)
			{
				_player = player;

				_console.SetGameMode(player.GameMode);
				_console.Show();
				_console.SendWelcome();

				EmitSignal(nameof(PlayerAtNpc));
			}
		}

		private void _OnDetectionAreaBodyExited(object body)
		{
			if (body is Player player)
			{
				_player = null;

				_console.Hide();

				EmitSignal(nameof(PlayerLeftNpc));
			}
		}
	}
}
