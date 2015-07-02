using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Loot : MonoBehaviour 
    {

        public Item item;
        public bool enableCollect;
        
	    void Awake () 
        {
            enableCollect = false;

            Invoke( "Extinction", 20f );
            Invoke( "EnableCollect", 1.0f );
	    }

        void EnableCollect ()
        {
            enableCollect = true;
        }

	    void Update () 
        {
            this.transform.RotateAround( Vector3.up, 2*Time.deltaTime );
	    }

        public void OnTriggerStay(Collider collider)
        {
            if ( enableCollect && collider.tag == "Player")
            {
                Debug.Log("Collected");
                SaveManager.AddItem( item );
                UpdateItemButton();
                Extinction();
            }
        }

        public void Extinction ()
        {
            GameObject.Destroy( this.gameObject );
        }

        public void UpdateItemButton()
        {
            var item =  GameObject.Find( "Item" );
            if(item != null)
                item.GetComponentInChildren<UIItem>().UpdateCount();
        }
    }
}