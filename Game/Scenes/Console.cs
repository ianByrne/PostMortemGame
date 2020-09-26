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
        public delegate void NewObjectives(List<Objective> objectives);
        [Signal]
        public delegate void NewFacts(List<string> facts);

        private GameMode _gameMode;
        private Guid _userCookieId;
        private TextEdit _log;
        private LineEdit _freeTextInput;
        private VBoxContainer _dialogueOptionsContainer;
        private bool _welcomeSent;
        private PostMortemContext _context;
        private SendMessageResponse _lastResponse;

        public string BotName { get; set; }

        public override void _Ready()
        {
            var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
            _userCookieId = ((User)sceneSwitcher.GetParameter("User")).CookieId;
            _context = (PostMortemContext)sceneSwitcher.GetParameter("Context");

            _log = GetNode<TextEdit>("LogContainer/Log");
            _freeTextInput = GetNode<LineEdit>("FreeTextContainer/FreeTextInput");
            _dialogueOptionsContainer = GetNode<VBoxContainer>("DialogueOptionsContainer");
            _welcomeSent = false;

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

            _gameMode = gameMode;
        }

        public void SendWelcome()
        {
            if (!_welcomeSent)
            {
                var response = SendMessageToChatScript(null);

                UpdateLog(BotName + ": " + response.Message);

                if (_gameMode == GameMode.DialogueTree)
                {
                    SetDialogueOptions(response);
                }
            }
            else
            {
                if (_gameMode == GameMode.DialogueTree && _lastResponse != null)
                {
                    SetDialogueOptions(_lastResponse);
                }
            }

            _welcomeSent = true;
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

            UpdateLog(BotName + ": " + response.Message);

            _welcomeSent = false;
        }

        private void OnDialogueOptionButtonPressed(string text)
        {
            UpdateLog("You: " + text);

            var response = SendMessageToChatScript(text);

            UpdateLog(BotName + ": " + response.Message);

            SetDialogueOptions(response);

            _welcomeSent = false;
        }

        private SendMessageResponse SendMessageToChatScript(string text)
        {
            try
            {
                var request = new SendMessageRequest
                {
                    UserCookieId = _userCookieId.ToString(),
                    BotName = BotName,
                    Message = text
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

                if (response.Facts != null && response.Facts.Count > 0)
                {
                    EmitSignal(nameof(NewFacts), response.Facts);
                }

                if (response.Objectives != null && response.Objectives.Count > 0)
                {
                    EmitSignal(nameof(NewObjectives), response.Objectives);
                }

                _lastResponse = response;

                return response;
            }
            catch (Exception ex)
            {
                return new SendMessageResponse()
                {
                    Message = $"Failed to send to ChatScript: {ex.Message}"
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
            else
            {
                AddDialogueOption("I see");
            }

            AddDialogueOption(":reset");
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
