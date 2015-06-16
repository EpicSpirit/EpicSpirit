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
            if (c.tag == "Player") 
			{
				_cinematique.Begin ();
				if (_cinematique._oneShot)
				{
					Destroy(gameObject);
				}
			}

            
        }
    }
}