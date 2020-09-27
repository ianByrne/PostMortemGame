using Godot;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;


namespace IanByrne.ResearchProject.Game
{
	public class Player : Character
	{
        private User _user;
        private PostMortemContext _context;

        public GameMode GameMode { get { return _user.GameMode; } }

        public override void _Ready()
        {
            var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
            _user = (User)sceneSwitcher.GetParameter("User");
            _context = (PostMortemContext)sceneSwitcher.GetParameter("Context");

            base._Ready();
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("SwitchGameMode"))
            {
                var newGameMode = _user.GameMode == GameMode.ChatBot ? GameMode.DialogueTree : GameMode.ChatBot;
                _user.GameMode = newGameMode;
                _user.UsedDevCommand = true;

                if (OS.HasFeature("JavaScript"))
                {
                    string javaScript = @"
						var user = " + JsonConvert.SerializeObject(_user) + @";

						parent.SaveUser(user);";

                    JavaScript.Eval(javaScript);
                }
                else
                {
                    _user.Save(_context);
                }
            }
        }

        public override void _UnhandledInput(InputEvent @event)
		{
			if (@event.IsActionPressed("Click"))
			{
				MoveToPosition(GetGlobalMousePosition());
			}
		}
	}
}
