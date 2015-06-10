using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class CollectableItem : MonoBehaviour {

        public Item item;

	    void Awake () {
            Invoke( "Extinction", 5f );
	    }
	
	    void Update () {
            this.transform.RotateAround( Vector3.up, 2*Time.deltaTime );
	    }

        public void OnTriggerEnter()
        {
            SaveManager.AddItem( item );
            UpdateItemButton();
            Extinction();
        }

        public void Extinction ()
        {
            GameObject.Destroy( this.gameObject );
        }

        public void UpdateItemButton()
        {
            GameObject.Find( "Item_Button" ).GetComponentInChildren<UIItem>().UpdateCount();
        }
    }
}