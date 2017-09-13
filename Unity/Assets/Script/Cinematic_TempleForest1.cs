using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Cinematic_TempleForest1 : Cinematic
    {
        Cinematic_EndTrigger endTrigger;
        public GameObject _finalGate;

        public override void Awake ()
        {
            base.Awake();
        }

        public override void LaunchCinematic ()
        {
            var badBoySpawner = GameObject.Find( "SpawnnerBadBoy" );
            endTrigger = GetComponentInChildren<Cinematic_EndTrigger>();
            endTrigger.SetFunctionEndOfCinematic( EndOfCinematic );

            // On fait apparaitre les badboys et on les rajouter au trigger de fin de cinematique
            var listBadBoySpawner = badBoySpawner.GetComponentsInChildren<CinematicSpawnPoint>();
            foreach ( var spawner in listBadBoySpawner )
            {
                endTrigger.AddWatchers( spawner.Spawn() );
            }
        }

        public void EndOfCinematic ()
        {
            Destroy( _finalGate );
        }
    }
}
