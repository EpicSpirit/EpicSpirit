using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace EpicSpirit.Game
{
    public class CinematicOnlyMove : Cinematic
    {
        [SerializeField]
        List<GameObject> _cameraPoints;
        [SerializeField]
        float _timeOfCinematicPoint;
        [SerializeField]
        List<string> _topSubtitles;
        [SerializeField]
        List<string> _bottomSubtitles;

        int _count;

        public List<GameObject> CameraPoints
        {
            get{ return _cameraPoints; }
            set{ _cameraPoints = value; }
        }
        public float TimeOfCinematicPoint
        {
            get{ return _timeOfCinematicPoint; }
            set{ _timeOfCinematicPoint = value; }
        }

        public override void LaunchCinematic ()
        {
            BlockEveryCharacter(true);
            _count = 0;

            for(int i = 0; i < CameraPoints.Count ; i++)
            {
                Invoke("MoveToNextPoint", TimeOfCinematicPoint * i);
            }
			Invoke("ReturnToPlayer", TimeOfCinematicPoint * CameraPoints.Count);
        }

        public void MoveToNextPoint()
        {
            if ( _count <= CameraPoints.Count )
            {
                BlackBars.BottomSubtitleText = _bottomSubtitles[_count];
                BlackBars.TopSubtitleText = _topSubtitles[_count];
                _cameraController.Move( CameraPoints[_count++], MoveCamera.MEDIUM );
            }
                
        }
        public void ReturnToPlayer()
        {
            BackCameraToPlayer();
            BlockEveryCharacter(false);
            if ( this.name == "End" ) SceneManager.LoadScene( "overworld" );
        }
    }
}
