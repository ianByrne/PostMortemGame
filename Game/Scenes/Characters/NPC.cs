using Godot;
using System;
using System.Threading.Tasks;

namespace IanByrne.ResearchProject.Game
{
	public class NPC : Character
	{
		[Export]
		public string BotName { get; set; }

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

				_console.Show();
				_console.SendWelcome();
			}
		}

		private void _OnDetectionAreaBodyExited(object body)
		{
			if (body is Player player)
			{
				_player = null;

				_console.Hide();
			}
		}
	}
}
