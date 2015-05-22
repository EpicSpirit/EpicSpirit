﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace EpicSpirit.Game
{
    public class SaveManager : MonoBehaviour
    {
        ProgressionManager _progressionManager;

        public void Start () 
        {
            Debug.Log( "SaveManager START" );

            _progressionManager = this.gameObject.AddComponent<ProgressionManager>();
            _progressionManager.CustomStart();

            ResetSave();    // TMP

            Debug.Log( "Start : progressionManager : "+_progressionManager );
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
            for ( int i = 0; i < _progressionManager.Items.Count; i++ )
            {
                message += "0";
            }
            PlayerPrefs.SetString( "Items", message );
        }
        
        // TODO : World managment
        public void ResetSave()
        {
            Debug.Log("ResetSave");
            InitializeItemList();
            InitializeSkillList();
            InitializeWeaponList();

            UnlockWeapon( 0 );
        }

        public void UnlockSkill ( int index)
        {
            Char[] skillFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();
            skillFlags [index] = '1';

            StringBuilder stringbuilder = new StringBuilder();
            foreach ( Char mychar in skillFlags )
            {
                stringbuilder.Append( mychar );
            }

            PlayerPrefs.SetString( "Skills", stringbuilder.ToString() );
        }
        public void UnlockWeapon ( int index )
        {
            Char[] WeaponFlags = PlayerPrefs.GetString( "Weapons" ).ToCharArray();
            WeaponFlags [index] = '1';

            StringBuilder stringbuilder = new StringBuilder();
            foreach(Char mychar in WeaponFlags)
            {
                stringbuilder.Append( mychar );   
            }

            PlayerPrefs.SetString( "Weapons", stringbuilder.ToString() );
            
        }
        public void UnlockItem ( int index )
        {
            Char[] itemFlags = PlayerPrefs.GetString( "Items" ).ToCharArray();
            itemFlags [index] = '1';

            StringBuilder stringbuilder = new StringBuilder();
            foreach ( Char mychar in itemFlags )
            {
                stringbuilder.Append( mychar );
            }

            PlayerPrefs.SetString( "Items", stringbuilder.ToString() );
        }

        public List<Skill> LoadAllUnlockSkills()
        {
            List<Skill> unlockSkills = new List<Skill>();
            Char[] skillsFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();


            for(int i=0; i < _progressionManager.Skills.Count; i++)
            {
                if ( i < skillsFlags.Length && skillsFlags [i] == '1' )
                    unlockSkills.Add( _progressionManager.Skills [i] );
            }

            return unlockSkills;
        }
        public List<Weapon> LoadAllUnlockWeapons ()
        {
            List<Weapon> unlockWeapons = new List<Weapon>();
            
            var a = PlayerPrefs.GetString("Weapons", "error");
            Debug.Log (a);
            Char[] weaponsFlags = PlayerPrefs.GetString( "Weapons" ).ToCharArray();

            for ( int i = 0 ; i < _progressionManager.Weapons.Count; i++ )
            {
                if ( i < weaponsFlags.Length && weaponsFlags [i] == '1' )
                {
                    unlockWeapons.Add( _progressionManager.Weapons [i] );
                    Debug.Log(":D");
                }
            }

            return unlockWeapons;
        }
        public List<Item> LoadAllUnlockItems ()
        {
            List<Item> unlockItems = new List<Item>();
            Char[] itemsFlags = PlayerPrefs.GetString( "Items" ).ToCharArray();

            for ( int i=0; i < _progressionManager.Items.Count; i++ )
            {
                if ( i < itemsFlags.Length && itemsFlags [i] == '1' )
                    unlockItems.Add( _progressionManager.Items [i] );
            }

            return unlockItems;
        }
    }
}
