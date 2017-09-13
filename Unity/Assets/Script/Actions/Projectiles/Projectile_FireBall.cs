using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Projectile_FireBall : Projectile
    {
        ParticleSystem[] particuleSystems;

        public override void Awake ()
        {
            base.Awake();
            _speed = 50f;
            _autoDestroyDelay = 0.5f;
        }

        public override void Start ()
        {
            base.Start();
            particuleSystems = GetComponentsInChildren<ParticleSystem>();
            particuleSystems[0].Play();
            particuleSystems[1].Play();
        }
        public override void Effect (Character target)
        {
            particuleSystems [1].Stop();
            particuleSystems [2].Play();
            target.takeDamage( 3, Action );
            if(target.AllowMoveBack)
                target.MoveBack( this.gameObject, 100 );
        }
    }
}
