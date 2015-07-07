using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class EmptyItem : Item
    {
        public override void Awake ()
        {
            base.Awake();
            _image = Resources.Load<Sprite>( "UI/Images/button_empty" );
            _name = "";
            _description = "Vide";
        }

        public override Action AddActionToPerso ( GameObject go )
        {
            return go.AddComponent<EmptyItem>();
        }

        public override void Start ()
        {
            base.Start();
        }

        public override bool Act ()
        {
            return base.Act();
        }


        public void UpdatePosition ()
        {

        }
        public void StopUpdatePosition ()
        {

        }
    }
}
