using Godot;

namespace IanByrne.ResearchProject.Game
{
	public class LetterBox : StaticBody2D
	{
		[Signal]
		public delegate void PlayerAtLetterBox();

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
	}
}
