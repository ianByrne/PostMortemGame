using Godot;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;


namespace IanByrne.ResearchProject.Game
{
	public class Player : Character
	{
        private Sprite _sprite;

        public override void _Ready()
        {
            _sprite = GetNode<Sprite>("Sprite");

            base._Ready();
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("SwitchGameMode"))
            {
                var user = GetNode<Map>("/root/Map").User;
                var context = GetNode<Map>("/root/Map").Context;

                var newGameMode = user.GameMode == GameMode.ChatBot ? GameMode.DialogueTree : GameMode.ChatBot;
                user.GameMode = newGameMode;
                user.UsedDevCommand = true;

                if (OS.HasFeature("JavaScript"))
                {
                    string javaScript = @"
						var user = " + JsonConvert.SerializeObject(user) + @";

						parent.SaveUser(user);";

                    JavaScript.Eval(javaScript);
                }
                else
                {
                    user.Save(context);
                }
            }
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventScreenTouch touchEvent)
            {
                if (_sprite.Visible)
                    MoveToPosition(GetCanvasTransform().AffineInverse().Xform(touchEvent.Position));
            }
            else if (@event.IsActionPressed("Click"))
			{
                if(_sprite.Visible)
				    MoveToPosition(GetGlobalMousePosition());
			}
		}

        public void Disable()
        {
            GetNode<ObjectivesHUD>("ObjectivesLayer/ObjectivesHUD").Hide();
            _sprite.Hide();
        }

        public void Enable()
        {
            GetNode<ObjectivesHUD>("ObjectivesLayer/ObjectivesHUD").Show();
            _sprite.Show();
        }
	}
}
