using Godot;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;
using System;

namespace IanByrne.ResearchProject.Game
{
	public class TitleScreen : Control
	{
		public override void _Ready()
		{
			foreach (MenuButton button in GetNode<VBoxContainer>("Menu").GetChildren())
			{
				button.Connect("pressed", this, "OnButtonPressed", new Godot.Collections.Array(new[] { button.GameMode }));
			}
		}

		private void OnButtonPressed(string gameModeStr)
		{
			GameMode gameMode;
			GameMode.TryParse(gameModeStr, out gameMode);

			// TO DO: Get from JS/cookie
			var userCookieGuid = Guid.NewGuid();

			var parameters = new System.Collections.Generic.Dictionary<string, object>();
			parameters.Add("GameMode", gameMode);
			parameters.Add("UserCookieId", userCookieGuid);

			var user = new User(userCookieGuid, gameMode);

			if (OS.HasFeature("JavaScript"))
			{
				string javaScript = @"
						var request = " + JsonConvert.SerializeObject(user) + @";

						parent.EnsureUserCreated(request);";
				
				JavaScript.Eval(javaScript);
			}
			else
			{
				user.EnsureCreated();
			}

			var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");

			sceneSwitcher.ChangeScene("res://Scenes/Map.tscn", parameters);
		}
	}
}
