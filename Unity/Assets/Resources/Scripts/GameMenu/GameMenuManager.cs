using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class GameMenuManager : MonoBehaviour
    {
        SaveManager _saveManager;

        List<Action> _actions;

        List<Weapon> _weapons;
        List<Skill> _skills;
        List<Item> _items;

        public GameObject target;

        public enum TypeOfContent
        {
            Weapons,
            Skills,
            Items
        }
        TypeOfContent _typeOfContent;
        int _x, _y;
        float _offset;
        int _beginX, _beginY;

        void Start ()
        {

            _saveManager = GameObject.FindWithTag( "SaveManager" ).GetComponent<SaveManager>();
            _weapons = _saveManager.LoadAllUnlockWeapons();
            _items = _saveManager.LoadAllUnlockItems();
            _skills = _saveManager.LoadAllUnlockSkills();

            _x = 0;
            _y = 0;
            _offset = 80;
            _beginY = 280;
            _beginX = 80;

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
                        Debug.Log("Adding Weapon");
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
            GameObject gameObject = (GameObject)Instantiate( Resources.Load<GameObject>( "UI/GameMenu/ActionIcon" ), new Vector3( _beginX +( _x * _offset), _beginY + (_y * _offset), 0 ), new Quaternion() );
            gameObject.transform.parent = target.transform;
            gameObject.transform.localScale = new Vector3(5,5,5);
            var ai = gameObject.GetComponent<actionicone>();
            ai.Action = action;
            var b = gameObject.GetComponent<Button>();
            b.onClick.AddListener( () => GetDescription(ai));
            

            if( ++_x >= 3)
            {
                _x = 0;
                _y++;
            }
        }
        private void GetDescription( actionicone ai)
        {
            Debug.Log(ai.Action.Name);
            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = ai.Action.Name;
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = ai.Action.Description;
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