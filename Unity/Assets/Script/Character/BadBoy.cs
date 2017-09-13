using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class BadBoy : Enemy
    {
        Action attack;

        public override void Awake ()
        {
            base.Awake();
            if(_aggroAreaAfterFirstAggro == 0)
                _aggroAreaAfterFirstAggro = _aggroArea + 5;
            _currentHealth = 3;
            _actions.Add( this.gameObject.AddComponent<Impulse>() );

        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update ()
        {
            base.Update();
        }

        //Todo : Même code que VeryBadBoy, rendre ça Dry avec une méthode générique ?
        public override void Move( Vector3 direction )
        {
            base.Move( direction );
            if ( isState( States.Idle ) && !IsSleeping )
            {
                AnimationManager( "idle" );
            }
            else if ( isState( States.Walk ) )
            {
                AnimationManager( "walk" );
            }
        }
        internal override void takeDamage ( int force, Action actionAttacker )
        {
            base.takeDamage( force, actionAttacker );

            if ( isState( States.Damaged ) )
            {
                AnimationManager( "damaged" );
            }
        }
        internal override void Die()
        {
            Loot( (UnityEngine.Object)UnityEngine.Resources.Load<UnityEngine.Object>( "CollectableItem/LowHealthPotion_Item" ) );
            base.Die();
        }
    }
}
