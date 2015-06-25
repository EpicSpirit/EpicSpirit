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
        public float _cameraSpeed;

        public const float SLOW= 0.05f;
        public const float MEDIUM=0.1f;
        public const float HIGH=0.4f;

        Vector3 _movement;

		// Zoom camera when raycast detect wall
		float _zOffset;
		RaycastHit _hitBackward;
		RaycastHit _hitForward;
        //Transform _originTransform; on peut utiliser transform.position.z - zoffset


		public float CameraSpeed 
		{
			get{ return _cameraSpeed; }
			set{ _cameraSpeed = value; }
		}

        void Start()
        {
            _cameraSpeed = 0.4f;
            _movement = this.transform.position;
            if ( _target == null ) _target = GameObject.Find( "Spi" );
        }

        public void Move(GameObject target,float speed)
        {
            _target = target;
            _cameraSpeed = speed;
        }
        /*
         * TODO: Rendre le code plus propre et générique (gérer notament la rotation de la caméra)
         * */
        void Update()
        {
            if ( _target != null )
            {
                #region new camera
                // enlever le && false pour passer dans ce code
				if( Application.loadedLevelName == "forest_temple" && false)
                {
                    _movement.x = _target.transform.position.x + x_delta;
                    _movement.z = _target.transform.position.z + z_delta;
                    _movement.y = _target.transform.position.y + y_delta;
                    RaycastHit hit;
                    // Si il y a un obj entre Spi et la cam, on avance la caméra
                    Debug.DrawRay( this.transform.position, _target.transform.position - this.transform.position, Color.red );
                    if(Physics.Raycast(new Ray(this.transform.position, _target.transform.position-this.transform.position),out hit))
                    {
                        if ( hit.collider.name.Contains( "Wall" ) )
                        {
                            _movement += ( hit.point - this.transform.position );
                        }
                    }
                    
                }
                #endregion
                #region Old Camera
                else
                {
                    _movement.x = _target.transform.position.x + x_delta;
                    _movement.z = _target.transform.position.z + z_delta;
                    _movement.y = _target.transform.position.y + y_delta;
                }
                #endregion

                this.transform.position = Vector3.Lerp( this.transform.position, _movement, 0.4f );
               
            }


        }
    }
}