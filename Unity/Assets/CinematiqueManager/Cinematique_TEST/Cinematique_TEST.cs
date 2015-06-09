using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game {
    public class Cinematique_TEST : Cinematique {

        public override void LaunchCinematique ()
        {
            var tab = GetComponentsInChildren<CinematiqueSpawnPoint>();
            foreach(var csp in tab)
            {
                csp.Spawn();
            }
        }
    }
}