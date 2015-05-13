using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game {

    public class Action : MonoBehaviour 
    {
        // Fields
        internal List<AnimationClip> _animations;
        internal float _attackDuration;
        internal float _delayTakeDamage;
        internal int _strengh;
        internal Texture _image;
        internal Character _character;
        internal bool _isStoppable;

        List<Vector3> _attackVectors;
        RaycastHit _hit;

        public float AttackDuration
        {
            get{ return _attackDuration; }
        }

	    public Action (Character character) 
        {
            _character = character;
            _attackVectors = new List<Vector3>();
            _attackVectors.Add( new Vector3( 0, 1, 2 ) );
            _attackVectors.Add( new Vector3( -1, 1, 2 ) );
            _attackVectors.Add( new Vector3( 1, 1, 2 ) );
        
        }

        public virtual void Act () 
        {
        
        }

        // TODO : remettre l'origine de l'attaque au bon endroit
        internal virtual List<Character> GetListOfTarget ()
        {
            List<Character> _targets = new List<Character>();
            Vector3 realAttackOrigin = new Vector3( transform.position.x, transform.position.y + 1, transform.position.z );

            foreach ( Vector3 vector in _attackVectors )
            {
                Debug.DrawRay( realAttackOrigin, _character.transform.TransformDirection( vector ), Color.yellow, 1.0f );
                if ( Physics.Raycast( realAttackOrigin, _character.transform.TransformDirection( vector ), out _hit ) )
                {
                    Character target = null;
                    target = _hit.transform.GetComponent<Character>();
                    if ( target != null && target.name != _character.name )
                    {
                        _targets.Add( target );
                    }
                }
            }
            return _targets;
        }

    }
}