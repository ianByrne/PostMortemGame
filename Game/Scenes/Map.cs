using Godot;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace IanByrne.ResearchProject.Game
{
    public partial class Map : Node2D
    {
        private const string _version = "v0.4";

        private ObjectivesHUD _objectivesHUD;
        private LetterBox _letterBox;
        private DeadBody _reggie;
        private Cow _cow;
        private Barkeep _olive;
        private Farmer _clarence;
        private MapLocation _playerLocation;

        public User User { get; set; }
        public PostMortemContext Context { get; private set; }

        public override void _Ready()
        {
            GD.Print(_version);

            Context = new PostMortemContext();
            _objectivesHUD = GetNode<ObjectivesHUD>("Game/YSort/Player/Player/ObjectivesLayer/ObjectivesHUD");
            _letterBox = GetNode<LetterBox>("Game/YSort/Buildings/LetterBox");
            _reggie = GetNode<DeadBody>("Game/YSort/NPCs/DeadBody");
            _cow = GetNode<Cow>("Game/YSort/NPCs/Cow");
            _olive = GetNode<Barkeep>("Game/YSort/NPCs/Barkeep");
            _clarence = GetNode<Farmer>("Game/YSort/NPCs/Farmer");

            // Connect to signals
            _letterBox.Connect("PlayerAtLetterBox", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _letterBox }));
            _letterBox.Connect("PlayerLeftLetterBox", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _letterBox }));
            _letterBox.Connect("NewFacts", this, "NewFacts");
            _reggie.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _reggie }));
            _reggie.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _reggie }));
            _reggie.Connect("NewFacts", this, "NewFacts");
            _cow.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _cow }));
            _cow.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _cow }));
            _cow.Connect("NewFacts", this, "NewFacts");
            _olive.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _olive }));
            _olive.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _olive }));
            _olive.Connect("NewFacts", this, "NewFacts");
            _clarence.Connect("PlayerAtNpc", this, "PlayerAtLocation", new Godot.Collections.Array(new[] { _clarence }));
            _clarence.Connect("PlayerLeftNpc", this, "PlayerLeftLocation", new Godot.Collections.Array(new[] { _clarence }));
            _clarence.Connect("NewFacts", this, "NewFacts");

            // Sort out player
            var player = GetNode<Player>("Game/YSort/Player/Player");
            player.Disable();

            _playerLocation = MapLocation.Wandering;
        }

        public void StartGame()
        {
            var player = GetNode<Player>("Game/YSort/Player/Player");
            player.MoveToPosition(new Vector2(1630, -224));

            var startGameTimer = new Timer();
            startGameTimer.Connect("timeout", this, nameof(ShowPlayer));
            startGameTimer.OneShot = true;
            AddChild(startGameTimer);
            startGameTimer.Start(2);

            GetNode<Node2D>("Game").Modulate = new Color(1, 1, 1, 1);
        }

        private void ShowPlayer()
        {
            var player = GetNode<Player>("Game/YSort/Player/Player");
            player.Enable();

            if (User.Objectives != null)
            {
                foreach (var objective in User.Objectives)
                {
                    if (!objective.Done)
                    {
                        _objectivesHUD.AddObjective(objective.Text);
                    }
                }
            }
        }

        // Signal handlers
        private void PlayerAtLocation(Godot.Object sender)
        {
            switch (sender)
            {
                case LetterBox letterBox:
                    _playerLocation = MapLocation.LetterBox;
                    break;

                case DeadBody reggie:
                    _playerLocation = MapLocation.Reggie;
                    break;

                case Cow cow:
                    _playerLocation = MapLocation.Cow;
                    break;

                case Barkeep olive:
                    _playerLocation = MapLocation.Olive;
                    break;

                case Farmer clarence:
                    _playerLocation = MapLocation.Clarence;
                    break;
            }

            HandleObjectives();
        }

        private void PlayerLeftLocation(Godot.Object sender)
        {
            _playerLocation = MapLocation.Wandering;

            HandleObjectives();
        }

        private void NewObjectives(List<Objective> objectives)
        {
            foreach (var objective in objectives)
            {
                _objectivesHUD.AddObjective(objective.Text);
            }

            HandleObjectives();
        }

        private void NewFacts(string[] facts)
        {
            var oldFacts = User?.Facts?.Split(',').ToList() ?? new List<string>();
            oldFacts.AddRange(facts);
            oldFacts = oldFacts.Distinct().ToList();
            User.Facts = string.Join(",", oldFacts);

            HandleObjectives();
        }
    }
}