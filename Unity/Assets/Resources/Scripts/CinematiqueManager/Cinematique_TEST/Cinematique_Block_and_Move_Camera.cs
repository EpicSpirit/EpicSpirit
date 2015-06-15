using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Cinematique_Block_and_Move_Camera : Cinematique
    {
        public List<GameObject> camerapoints;
        public GameObject VeryBadBoyPlace;

        public override void LaunchCinematique ()
        {
            BlockEveryCharacter(false);
            
            _camera.Move( camerapoints[0],MoveCamera.MEDIUM);
            Invoke( "SpawnVeryBadBoy", 1f );

        }

        public void SpawnVeryBadBoy()
        {
            Instantiate( ( UnityEngine.Object ) UnityEngine.Resources.Load<UnityEngine.Object>( "Characters/Prefab/VeryBadBoy" ), VeryBadBoyPlace.transform.position, VeryBadBoyPlace.transform.rotation );
            Invoke("ReturnToSpi",1f);
        }
        public void ReturnToSpi()
        {
            BackCameraToSpi();
            BlockEveryCharacter(true);
        }
    }
}