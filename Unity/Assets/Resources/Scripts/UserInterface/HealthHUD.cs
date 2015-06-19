using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class HealthHUD : MonoBehaviour
    {
		Image _hp;
        Character _character;

        void Awake()
        {
            _hp = GetComponent<Image>();
            _character = GameObject.FindWithTag( "Player" ).GetComponent<Character>();

        }
        void Update()
        {
            _hp.fillAmount = (0.9f * (float)_character.CurrentHealth / (float)_character.MaxHealth) + 0.1f;
        }
    }
}