using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class ForestWall : Character
    {
        public override void Awake()
        {
 	         base.Awake();
             CurrentHealth = 1;
             _allowMoveBack = false;
        }
        internal override void takeDamage ( int force, Action actionAttacker )
        {
            if(actionAttacker is FireBall)
            {
                CurrentHealth = 0;
            }

        }

    }
}