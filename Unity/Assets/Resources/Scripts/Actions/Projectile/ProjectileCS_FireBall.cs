using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class ProjectileCS_FireBall : ProjectileCS
    {
        public override void Effect (Character target)
        {
            GetComponentsInChildren<ParticleSystem>() [1].Stop();
            GetComponentsInChildren<ParticleSystem>() [2].Play();
            target.takeDamage( 3 );
            target.MoveBack( this.gameObject, 100 );
        }
    }
}