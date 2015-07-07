using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	public class CameraFollow : MonoBehaviour 
	{
		public GameObject _target;
		public int _yOffset = 10;

        void Start()
        {
            if ( _target == null )
                _target = GameObject.FindGameObjectWithTag( "Player" );
        }

		void Update () 
		{
				if ( _target != null )
				{
					Vector3 movement = this.transform.position;
					
					movement.x = _target.transform.position.x;
					movement.y = _target.transform.position.y + _yOffset;
					movement.z = _target.transform.position.z;
					
					this.transform.position = Vector3.Lerp( this.transform.position, movement, 0.4f );
				}
		
		}
	}
}