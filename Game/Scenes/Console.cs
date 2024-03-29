using Godot;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared;
using IanByrne.ResearchProject.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace IanByrne.ResearchProject.Game
{
    public class Console : VBoxContainer
    {
        [Signal]
        public delegate void NewFacts(string[] facts);

        [Signal]
        public delegate void CloseButtonPushed();

        private TextEdit _log;
        private LineEdit _freeTextInput;
        private VBoxContainer _dialogueOptionsContainer;
        private bool _welcomeSent;
        private SendMessageResponse _lastResponse;
        private List<string> _oldFacts;
        private List<string> _messageQueue;
        private Timer _messageQueueTimer;

        public string BotName { get; set; }

        public override void _Ready()
        {
            _log = GetNode<TextEdit>("HBoxContainer/VBoxContainer/LogContainer/Log");
            _freeTextInput = GetNode<LineEdit>("HBoxContainer/VBoxContainer/FreeTextContainer/FreeTextInput");
            _dialogueOptionsContainer = GetNode<VBoxContainer>("HBoxContainer/VBoxContainer/DialogueOptionsContainer");
            _welcomeSent = false;
            _oldFacts = GetNode<Map>("/root/Map").User?.Facts?.Split(',').ToList() ?? new List<string>();

            _messageQueue = new List<string>();
            _messageQueueTimer = new Timer();
            _messageQueueTimer.Connect("timeout", this, nameof(UpdateLogFromQueue));
            _messageQueueTimer.OneShot = false;
            AddChild(_messageQueueTimer);
            _messageQueueTimer.Start(0.6f);

            Hide();
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
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
            var currentFacts = GetNode<Map>("/root/Map").User?.Facts?.Split(',').ToList() ?? new List<string>();

            if(_oldFacts != null && currentFacts != null && _oldFacts.Count != currentFacts.Count)
            {
                _welcomeSent = false;
            }

            if (!_welcomeSent)
            {
                UpdateLog("");
                _freeTextInput.Editable = false;
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
                    EnableDialogueOptions();
                }
            }

            _welcomeSent = true;
            _oldFacts = GetNode<Map>("/root/Map").User?.Facts?.Split(',').ToList() ?? new List<string>();
        }

        private void UpdateLog(string text)
        {
            _messageQueue.Add(text);
        }

        private void UpdateLogFromQueue()
        {
            string text = _messageQueue.FirstOrDefault();

            if (text != null)
            {
                _log.Text += "\n" + text;

                int lineCount = _log.GetLineCount();
                _log.CursorSetLine(lineCount);

                _messageQueue.RemoveAt(0);

                if (_messageQueue.Count < 1)
                {
                    _freeTextInput.Editable = true;
                    _freeTextInput.GrabFocus();
                    EnableDialogueOptions();
                }
            }
        }

        private void OnInputTextEntered(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                _freeTextInput.Editable = false;
                _freeTextInput.Text = "";

                UpdateLog("You: " + text);

                var response = SendMessageToChatScript(text);

                foreach (string message in response.Messages)
                {
                    UpdateLog(BotName + ": " + message);
                }

                _welcomeSent = false;
            }
        }

        private void OnDialogueOptionButtonPressed(string text)
        {
            DisableDialogueOptions();

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
                    InputData = JsonConvert.SerializeObject(map.User?.Facts?.Split(',').ToList() ?? new List<string>())
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

                        response = chatScript.SendMessage(request, GetNode<Map>("/root/Map").Context);
                    }
                }

                if (response.NewFacts != null && response.NewFacts.Length > 0)
                {
                    EmitSignal(nameof(NewFacts), new[] { response.NewFacts });
                    _oldFacts = GetNode<Map>("/root/Map").User?.Facts?.Split(',').ToList() ?? new List<string>();
                }

                _lastResponse = response;

                return response;
            }
            catch (Exception ex)
            {
                throw;
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
            button.Visible = false;
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

        private void DisableDialogueOptions()
        {
            var options = _dialogueOptionsContainer.GetChildren();

            foreach (Button option in options)
            {
                option.Visible = false;
            }
        }

        private void EnableDialogueOptions()
        {
            var options = _dialogueOptionsContainer.GetChildren();

            foreach (Button option in options)
            {
                option.Visible = true;
            }
        }

        private void _OnCloseButtonPressed()
        {
            EmitSignal(nameof(CloseButtonPushed));
        }

        private void _OnLogChanged()
        {
            int lineCount = _log.GetLineCount();
            _log.CursorSetLine(lineCount + 200);
            _log.ScrollVertical += 200;
        }
    }
}
