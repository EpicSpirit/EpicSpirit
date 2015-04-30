using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class MoveCamera : MonoBehaviour
    {
        // GameObject that we have to follow
        public GameObject _target;

        /*
         * TODO: Rendre le code plus propre et générique (gérer notament la rotation de la caméra)
         * */
        void Update()
        {
            if ( _target != null )
            {
                Vector3 movement = this.transform.position;

                movement.x = _target.transform.position.x;
                movement.z = _target.transform.position.z - 10;
                movement.y = _target.transform.position.y + 15;

                this.transform.position = movement;
            }


        }
    }
}