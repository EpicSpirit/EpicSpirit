using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Cinematique_Forest_1 : Cinematic
    {
        Cinematic_EndTrigger endTrigger;
        CinematicSpawnPoint[] _listWallSpawner;

        public override void Awake()
        {
            base.Awake();
        }

        public override void LaunchCinematic ()
        {
            var badBoySpawner = GameObject.Find( "SpawnnerBadBoy" );
            var WallSpawner = GameObject.Find( "SpawnerWall" );
            endTrigger = GetComponentInChildren<Cinematic_EndTrigger>();
            endTrigger.SetFunctionEndOfCinematic( EndOfCinematic );

            // On fait apparaitre les badboys et on les rajouter au trigger de fin de cinematique
            var listBadBoySpawner = badBoySpawner.GetComponentsInChildren<CinematicSpawnPoint>();
            foreach ( var spawner in listBadBoySpawner )
            {
                endTrigger.AddWatchers(spawner.Spawn());
            }


            // On fait apparaitre les murs
            _listWallSpawner = WallSpawner.GetComponentsInChildren<CinematicSpawnPoint>();
            foreach ( var spawner in _listWallSpawner )
            {
                spawner.Spawn();
            }

            // On bouge la caméra pour voir tout les badboys
            var listCameraPoint = GetComponentsInChildren<Cinematic_CameraPoint>();
            Cinematic_CameraPoint camera;
            for ( var i=0; i < listCameraPoint.Length; i++ )
            {
                camera = listCameraPoint [i];

                if ( i == listCameraPoint.Length - 1 )
                    camera.Prepare( _cameraController, i * 1, _player );
                else
                    camera.Prepare( _cameraController, i * 1 );
            }

        }

        public void EndOfCinematic()
        {
            Debug.Log("EndOfCinematic");
            Debug.Log( _listWallSpawner );
            foreach(var wallSpawner in _listWallSpawner)
            {
                Debug.Log("destroy "+wallSpawner.name);
                Destroy(wallSpawner.Instance);
            }
        }

    }
}