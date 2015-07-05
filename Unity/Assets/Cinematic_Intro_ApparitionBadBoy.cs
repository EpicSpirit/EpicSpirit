using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Cinematic_Intro_ApparitionBadBoy : Cinematic
    {
        public override void LaunchCinematic ()
        {
            BlockEveryCharacter( true );
            GameObject go;

            var listSpawnPoint = GetComponentsInChildren<CinematicSpawnPoint>();
            go = listSpawnPoint [0].Prefab;

            foreach(var spawnPoint in listSpawnPoint)
            {
                go = spawnPoint.Spawn();
                go.GetComponent<AIController>().Target=_player;
                var badboy = go.GetComponent<BadBoy>();
                badboy._aggroMovementSpeed = 20;
                badboy._attackSpeed = 1f;

            }

            BlackBars.EnableSubtitlesAndBlackBars = true;


            var cameraPoint = GetComponentInChildren<Cinematic_CameraPoint>();
            cameraPoint.Prepare( _cameraController, 0f, go );

            Invoke( "Etape1", 0f );
            Invoke( "Etape2", 0.5f );
            Invoke( "Etape3", 1f );
            Invoke( "Etape4", 1.5f );


        }

        public void Etape1 ()
        {
            BlackBars.TopSubtitleText = "Oh";
        }
        public void Etape2 ()
        {
            BlackBars.TopSubtitleText = "My";
        }
        public void Etape3 ()
        {
            BlackBars.TopSubtitleText = "Spirit !!!";
        }
        public void Etape4 ()
        {
            BlackBars.EnableSubtitlesAndBlackBars = false;
            _cameraController._target = _player;
            BlockEveryCharacter( false );
        }
        
    }
}