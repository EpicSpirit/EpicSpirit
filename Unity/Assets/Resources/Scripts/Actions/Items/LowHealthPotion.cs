using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class LowHealthPotion : HealthPotion
    {
        public override void Awake()
        {
            base.Awake();
            _name = "Low Health Potion";
            _description = "Restaure a small amount of hp.";
            HealingAmount = 2;
            _image = Resources.Load<Sprite>( "UI/Images/button_health_potion" );
        }
        public override Action AddActionToPerso( GameObject go )
        {
            return go.AddComponent<LowHealthPotion>();
        }
    }
}