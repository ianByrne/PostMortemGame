using Godot;
using System;

namespace IanByrne.ResearchProject.Game
{
	public class LetterBox : StaticBody2D
	{
		private void _OnDetectionAreaBodyEntered(object body)
		{
			if (body is Player player)
			{
				GD.Print("Player at letterbox");
			}
		}


		private void _OnDetectionAreaBodyExited(object body)
		{
			if (body is Player player)
			{
				GD.Print("Player left letterbox");
			}
		}
	}
}
