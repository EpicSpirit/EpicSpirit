using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Skill : Action
    {
        public override void Awake ()
        {
            base.Awake();
			_attackDuration = 0.1f;
            _cooldown = 0f;
        }
        

        
    }
}
