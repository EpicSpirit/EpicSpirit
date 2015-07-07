using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class TempleSwitchPole : TempleSwitch
	{


        internal override void takeDamage ( int force, Action actionAttacker )
		{
			if (_isOn == false) 
			{
				_isOn = true;
			} 
			else
			{
				_isOn = false;
			}

			foreach( GameObject gate in Gates )
			{
				if( gate.activeSelf )
				{
					gate.SetActive(false);
				}
				else
				{
					gate.SetActive(true);
				}
			}
            base.takeDamage(force, actionAttacker);
		}

	}
}