using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class MiddleHealthPotion : HealthPotion
    {
        public override void Awake()
        {
            base.Awake();
            _name = "Middle Health Potion";
            _description = "Restaure a middle amount of hp.";
            HealingAmount = 5;
            _image = Resources.Load<Sprite>( "UI/Images/Potion2" );
        }

        public override Action AddActionToPerso( GameObject go )
        {
            return go.AddComponent<MiddleHealthPotion>();
        }
    }
}
