using Godot;
using Godot.Collections;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared.Models;
using System;
using System.Linq;

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

			using (var db = new PostMortemContext())
			{
				var user = db.Users.SingleOrDefault(x => x.CookieId == userCookieGuid);

				if(user == null)
                {
					db.Users.Add(new User()
					{
						CookieId = userCookieGuid,
						CreatedDateTime = DateTime.UtcNow,
						GameMode = gameMode
					});

					db.SaveChanges();
				}
			}

			var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");

			sceneSwitcher.ChangeScene("res://Scenes/Map.tscn", parameters);
		}
	}
}
