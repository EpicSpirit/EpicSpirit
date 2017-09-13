using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Cinematic_Coffre : Cinematic
    {
        new MoveCamera camera;

        public override void LaunchCinematic ()
        {
            BlockEveryCharacter( true );
            var pointCamera = GetComponentInChildren<Cinematic_CameraPoint>();

            camera = GameObject.Find( "Camera" ).GetComponent<MoveCamera>();
            pointCamera.Prepare( camera, 0f );
            BlackBars.TopSubtitleText = "Gosh !";
            BlackBars.BottomSubtitleText = "A treasure chest in a forest ?";
            Invoke( "Etape1", 2f );
        }

        void Etape1()
        {
            var spi = GameObject.FindGameObjectWithTag( "Player" );
            camera._target = spi;
            BlackBars.EnableSubtitlesAndBlackBars = false;
            BlockEveryCharacter( false );
            
        }

    }
}