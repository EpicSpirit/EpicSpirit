using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Item : Action
    {

        public override void Awake ()
        {
            base.Awake();
        }

        public override void Start () { 
            base.Start();
        }

        public int Quantity
        {
            get
            {
                return SaveManager.GetItemQuantity( this );
            }
        }
        public void Add () 
        {
            SaveManager.AddItem( this );
        }
        public void Remove () 
        {
            SaveManager.RemoveItem( this );
        }

        
    }
}
