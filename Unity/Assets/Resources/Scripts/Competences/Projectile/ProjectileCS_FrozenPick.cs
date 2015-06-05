using UnityEngine;
using System.Collections;
namespace EpicSpirit.Game
{
    public class ProjectileCS_FrozenPick : ProjectileCS
    {

        public override void Effect ( Character target )
        {
            GetComponentsInChildren<ParticleSystem>() [1].Stop();
            GetComponentsInChildren<ParticleSystem>() [2].Play();
            target.takeDamage( 2 );
            target.MoveBack( this.gameObject, 10 );
            target.Iced(5f);
        }
    }
}