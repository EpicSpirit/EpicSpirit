using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EpicSpirit.Game
{
    public class SaveManager : MonoBehaviour
    {
        ProgressionManager _progressionManager;

        public void Start () 
        {
            if (_progressionManager == null )
            {
                _progressionManager = this.gameObject.AddComponent<ProgressionManager>();
            }
            
        }
        public void SavSpi ( Spi spi )
        {
            PlayerPrefs.SetInt( "Spi_Health", spi.Health );
        }
        public void GetSavSpi ( Spi spi )
        {
            if ( PlayerPrefs.HasKey( "Spi_Health" ) )
            {
                spi.Health = PlayerPrefs.GetInt( "Spi_Health" );
            }
            else
            {
                Debug.LogError( "SaveManager, don't have Spi_Health Key on PlayerPrefs" );
            }
        }

        public List<Action> LoadAllSkills () 
        {

            return new List<Action>();
        }

        public void InitializeSkillList () 
        {
            string message ="";
            
            for ( int i=0; i < _progressionManager.Skills.Count; i++ )
            {
                message += "0";
            }
            PlayerPrefs.SetString( "Skills", message );
        }
        public void InitializeWeaponList ()
        {
            string message ="";
            for ( int i=0; i < _progressionManager.Weapons.Count; i++ )
            {
                message += "0";
            }
            PlayerPrefs.SetString( "Weapons", message );
        }
        public void InitializeItemList ()
        {
            string message ="";
            for ( int i=0; i < _progressionManager.Items.Count; i++ )
            {
                message += "0";
            }
            PlayerPrefs.SetString( "Items", message );
        }
        
        // TODO : World managment
        public void ResetSave()
        {
            InitializeItemList();
            InitializeSkillList();
            InitializeWeaponList();

            UnlockWeapon( 0 );
        }

        public void UnlockSkill ( int index)
        {
            Char[] skillFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();
            skillFlags [index] = '1';
            PlayerPrefs.SetString( "Weapons", skillFlags.ToString() );
        }
        public void UnlockWeapon ( int index )
        {
            Char[] WeaponFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();
            WeaponFlags [index] = '1';
            PlayerPrefs.SetString( "Weapons", WeaponFlags.ToString() );
        }
        public void UnlockItem ( int index )
        {
            Char[] itemFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();
            itemFlags [index] = '1';
            PlayerPrefs.SetString( "Weapons", itemFlags.ToString() );
        }

        public List<Skill> LoadAllUnlockSkills()
        {
            List<Skill> unlockSkills = new List<Skill>();
            Char[] skillsFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();

            for(int i=0; i< _progressionManager.Skills.Count; i++)
            {
                if ( skillsFlags [i] == '1' )
                    unlockSkills.Add( _progressionManager.Skills [i] );
            }

            return unlockSkills;
        }
        public List<Weapon> LoadAllUnlockWeapons ()
        {
            List<Weapon> unlockWeapons = new List<Weapon>();
            Char[] skillsWeapons = PlayerPrefs.GetString( "Skills" ).ToCharArray();
                
            Debug.Log( "Progression manager" + _progressionManager.Skills );

            for ( int i = 0 ; i < _progressionManager.Skills.Count; i++ )
            {
                if ( skillsWeapons [i] == '1' )
                    unlockWeapons.Add( _progressionManager.Weapons [i] );
            }

            return unlockWeapons;
        }
        public List<Item> LoadAllUnlockItems ()
        {
            List<Item> unlockItems = new List<Item>();
            Char[] skillsItems = PlayerPrefs.GetString( "Skills" ).ToCharArray();

            for ( int i=0; i < _progressionManager.Skills.Count; i++ )
            {
                if ( skillsItems [i] == '1' )
                    unlockItems.Add( _progressionManager.Items [i] );
            }

            return unlockItems;
        }
    }
}
