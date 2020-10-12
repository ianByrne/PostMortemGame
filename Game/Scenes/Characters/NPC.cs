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
		public delegate void NewFacts(string[] facts);

		private Console _console;
		private Sprite _avatar;
		private Button _closeButton;
		private Player _player;

		public override void _Ready()
		{
			base._Ready();

			_avatar = GetNode<Sprite>("HUD/Avatar");
			_avatar.Hide();
			_closeButton = GetNode<Button>("HUD/CloseButton");
			_closeButton.Hide();
			_console = GetNode<Console>("HUD/Console");
			_console.BotName = BotName;
			_console.Connect("NewFacts", this, "_NewFacts");
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
				var user = GetNode<Map>("/root/Map").User;

				_console.SetGameMode(user.GameMode);
				_console.Show();
				_avatar.Show();
				_closeButton.Show();
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
				_avatar.Hide();
				_closeButton.Hide();

				EmitSignal(nameof(PlayerLeftNpc));
			}
		}

		private void _NewFacts(string[] facts)
        {
			EmitSignal(nameof(NewFacts), new[] { facts });
		}

		private void _OnCloseButtonPressed()
        {
			_console.Hide();
			_avatar.Hide();
			_closeButton.Hide();
		}
	}
}
