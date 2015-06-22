using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game 
{
    public class Cinematic_TEST : Cinematic
    {

        public override void LaunchCinematic ()
        {
            var tab = GetComponentsInChildren<CinematicSpawnPoint>();
            foreach(var csp in tab)
            {
                csp.Spawn();
            }
        }
    }
}