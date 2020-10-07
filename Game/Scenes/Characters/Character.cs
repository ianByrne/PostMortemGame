using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IanByrne.ResearchProject.Game
{
	public class Character : KinematicBody2D
	{
		private const float _speed = 300;

		private Navigation2D _navigation2D;
		private List<Vector2> _path;

		protected AnimationTree AnimationTree { get; set; }
		protected AnimationNodeStateMachinePlayback AnimationMode { get; set; }

		public override void _Ready()
		{
			AnimationTree = GetNode<AnimationTree>("AnimationTree");
			AnimationMode = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
			_navigation2D = GetNode<Navigation2D>("/root/Map/Game/Navigation2D");
		}

		public override void _PhysicsProcess(float delta)
		{
			MoveAlongPath(delta);
		}

		public void MoveToPosition(Vector2 destination)
		{
			_path = _navigation2D.GetSimplePath(GlobalPosition, destination).ToList();
		}

		private void MoveAlongPath(float delta)
		{
			if (_path != null && _path.Count > 0)
			{
				float moveDistance = _speed * delta;

				while (moveDistance > 0 && _path.Count > 0)
				{
					var distanceToNextPoint = Position.DistanceTo(_path[0]);
					var direction = Position.DirectionTo(_path[0]);

					AnimationTree.Set("parameters/Run/blend_position", direction);
					AnimationTree.Set("parameters/Idle/blend_position", direction);
					AnimationMode.Travel("Run");

					if (moveDistance <= distanceToNextPoint)
					{
						var velocity = MoveAndSlide(direction * _speed);

						if (GetSlideCount() > 0)
						{
							var collision = GetSlideCollision(0);

							if (collision != null)
							{
								if (velocity.Length() < 20)
								{
									_path.Clear();
								}
							}
						}
					}
					else
					{
						Position = _path[0];
						_path.RemoveAt(0);
					}

					moveDistance -= distanceToNextPoint;
				}
			}
			else
			{
				AnimationMode.Travel("Idle");
			}
		}
	}
}
