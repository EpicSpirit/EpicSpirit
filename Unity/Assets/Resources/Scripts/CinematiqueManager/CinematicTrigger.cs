using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game {
    public class CinematicTrigger : MonoBehaviour {
        Cinematic _cinematic;
	    void Awake () {
            _cinematic = GetComponentInParent<Cinematic>();
	    }
        void OnTriggerEnter (Collider c)
        {
            if ( c.name == "Spi" )
                _cinematic.Begin();


            
        }
    }
}