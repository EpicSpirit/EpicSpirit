using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class HealthHUD : MonoBehaviour
    {
		Text _hp;

        void Start()
        {
			_hp = gameObject.AddComponent<Text>();
			_hp.fontSize = 56;
			_hp.fontStyle = FontStyle.Bold;
			_hp.font = (Font)Resources.Load("UI/BLKCHCRY");
			_hp.color = Color.red;
			// Pourquoi il me sort 52530 à la place de 206 ? wtfff putain omg fils de pute !
			// bon ici gros problème à résoudre. Si je mets n'importe quelle couleur il va me sortir un rouge vif.
			// Exemple new Color(191,0,0) new Color -10,0,0) c'est pareil.
			// Moi je veux ça exactement : (200,0,0)
        }
        void Update()
        {
            _hp.text = "HP : " + GameObject.FindWithTag( "Player" ).GetComponent<Character>().Health.ToString();
        }
    }
}