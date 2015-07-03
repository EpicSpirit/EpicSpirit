using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Cinematique_Wall_Forest1 : Cinematic
    {
        public override void LaunchCinematic ()
        {
            var pointCamera = GetComponentInChildren<Cinematic_CameraPoint>();
            pointCamera.Prepare( _cameraController, 0f );
            BlackBars.EnableBlackBars = true;
            BlackBars.TopSubtitleText = "Tiens, je ne peux pas passer";
            BlackBars.BottomSubtitleText = "Il existe ptet un moyen de passer";
            Invoke( "Etape1", 2f );
        }
        void Etape1()
        {
            BlackBars.EnableSubtitlesAndBlackBars = false;
            _cameraController._target = _player;
        }
    }
}