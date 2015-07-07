using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Cinematic_CameraPoint : MonoBehaviour
    {
        MoveCamera _camera;
        GameObject _target;

        public void Prepare(MoveCamera camera, float timeInvoke)
        {
            Prepare( camera, timeInvoke, this.gameObject );
        }
        public void Prepare ( MoveCamera camera, float timeInvoke, GameObject target )
        {
            _camera = camera;
            _target = target;
            Invoke( "LauchMove", timeInvoke );
        }

        public void LauchMove()
        {
            _camera._target = _target;
        }
        
    }
}