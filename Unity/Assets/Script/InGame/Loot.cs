using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Loot : MonoBehaviour 
    {

        public Item item;
        public bool enableCollect;

        public virtual void Awake () 
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
            this.transform.Rotate( Vector3.up, 2*Time.deltaTime );
	    }


        public virtual void OnTriggerStay(Collider collider)
        {

            if ( enableCollect && collider.tag == "Player")
            {
                SaveManager.AddItem( item );

                var saveManager = GameObject.Find( "SaveManager" ).GetComponent<SaveManager>();

                if ( !saveManager.IsItemUnlock( item ) )
                {
                    Debug.Log( "Unlocking Item "+item.Name );
                    saveManager.UnlockItem( item );
                    SaveManager.SetIconAttack(SaveManager.IconType.ActualItem, item);
                    // Solution temporaire :
                    GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>().AddAction( item, 1 );
                    GameObject.Find("Actions").GetComponent<UIManager>().EnableAction(1);

                }

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
            UIAction uiAction;
            if ( item != null && ( uiAction = item.GetComponent<UIAction>() ) != null )
                uiAction.UpdateCount();
        }
    }
}