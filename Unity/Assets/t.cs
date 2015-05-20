using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game {
    public class t : MonoBehaviour {

        Button b;
        public int _indice;
        public Character target;


	    // Use this for initialization
	    void Start () {
            b.image = target.GetAttack(_indice).GetImage;

            b = this.GetComponent<Button>();
            b.onClick.AddListener( () => target.Attack(_indice) );
	    }

    }
}