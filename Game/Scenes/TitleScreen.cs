using Godot;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;
using System;

namespace IanByrne.ResearchProject.Game
{
    public class TitleScreen : Control
    {
        Timer _checkModalTimer;
        Map _map;

        public override void _Ready()
        {
            _checkModalTimer = GetNode<Timer>("CheckModalTimer");
            _map = GetNode<Map>("/root/Map");
        }

        private void _OnContinueGameButtonPressed()
        {
            // Show participant information modal
            if (OS.HasFeature("JavaScript"))
            {
                string javaScript = @"parent.ShowParticipantInformationModal();";

                JavaScript.Eval(javaScript);
            }

            if (OS.HasFeature("JavaScript"))
            {
                string javaScript = "parent.GetUserFromCookieId();";

                string jsResponse = JavaScript.Eval(javaScript)?.ToString();

                _map.User = string.IsNullOrWhiteSpace(jsResponse) ? null : JsonConvert.DeserializeObject<User>(jsResponse);
            }

            _checkModalTimer.Start();
        }

        private void _onCheckModalTimerTimeout()
        {
            bool isModalClosed = false;

            if (OS.HasFeature("JavaScript"))
            {
                string javaScript = @"parent.IsParticipantInformationModalClosed();";

                isModalClosed = Convert.ToBoolean(JavaScript.Eval(javaScript));
            }
            else
            {
                isModalClosed = true;
            }

            if (isModalClosed)
            {
                _checkModalTimer.Stop();

                this.Hide();

                if (_map.User == null)
                {
                    // Show consent screen
                    var consentScreen = GetNode<ConsentScreen>("/root/Map/ConsentScreen");
                    consentScreen.Show();
                }
                else
                {
                    _map.StartGame();
                }
            }
        }
    }
}
