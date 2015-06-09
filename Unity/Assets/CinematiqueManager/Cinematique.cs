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

    }

}