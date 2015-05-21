using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game {
    public class t : MonoBehaviour {

        Button b;
        public int _indice;
        public Character target;
        bool a=false;

	    void Update () 
        {
            if ( target.GetAttack( _indice ).GetSprite != null )
            {
                b = this.GetComponent<Button>();
                b.image.overrideSprite = target.GetAttack( _indice ).GetSprite;
                b.onClick.AddListener( () => target.Attack( _indice ) );

                this.enabled = false;
               
            }
            
	    }

    }
}