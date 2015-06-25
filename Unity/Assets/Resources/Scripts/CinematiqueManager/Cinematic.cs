using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace EpicSpirit.Game
{
    public abstract class Cinematic : MonoBehaviour
    {
        public bool _oneShot;
        bool _asBegin;
        internal MoveCamera _cameraController;
		internal GameObject _camera;
		GameObject _player;

        public void Awake ()
        {
            _asBegin = false;
			_camera = GameObject.Find("Camera");
            _cameraController = _camera.GetComponent<MoveCamera>();
			_player = GameObject.FindWithTag ("Player");
        }

		public void Update()
		{
			if ( Input.GetKeyDown( KeyCode.A ) )
			{
				CancelInvoke();
				BlockEveryCharacter(false);
				BackCameraToPlayer();
			}

		}

        public abstract void LaunchCinematic ();

        public void Begin ()
        {
            if ( !_oneShot || ( _oneShot && !_asBegin) )
            {
                _asBegin = true;
                
                LaunchCinematic();
            }

        }

        internal void BlockEveryCharacter(bool value)
        {
            // Desactive tout les ennemis
            AIController[] allAIController = FindObjectsOfType<AIController>();
            foreach ( AIController aiController in allAIController )
            {
                aiController.enabled = !value;
                aiController.GetComponent<Character>().AnimationManager( "idle" );
            }

            // Desactive toute l'UI
            GameObject ui = GameObject.FindGameObjectWithTag( "UI" );
            MonoBehaviour[] allBehaviour = ui.GetComponentsInChildren<MonoBehaviour>();
            foreach(var behaviour in allBehaviour)
            {
                if ( behaviour != null )
                {
                    behaviour.enabled = !value;
                }
            }

            // Desactive la gestion des touches
            GameObject.Find( "Controller" ).GetComponent<PlayerController>().enabled = !value;

            // Met à Spi l'Idle
			_player.GetComponent<Character>().AnimationManager( "idle" );
        }

        internal void BackCameraToPlayer()
        {
            
			_cameraController._target = _player;
			_cameraController.CameraSpeed = 0.4f;
        }



    }

}