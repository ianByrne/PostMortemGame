using Godot;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace IanByrne.ResearchProject.Game
{
    public class Console : VBoxContainer
    {
        [Signal]
        public delegate void NewFacts(string[] facts);

        private TextEdit _log;
        private LineEdit _freeTextInput;
        private VBoxContainer _dialogueOptionsContainer;
        private bool _welcomeSent;
        private PostMortemContext _context;
        private SendMessageResponse _lastResponse;
        private List<string> _oldFacts;

        public string BotName { get; set; }

        public override void _Ready()
        {
            _log = GetNode<TextEdit>("LogContainer/Log");
            _freeTextInput = GetNode<LineEdit>("FreeTextContainer/FreeTextInput");
            _dialogueOptionsContainer = GetNode<VBoxContainer>("DialogueOptionsContainer");
            _welcomeSent = false;
            _oldFacts = GetNode<Map>("/root/Map").Facts;

            Hide();
        }

        public void SetGameMode(GameMode gameMode)
        {
            if (gameMode == GameMode.ChatBot)
            {
                _freeTextInput.GetParent<VBoxContainer>().Show();
                _dialogueOptionsContainer.Hide();
            }
            else
            {
                _freeTextInput.GetParent<VBoxContainer>().Hide();
                _dialogueOptionsContainer.Show();

                ClearDialogueOptions();
            }
        }

        public void SendWelcome()
        {
            var user = GetNode<Map>("/root/Map").User;
            var currentFacts = GetNode<Map>("/root/Map").Facts;

            if(_oldFacts != null && currentFacts != null && _oldFacts.Count != currentFacts.Count)
            {
                _welcomeSent = false;
            }

            if (!_welcomeSent)
            {
                var response = SendMessageToChatScript(null);

                foreach (string message in response.Messages)
                {
                    UpdateLog(BotName + ": " + message);
                }

                if (user.GameMode == GameMode.DialogueTree)
                {
                    SetDialogueOptions(response);
                }
            }
            else
            {
                if (user.GameMode == GameMode.DialogueTree && _lastResponse != null)
                {
                    SetDialogueOptions(_lastResponse);
                }
            }

            _welcomeSent = true;
            _oldFacts = GetNode<Map>("/root/Map").Facts;
        }

        private void UpdateLog(string text)
        {
            _log.Text += text + "\n";

            int lineCount = _log.GetLineCount();
            _log.CursorSetLine(lineCount);
        }

        private void OnInputTextEntered(string text)
        {
            _freeTextInput.Text = "";

            UpdateLog("You: " + text);

            var response = SendMessageToChatScript(text);

            foreach (string message in response.Messages)
            {
                UpdateLog(BotName + ": " + message);
            }

            _welcomeSent = false;
        }

        private void OnDialogueOptionButtonPressed(string text)
        {
            UpdateLog("You: " + text);

            var response = SendMessageToChatScript(text);

            foreach (string message in response.Messages)
            {
                UpdateLog(BotName + ": " + message);
            }

            SetDialogueOptions(response);

            _welcomeSent = false;
        }

        private SendMessageResponse SendMessageToChatScript(string text)
        {
            try
            {
                var map = GetNode<Map>("/root/Map");

                var request = new SendMessageRequest
                {
                    UserCookieId = map.User.CookieId.ToString(),
                    BotName = BotName,
                    Message = text,
                    InputData = JsonConvert.SerializeObject(map.Facts)
                };

                SendMessageResponse response;

                if (OS.HasFeature("JavaScript"))
                {
                    string javaScript = @"
						var sendMessageRequest = " + JsonConvert.SerializeObject(request) + @";

						parent.SendMessageToChatScript(sendMessageRequest);
						";
                    string jsResponse = JavaScript.Eval(javaScript).ToString();

                    response = JsonConvert.DeserializeObject<SendMessageResponse>(jsResponse);
                }
                else
                {
                    var tcpClient = new TcpClient("localhost", 1024);
                    using (ITcpClient client = new TcpClientHandler(tcpClient))
                    {
                        var chatScript = new ChatScriptHandler(client);

                        response = chatScript.SendMessage(request, _context);
                    }
                }

                if (response.NewFacts != null && response.NewFacts.Length > 0)
                {
                    EmitSignal(nameof(NewFacts), new[] { response.NewFacts });
                    _oldFacts = GetNode<Map>("/root/Map").Facts;
                }

                _lastResponse = response;

                return response;
            }
            catch (Exception ex)
            {
                return new SendMessageResponse()
                {
                    Messages = new string[] { $"Failed to send to ChatScript: {ex.Message}" }
                };
            }
        }

        private void SetDialogueOptions(SendMessageResponse response)
        {
            ClearDialogueOptions();

            if (response.DialogueOptions != null && response.DialogueOptions.Length > 0)
            {
                foreach (string option in response.DialogueOptions)
                {
                    AddDialogueOption(option);
                }
            }
        }

        private void AddDialogueOption(string text)
        {
            PackedScene buttonScene = (PackedScene)ResourceLoader.Load("res://Resources/Buttons/DialogueOptionButton.tscn");

            var button = (Button)buttonScene.Instance();
            button.Text = text;
            button.Connect("pressed", this, "OnDialogueOptionButtonPressed", new Godot.Collections.Array(new[] { button.Text }));

            _dialogueOptionsContainer.AddChild(button);
        }

        private void ClearDialogueOptions()
        {
            var options = _dialogueOptionsContainer.GetChildren();

            foreach (Button option in options)
            {
                option.QueueFree();
            }
        }
    }
}
