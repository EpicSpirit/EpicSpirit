using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game 
{
    public class  ProgressionManager : MonoBehaviour 
    {
        List<Skill> _skills;
        List<Weapon> _weapons;
        List<Item> _items;

        public List<Skill> Skills { get { return _skills; } }
        public List<Weapon> Weapons { get { return _weapons; } }
        public List<Item> Items { get { return _items; } }

        public void CustomStart () 
        {
            Debug.Log( "ProgressionManager START" );

            _skills = new List<Skill>();
            _weapons = new List<Weapon>();
            _items = new List<Item>();

            #region Weapons 
            _weapons.Add( this.gameObject.AddComponent<Sword>() );
            _weapons.Add( this.gameObject.AddComponent<Excalibur>() );
            _weapons.Add( this.gameObject.AddComponent<Masamune>() );
            #endregion

            #region Items
            _items.Add( this.gameObject.AddComponent<HealthPotion>() );
            #endregion

            #region Skills
            _skills.Add(this.gameObject.AddComponent<FireBall>());
            #endregion

        }


    }
}