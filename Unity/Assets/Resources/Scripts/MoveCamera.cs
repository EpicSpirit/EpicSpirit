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
        /*
         * TODO: Rendre le code plus propre et générique (gérer notament la rotation de la caméra)
         * */
        void Update()
        {
            if ( _target != null )
            {
                Vector3 movement = this.transform.position;

                movement.x = _target.transform.position.x+x_delta;
                movement.z = _target.transform.position.z +z_delta;
                movement.y = _target.transform.position.y + y_delta;

                //this.transform.position = movement;
                this.transform.position = Vector3.Lerp( this.transform.position, movement, 0.4f );
            }


        }
    }
}