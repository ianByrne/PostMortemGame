using Godot;
using System;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.Game
{
    public class SceneSwitcher : Node
    {
        Dictionary<string, object> _parameters;

        public void ChangeScene(string nextScene, Dictionary<string, object> parameters = null)
        {
            _parameters = parameters;
            GetTree().ChangeScene(nextScene);
        }

        public object GetParameter(string key)
        {
            if (_parameters != null && _parameters.ContainsKey(key))
            {
                return _parameters[key];
            }
            else
            {
                return null;
            }
        }
    }
}