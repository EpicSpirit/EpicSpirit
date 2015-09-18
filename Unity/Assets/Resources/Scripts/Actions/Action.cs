using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace EpicSpirit.Game
{
    public class Action : MonoBehaviour 
    {
        // Fields
        internal List<AttackAnimation> _attackAnimations;
        internal Animation _animations;

        internal float _attackDuration;
        internal float _delayTakeDamage;
        internal int _strengh;
        internal Sprite _image;
        internal Character _character;
        internal bool _isStoppable;
		internal int _range;

        // CoolDown Managment
        internal float _currentCoolDown;
        internal float _cooldown;
        internal bool _isEnable;
        //Func<UISkill,float,bool> _function;
        //UISkill _textButton;

        //Sound Managment
        internal AudioSource _audioSource;

        public virtual void CancelAttack()
        {
            // Nothing
        }
        public void CancelAttack(string methodName)
        {
            if ( _isStoppable )
                CancelInvoke( methodName );
            else
                Debug.Log("Don't have to Cancel the Attack");
                
        }
        public float CurrentCoolDown
        {
            get { return _currentCoolDown; }
        }


        internal List<Vector3> _attackVectors;
        RaycastHit _hit;
        internal string _name;
        internal string _description;

        public float CoolDown
        {
            get { return _cooldown; }
        }

        public virtual float AttackDuration
        {
            get{ return _attackDuration; }
        }
        public Sprite GetSprite
        {
            get
            {
                return _image;
            }
        }
        public string Name
        {
            get { return _name; }
        }
        public string Description
        {
            get { return _description; }
        }
        public bool IsStoppable
        {
            get { return _isStoppable; }
        }

        public virtual void Awake ()
        {
            _currentCoolDown = 0;
            _isEnable = true;
            _character = GetComponent<Character>();
            _attackVectors = new List<Vector3>();
            _attackVectors.Add( new Vector3( 0, 1, 2 ) );
            _attackVectors.Add( new Vector3( -1, 1, 2 ) );
            _attackVectors.Add( new Vector3( 1, 1, 2 ) );
            _attackVectors.Add( new Vector3( -2, 1, 2 ) );
			_attackVectors.Add( new Vector3( 2, 1, 2 ) );

			_attackVectors.Add( new Vector3( 0, 0, 2 ) );
			_attackVectors.Add( new Vector3( -1, 0, 2 ) );
			_attackVectors.Add( new Vector3( 1, 0, 2 ) );
			_attackVectors.Add( new Vector3( -2, 0, 2 ) );
			_attackVectors.Add( new Vector3( 2, 0, 2 ) );

			_cooldown = 0f;
            _attackAnimations = new List<AttackAnimation>();
            _animations = GetComponentInChildren<Animation>();
            _audioSource = GetComponent<AudioSource>();
            _image = Resources.Load<Sprite>( "UI/Images/default" );
            _name = "NoName";
        }

        public virtual Action AddActionToPerso(GameObject go)
        { 
            Debug.Log("MustBe Implemented");
            return null;
        }

        public virtual void Start () { }

        public virtual bool Act () 
        {
            if(_isEnable)
            {
                if ( _cooldown != 0f )
                {
                    _isEnable = false;
                    Invoke( "EnableAttack", _cooldown );
                }
                return true;
            }
            return false;
        }
        private void EnableAttack()
        {
            _isEnable = true;
        }

        // TODO : remettre l'origine de l'attaque au bon endroit
		// TODO donner des parametres à la methode, exemple :  Cercle, cone... et amplitude.
        internal virtual List<Character> GetListOfTarget ()
        {
            List<Character> _targets = new List<Character>();
            Vector3 realAttackOrigin = new Vector3( _character.transform.position.x, _character.transform.position.y + 1, _character.transform.position.z );

            foreach ( Vector3 vector in _attackVectors )
            {
				Ray ray = new Ray(realAttackOrigin, _character.transform.TransformDirection( vector )*2);
                Debug.DrawRay( ray.origin, ray.direction, Color.yellow, 1.0f );//TODO comment it
                if ( Physics.Raycast( ray, out _hit, _range ) )
                {
                    Character target = null;
                    target = _hit.transform.GetComponent<Character>();
                    if ( target != null && target.name != _character.name && !_targets.Contains(target))
                    {
                        _targets.Add( target );
                    }
                }
            }
            return _targets;
        }

        /*
        public void StartCoolDown ( Func<UISkill,float, bool> function, UISkill textButton )
        {
            _currentCoolDown = _cooldown;
            _function = function;
            _textButton = textButton;
            Invoke( "DecrementCoolDown", 0f );
        }

        private void DecrementCoolDown ()
        {
            _function( _textButton, _currentCoolDown-- );
            if ( _currentCoolDown >= 0 ) Invoke( "DecrementCoolDown", 1f );
        }
         *
         * */

        public virtual void StopAction() {}

    }
}