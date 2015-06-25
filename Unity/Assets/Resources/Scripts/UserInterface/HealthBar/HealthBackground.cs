using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class HealthBackground : MonoBehaviour
    {
        Image _hp;

        void Awake()
        {
            _hp = gameObject.AddComponent<Image>();
            _hp.sprite = Resources.Load<Sprite>( "UI/Images/health_bar" );
            _hp.preserveAspect = true;
        }
    }
}