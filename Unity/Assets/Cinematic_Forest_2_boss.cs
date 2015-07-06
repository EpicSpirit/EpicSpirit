using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Cinematic_Forest_2_boss : Cinematic
    {

        GameObject end;

        public override void Awake ()
        {
            base.Awake();
            end = GameObject.Find( "End" );
            end.SetActive( false );
        }

        public override void LaunchCinematic ()
        {
            BlockEveryCharacter( true );
            Invoke( "Etape1", 0f );
            Invoke( "Etape2", 4f ); // 4f la vraie valeur
        }

        void Etape1()
        {
            var pointCamera = GetComponentInChildren<Cinematic_CameraPoint>();
            pointCamera.Prepare( _cameraController, 0f );

            BlackBars.EnableSubtitlesAndBlackBars = true;
            BlackBars.TopSubtitleText = "Te voilà";
            BlackBars.BottomSubtitleText = "Ainsi ils ont envoyé un minuscule esprit. Pour se battre contre les Nellys c'est bien peu ...";
        }

        void Etape2 ()
        {
            _cameraController._target = _player;
            var spawnPoint = GetComponentInChildren<CinematicSpawnPoint>();
            spawnPoint.Spawn();
            BlackBars.EnableSubtitlesAndBlackBars = false;
            GameObject.Find( "VeryBadBoy" ).GetComponent<VeryBadBoyAI>().Figth();
            BlockEveryCharacter( false );
        }

        public void End()
        {
            end.SetActive( true );
        }


    }
}