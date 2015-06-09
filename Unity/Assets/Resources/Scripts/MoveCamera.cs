using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class MoveCamera : MonoBehaviour
    {
        // GameObject that we have to follow
        public GameObject _target;
        public int x_delta;
        public int y_delta;
        public int z_delta;

		Vector3 _movement;

		// Zoom camera when raycast detect wall
		float _zOffset;
		RaycastHit _hitOrigin;
		RaycastHit _hitCurrent;

        /*
         * TODO: Rendre le code plus propre et générique (gérer notament la rotation de la caméra)
         * */
		void Start()
		{
			_movement = this.transform.position;
		}
        void Update()
        {
            if ( _target != null )
            {
				//new camera
				if( Application.loadedLevelName == "forest_temple" )
				{
					Physics.Raycast(transform.position, _target.transform.position - transform.position, out _hitCurrent);
					Physics.Raycast(transform.position - new Vector3( 0, 0, _zOffset + z_delta ), _target.transform.position - transform.position - new Vector3( 0, 0, _zOffset + z_delta ), out _hitOrigin);

					if(_hitCurrent.transform.name.Contains("Wall"))
					{
						Debug.Log(_hitCurrent.transform.name);
						_zOffset = (_hitCurrent.transform.position.z - transform.position.z) + 1;
						Debug.Log(_zOffset);
						_movement.x = _target.transform.position.x + x_delta;
						_movement.z = _target.transform.position.z + _zOffset;
						_movement.y = _target.transform.position.y + y_delta;
					}
					else if(!_hitOrigin.transform.name.Contains("Wall"))
					{
						Debug.Log("else");
						_movement.x = _target.transform.position.x + x_delta;
						_movement.z = _target.transform.position.z + z_delta;
						_movement.y = _target.transform.position.y + y_delta;
					}
				}

				// Old camera
				else 
				{
					_movement.x = _target.transform.position.x+x_delta;
					_movement.z = _target.transform.position.z +z_delta;
					_movement.y = _target.transform.position.y + y_delta;
				}
			
				this.transform.position = Vector3.Lerp( this.transform.position, _movement, 0.4f );

            }


        }
    }
}