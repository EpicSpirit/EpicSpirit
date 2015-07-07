using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class TempleSwitchStone : TempleSwitch
    {
        internal override void takeDamage ( int force, Action actionAttacker )
        {
            if ( _isOn == false )
            {
                _isOn = true;
                transform.Rotate( new Vector3( 90, 0, 0 ) );
            }
            else
            {
                _isOn = false;
                transform.Rotate( new Vector3( -90, 0, 0 ) );
            }

            foreach ( GameObject gate in Gates )
            {
                if ( gate.activeSelf )
                {
                    gate.SetActive( false );
                }
                else
                {
                    gate.SetActive( true );
                }
            }
            base.takeDamage( force, actionAttacker );
        }
    }
}

