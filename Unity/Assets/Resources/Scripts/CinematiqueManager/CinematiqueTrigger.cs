using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game {
    public class CinematiqueTrigger : MonoBehaviour {
        Cinematique _cinematique;
	    void Awake () {
            _cinematique = GetComponentInParent<Cinematique>();
	    }
        void OnTriggerEnter (Collider c)
        {
            if ( c.name == "Spi" )
                _cinematique.Begin();


            
        }
    }
}