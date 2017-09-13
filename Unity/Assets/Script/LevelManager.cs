using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace EpicSpirit.Game
{
    public class LevelManager
    {
    	private static Dictionary<string, object> _parameters = new Dictionary<string, object>();

        static LevelManager ()
        {
            SetParameter( "level", "" );
        }
        public static object GetParameter(string key)
	        {
       		    if (_parameters.ContainsKey(key))
			        return _parameters[key];
       		    else 
                    return null;
    	    }

    	    public static void SetParameter(string key, object value)
    	    {
		        if (_parameters.ContainsKey(key))
                    _parameters[key] = value;
        	    else
			        _parameters.Add(key, value);
    	    }

	        public static void ClearParameters()
	        {
                _parameters.Clear();
    	    }

    	    public static void LoadLevel(string level)
    	    {
		        LoadLevel(level, null);
	        }

	        public static void LoadLevel(string level, Dictionary<string, object> param)
	        {
            if (param != null)
                _parameters = param;
                SceneManager.LoadScene(level);
	        }
    }
}