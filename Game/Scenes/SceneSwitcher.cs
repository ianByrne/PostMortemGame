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
            if(_parameters == null)
            {
                _parameters = new Dictionary<string, object>();
            }

            if(parameters != null)
            {
                foreach(var parameter in parameters)
                {
                    if(!_parameters.ContainsKey(parameter.Key))
                    {
                        _parameters.Add(parameter.Key, parameter.Value);
                    }
                }
            }

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