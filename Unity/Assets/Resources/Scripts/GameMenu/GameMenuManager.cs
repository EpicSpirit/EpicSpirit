using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace EpicSpirit.Game
{
    public class GameMenuManager : MonoBehaviour
    {
        SaveManager _saveManager;

        List<Action> _actions;

        List<Weapon> _weapons;
        List<Skill> _skills;
        List<Item> _items;
        actionicone _selectedAction;

        // TODO : Rendre ça propre ensuite
        public GameObject description;
        public GameObject slot_1;
        public GameObject slot_2;
        public GameObject slot_3;
        public GameObject cache;
        public GameObject target;
        public GameObject origin;

        public enum TypeOfContent
        {
            Weapons,
            Skills,
            Items
        }
        TypeOfContent _typeOfContent;
        int _x, _y;
        float _xOffset;
        float _yOffset;

        void Awake ()
        {
            _selectedAction = null;
            _saveManager = GameObject.FindWithTag( "SaveManager" ).GetComponent<SaveManager>();
            _weapons = _saveManager.LoadAllUnlockWeapons();
            _items = _saveManager.LoadAllUnlockItems();
            _skills = _saveManager.LoadAllUnlockSkills();
            _typeOfContent = TypeOfContent.Weapons;

            _x = 0;
            _y = 0;
            _xOffset = Screen.width / 5f;
            _yOffset = Screen.height / 3.8f;
        }

        void Start ()
        {
            WeaponMenu();
        }

        public void RefreshMenu()
        {
            _x = 0;
            _y = 0;
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
                case TypeOfContent.Skills:
                    foreach ( Skill skill in _skills )
                    {
                        AddIcon( skill );
                    }
                    break;
                case TypeOfContent.Items:
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

        public void AddIcon(Action action)
        {
            GameObject gameObject = (GameObject)Instantiate( Resources.Load<GameObject>( "UI/GameMenu/ActionIcon" ), new Vector3( target.transform.position.x +( _x * _xOffset), target.transform.position.y - (_y * _yOffset), 0 ), new Quaternion() );
            gameObject.transform.parent = target.transform;
            Debug.Log( "parent" + target.transform.position );
            Debug.Log( "target" + gameObject.transform.position );

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
            cache.SetActive( false );

            _selectedAction = ai;
            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = ai.Action.Name;
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = ai.Action.Description;
        }
        
        public void AddToSlot(int index)
        {
            var p = GameObject.Find( "ProgressionManager" ).GetComponent<ProgressionManager>();

            string key;
            int value;

            if ( _selectedAction.Action is Weapon )
            {
                key = "ActualWeapon";
                value = p.Weapons.IndexOf( (Weapon)_selectedAction.Action );
            }
            else if ( _selectedAction.Action is Item )
            {
                key = "ActualItem";
                value = p.Items.IndexOf( (Item)_selectedAction.Action );
            }
            else if ( _selectedAction.Action is Skill )
            {
                key = "ActualSkill_" + index;
                value = p.Skills.IndexOf( (Skill)_selectedAction.Action );
            }
            else
            {
                Debug.LogException(new Exception("action must be Weapon,Item or Skill"));
                key = "";
                value = -1;
            }

            PlayerPrefs.SetInt(key,value);
            Debug.Log( key + " => " + value );

        }

        public void WeaponMenu()
        {
            _typeOfContent = TypeOfContent.Weapons;
            cache.SetActive( true );
            slot_1.SetActive( false );
            slot_3.SetActive( false );

            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = "";
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = "";

            slot_2.GetComponentInChildren<Text>().text = "Slot 1";

            RefreshMenu();
        }
        public void ItemMenu ()
        {
            Debug.Log( "aaa" );
            _typeOfContent = TypeOfContent.Items;
            cache.SetActive( true );

            slot_1.SetActive( false );
            slot_3.SetActive( false );
            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = "";
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = "";

            slot_2.GetComponentInChildren<Text>().text = "Slot 1";

            RefreshMenu();
        }
        public void SkillMenu ()
        {
            cache.SetActive( true );

            _typeOfContent = TypeOfContent.Skills;
            slot_1.SetActive( true );
            slot_3.SetActive( true );
            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = "";
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = "";

            slot_2.GetComponentInChildren<Text>().text = "Slot 2";


            RefreshMenu();
        }

        public void ExitMenu ()
        {
            Application.LoadLevel( "overworld" );
        }

    }
}