using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Cinematic_EndTrigger : MonoBehaviour {

        List<GameObject> _listWatchers;
        System.Action _endFunction;
        bool _begin;

        public void Awake()
        {
            _listWatchers = new List<GameObject>();
            _begin = false;
        }
        public void AddWatchers (GameObject gameObject)
        {
            _listWatchers.Add( gameObject );
        }

        public void Update()
        {
            if ( _begin )
            {
                for ( var i=0; i < _listWatchers.Count; i++ )
                {
                    // Si on a encore un élément, on ne balance pas la fin
                    if ( _listWatchers [i] != null ) return;

                }
                // Si l'on est encore là c'est que l'on doit lancer la function de fin
                _endFunction.Invoke();
                _begin = false;
            }
        }

        internal void SetFunctionEndOfCinematic ( System.Action EndOfCinematic )
        {
            _begin = true;
            _endFunction = EndOfCinematic;
        }
    }
}