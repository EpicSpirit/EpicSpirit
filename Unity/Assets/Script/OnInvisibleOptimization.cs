using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class OnInvisibleOptimization : MonoBehaviour
    {
        public MonoBehaviour _monoBehaviour;
        public static OnInvisibleOptimization Initialize ( MonoBehaviour monoBehaviour )
        {
            OnInvisibleOptimization addInRenderer=monoBehaviour.GetComponentInChildren<Renderer>().gameObject.AddComponent<OnInvisibleOptimization>();
            addInRenderer._monoBehaviour = monoBehaviour;
            return addInRenderer;
        }

        public void OnBecameVisible ()
        {
            _monoBehaviour.enabled = true;
        }
        public void OnBecameInvisible ()
        {
            _monoBehaviour.enabled = false;
        }
    }
}
