using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game {

    public abstract class Competence : MonoBehaviour {

        // Fields
        Animation[] _animations;
        float _attackDuration;
        float _delayTakeDamage;
        int _strengh;
        Texture _image;
        Character _character;
        bool _isStoppable;

	    public abstract void Start () {
	        
	    }

        public virtual void Act ()
        {
            _character.
        }
	
    }
}