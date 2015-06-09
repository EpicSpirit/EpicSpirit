using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace EpicSpirit.Game
{
    public abstract class Cinematique : MonoBehaviour
    {
        public bool _oneShot;
        bool _asBegin;

        public void Awake ()
        {
            _asBegin = false;
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


        // TODO : PoC, rendre ça beau et reflechit ^^
        internal void BlockEveryCharacter(bool value)
        {
            // Desactive tout les ennemis
            var t = FindObjectsOfType<Character>();
            foreach(var t_ in t)
            {
                t_.enabled = value;
            }

            // Desactive toute l'UI
            var a = GameObject.FindGameObjectWithTag( "UI" );
            var b = a.GetComponentsInChildren<MonoBehaviour>();
            foreach(var b_ in b)
            {
                if ( b_ != null )
                {
                    b_.enabled = value;
                }
            }

            // Desactive la gestion des touches
            var c = GameObject.Find( "Controller" ).GetComponent<PlayerController>().enabled = value; 
            
            // Met à Spi l'Idle
            GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>().AnimationManager( "idle" );
        }

        internal void MoveCamera(GameObject target)
        {
            var camera = GameObject.Find( "Camera" ).GetComponent<MoveCamera>();
            camera._cameraSpeed = 0.1f;
            camera._target = target;
        }

        // TODO : RENDRE CA BEAU 
        internal void BackCameraToSpi()
        {
            var camera = GameObject.Find( "Camera" ).GetComponent<MoveCamera>();
            var spi = GameObject.Find( "Spi" );
            camera._target = spi;
            camera._cameraSpeed = 0.4f;
        }



    }

}