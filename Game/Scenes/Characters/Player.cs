using Godot;
using System;


namespace IanByrne.ResearchProject.Game
{
	public class Player : Character
	{
		public override void _UnhandledInput(InputEvent @event)
		{
			if (@event.IsActionPressed("Click"))
			{
				MoveToPosition(GetGlobalMousePosition());
			}
		}
	}
}
