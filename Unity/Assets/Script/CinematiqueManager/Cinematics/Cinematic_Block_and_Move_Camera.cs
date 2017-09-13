using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Cinematic_Block_and_Move_Camera : Cinematic
    {
        public List<GameObject> camerapoints;
        public GameObject VeryBadBoyPlace;

        public override void LaunchCinematic ()
        {
            BlockEveryCharacter(true);
            
            _cameraController.Move( camerapoints[0],MoveCamera.MEDIUM);
            Invoke( "SpawnVeryBadBoy", 1f );

        }

        public void SpawnVeryBadBoy()
        {
            Instantiate( ( UnityEngine.Object ) UnityEngine.Resources.Load<UnityEngine.Object>( "Characters/Prefab/VeryBadBoy" ), VeryBadBoyPlace.transform.position, VeryBadBoyPlace.transform.rotation );
            Invoke("ReturnToPlayer",1f);
        }
        public void ReturnToPlayer()
        {
            BackCameraToPlayer();
            BlockEveryCharacter(false);
        }
    }
}
