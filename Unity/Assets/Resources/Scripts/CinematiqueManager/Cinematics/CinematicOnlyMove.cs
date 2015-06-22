using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class CinematicOnlyMove : Cinematic
    {
        [SerializeField]
        List<GameObject> _cameraPoints;
        [SerializeField]
        float _timeOfCinematicPoint;

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
            Invoke("ReturnToSpi", TimeOfCinematicPoint * CameraPoints.Count);
        }

        public void MoveToNextPoint()
        {
            if(CameraPoints.Count >= _count)
                _camera.Move(CameraPoints[_count++], MoveCamera.MEDIUM);
        }
        public void ReturnToSpi()
        {
            BackCameraToSpi();
            BlockEveryCharacter(false);
        }
    }
}
