using Godot;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;
using System;

namespace IanByrne.ResearchProject.Game
{
	public class TitleScreen : Control
	{
		Button _newGameButton;
		Button _continueGameButton;
		User _user;
		PostMortemContext _context;

		public override void _Ready()
		{
			_context = new PostMortemContext();
			_newGameButton = GetNode<Button>("Menu/NewGameButton");
			_continueGameButton = GetNode<Button>("Menu/ContinueGameButton");

			if (OS.HasFeature("JavaScript"))
			{
				string javaScript = "parent.GetUserFromCookieId();";

				string jsResponse = JavaScript.Eval(javaScript)?.ToString();

				_user = string.IsNullOrWhiteSpace(jsResponse) ? null : JsonConvert.DeserializeObject<User>(jsResponse);
			}

			if (_user == null)
			{
				// New Game
				_newGameButton.Visible = true;
			}
			else
            {
				// Continue Game
				_continueGameButton.Visible = true;
            }
		}

		private void _OnContinueGameButtonPressed()
		{
			StartGame();
        }

		private void _OnNewGameButtonPressed()
		{
			var rnd = new Random();
			var gameMode = (GameMode)rnd.Next(2);
            var userCookieGuid = Guid.NewGuid();

            _user = new User(userCookieGuid, gameMode);

            if (OS.HasFeature("JavaScript"))
            {
                string javaScript = @"
						var user = " + JsonConvert.SerializeObject(_user) + @";

						parent.EnsureUserCreated(user);";

                JavaScript.Eval(javaScript);
            }
            else
            {
                _user.EnsureCreated(_context);
            }

			StartGame();
        }

		private void StartGame()
        {
			var parameters = new System.Collections.Generic.Dictionary<string, object>();
			parameters.Add("User", _user);
			parameters.Add("Context", _context);

			var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");

			sceneSwitcher.ChangeScene("res://Scenes/Map.tscn", parameters);
		}
	}
}
