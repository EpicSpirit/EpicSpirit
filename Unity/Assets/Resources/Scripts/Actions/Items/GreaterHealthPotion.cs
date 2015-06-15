using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class GreaterHealthPotion : HealthPotion
    {
        public override void Awake()
        {
            base.Awake();
            _name = "Greater Health Potion";
            _description = "Restaure a large amount of hp.";
            HealingAmount = 20;
            _image = Resources.Load<Sprite>( "UI/Images/button_health_potion" );
        }

        public override Action AddActionToPerso( GameObject go )
        {
            return go.AddComponent<GreaterHealthPotion>();
        }
    }
}