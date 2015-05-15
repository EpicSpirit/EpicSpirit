using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game {

    public class Competence : MonoBehaviour {

        // Fields
        Animation[] _animations;
        float _attackDuration;
        float _delayTakeDamage;
        int _strengh;
        Texture _image;
        Character _character;
        bool _isStoppable;

	    public virtual void Start () {}

        public virtual void Act () { }
	
    }
}