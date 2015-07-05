using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class CinematicTrigger : MonoBehaviour 
	{
		public Cinematic _cinematic;
		void Awake () 
		{
			if( _cinematic == null )
            _cinematic = GetComponentInParent<Cinematic>();

            BoxCollider collider = GetComponent<BoxCollider>();
            if(collider !=null) collider.isTrigger = true;
	    }
        void OnTriggerEnter (Collider collider)
        {
			if ( collider.tag == "Player" )
                _cinematic.Begin();      
        }
    }
}