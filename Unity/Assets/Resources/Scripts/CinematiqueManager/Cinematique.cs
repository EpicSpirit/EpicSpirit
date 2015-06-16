using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace EpicSpirit.Game
{
    public abstract class Cinematique : MonoBehaviour
    {
        public bool _oneShot;
        bool _asBegin; // has began ? :p
        internal MoveCamera _camera;
		[SerializeField]
		string _topSubtitles;
		[SerializeField]
		string _bottomSubtitles;
		[SerializeField]
		bool _allowBlackBars = true;

		public bool AllowBlackBars
		{
			get { return _allowBlackBars; }
			set { _allowBlackBars = value; }
		}

		public string TopSubTitles
		{
			get { return _topSubtitles; }
			set { _topSubtitles = value; }
		}

		public string BottomSubTitles
		{
			get { return _bottomSubtitles; }
			set { _bottomSubtitles = value; }
		}


        public void Awake ()
        {
            _asBegin = false;
            _camera = GameObject.Find( "Camera" ).GetComponent<MoveCamera>();
        }

        public virtual void LaunchCinematique ()
		{
			// Subtitles + black bars
		}

        public void Begin ()
        {
            if ( !_oneShot || ( _oneShot && !_asBegin) )
            {
                _asBegin = true;
                
                LaunchCinematique();
            }

        }

        internal void BlockEveryCharacter(bool value)
        {
            // Desactive tout les ennemis
            AIController[] allAIController = FindObjectsOfType<AIController>();
            foreach ( AIController aiController in allAIController )
            {
                aiController.enabled = value;
                aiController.GetComponent<Character>().AnimationManager( "idle" );
            }

            // Desactive toute l'UI
            GameObject ui = GameObject.FindGameObjectWithTag( "UI" );
            MonoBehaviour[] allBehaviour = ui.GetComponentsInChildren<MonoBehaviour>();
            foreach(var behaviour in allBehaviour)
            {
                if ( behaviour != null )
                {
                    behaviour.enabled = value;
                }
            }

            // Desactive la gestion des touches
            GameObject.Find( "Controller" ).GetComponent<PlayerController>().enabled = value;

            // Met à Spi l'Idle
            GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>().AnimationManager( "idle" );
        }

        internal void BackCameraToSpi()
        {
            var camera = GameObject.Find( "Camera" ).GetComponent<MoveCamera>();
            var spi = GameObject.Find( "Spi" );
            camera._target = spi;
            camera._cameraSpeed = 0.4f;
        }



    }

}