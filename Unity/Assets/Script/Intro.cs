using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class Intro : MonoBehaviour
    {
        Image _imageIntro;
        public void Awake()
        {
            _imageIntro = GetComponent<Image>();
        }
        public void Start()
        {
            Invoke( "Hide", 21f );
        }

        private void Hide()
        {
            GameObject.Destroy( _imageIntro );
        }
    }
}
