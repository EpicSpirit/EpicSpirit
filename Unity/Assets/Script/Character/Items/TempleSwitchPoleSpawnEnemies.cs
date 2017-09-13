using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class TempleSwitchPoleSpawnEnemies : TempleSwitchPole
    {
        Cinematic_TempleForest1 _cinematic;

        public override void Awake()
        {
            _cinematic = GetComponentInChildren<Cinematic_TempleForest1>();
        }

        internal override void takeDamage ( int force, Action actionAttacker )
        {
            _cinematic.LaunchCinematic();
            base.takeDamage( force, actionAttacker );
        }
    }
}