using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class GameMenuManager : MonoBehaviour
    {
        SaveManager _saveManager;

        List<Action> _actions;

        List<Weapon> _weapons;
        List<Skill> _skills;
        List<Item> _items;
        public enum TypeOfContent
        {
            Weapons,
            Skills,
            Items
        }
        TypeOfContent _typeOfContent;
        int _x, _y;
        float _offset;

        void Start ()
        {

            _saveManager = GameObject.FindWithTag( "SaveManager" ).GetComponent<SaveManager>();
            _weapons = _saveManager.LoadAllUnlockWeapons();
            _items = _saveManager.LoadAllUnlockItems();
            _skills = _saveManager.LoadAllUnlockSkills();

            _x = 0;
            _y = 0;
            _offset = 1;

            WeaponMenu();
        }

        public void RefreshMenu()
        {
            // Destroy all ActionIcon
            foreach ( GameObject actionIcon in GameObject.FindGameObjectsWithTag( "ActionIcon" ))
            {
                GameObject.Destroy(actionIcon);
            }

            // Refresh
            switch ( _typeOfContent )
            {
                case TypeOfContent.Weapons:
                    foreach ( Weapon weapon in _weapons )
                    {
                        AddIcon( weapon );
                    }
                    break;
                case TypeOfContent.Items:
                    foreach ( Skill skill in _skills )
                    {
                        AddIcon( skill );
                    }
                    break;
                case TypeOfContent.Skills:
                    foreach ( Item item in _items )
                    {
                        AddIcon( item );
                    }
                    break;
                default:
                    Debug.Log("Probleme TypeOfContent");
                    break;


            }
            
        }

        // TODO : modifier le boutton ensuite
        public void AddIcon(Action action)
        {
            Debug.Log("blou");
            Instantiate( Resources.Load<GameObject>( "UI/GameMenu/ActionIcon" ), new Vector3( _x * _offset, _y * _offset, 0 ), new Quaternion() );
            

            if( ++_x >= 3)
            {
                _x = 0;
                _y++;
            }
        }

        

        public void WeaponMenu()
        {
            _typeOfContent = TypeOfContent.Weapons;
            RefreshMenu();
        }
        public void ItemMenu ()
        {
            _typeOfContent = TypeOfContent.Items;
            RefreshMenu();

        }
        public void SkillMenu ()
        {
            _typeOfContent = TypeOfContent.Skills;
            RefreshMenu();
        }
    }
}