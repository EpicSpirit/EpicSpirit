using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public abstract class Projectile : MonoBehaviour
    {
        Vector3 _direction;
        Character _source;
        internal float _speed;
        internal float _autoDestroyDelay;
        Action _action;

        public Action Action
        {
            get { return _action; }
            set { _action = value; }
        }
        Character Source
        {
            get { return _source;}
            set { _source=value; }
        }
        public static void Create (GameObject gameobject ,Character source, Action action)
        {
            Projectile projectile = GameObject.Instantiate<GameObject>( gameobject ).GetComponent<Projectile>();
            projectile.transform.position = source.transform.position;
            projectile.transform.rotation = source.transform.rotation;
            projectile.Source = source;
            projectile.Action = action;
        }

        public virtual void Awake()
        {
            _direction = Vector3.forward;
            _speed = 20f;
            _autoDestroyDelay = 1f;
        }

        public virtual void Start()
        {
            Invoke( "AutoDestroy", _autoDestroyDelay );
        }

        public virtual void Update ()
        {
            this.transform.Translate( _direction * _speed *Time.deltaTime, Space.Self);
        }

        public void OnTriggerEnter(Collider collider)
        {
            Character target;
            if ( collider != _source.CharacterController && (target = collider.GetComponent<Character>()) != null)
            {
                Effect( target );
                CancelInvoke( "AutoDestroy" );
                Destroy( this.gameObject, 0.15f );
            }
        }

        public void AutoDestroy ()
        {
            Destroy( this.gameObject );
        }

        public abstract void Effect (Character target);
        
    }
}
