using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace EpicSpirit.Game
{
    public abstract class Cinematique : MonoBehaviour
    {
        public bool _oneShot;
        bool _asBegin;
        internal MoveCamera _camera;


        public void Awake ()
        {
            _asBegin = false;
            _camera = GameObject.Find( "Camera" ).GetComponent<MoveCamera>();
        }

        public abstract void LaunchCinematique ();

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