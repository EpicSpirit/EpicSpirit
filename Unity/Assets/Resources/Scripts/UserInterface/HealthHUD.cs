using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class HealthHUD : MonoBehaviour
    {
		Text _hp;
        Character _character;

        void Awake()
        {
			_hp = gameObject.AddComponent<Text>();
			_hp.fontSize = 56;
			_hp.fontStyle = FontStyle.Bold;
			_hp.font = (Font)Resources.Load("UI/BLKCHCRY");
			_hp.color = Color.red;
            _character = GameObject.FindWithTag( "Player" ).GetComponent<Character>();

        }
        void Update()
        {
            _hp.text = "HP : " + _character.Health.ToString();
        }
    }
}