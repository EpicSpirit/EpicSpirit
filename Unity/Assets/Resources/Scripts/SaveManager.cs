using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class SaveManager : MonoBehaviour
    {
        ProgressionManager _progressionManager;
        public void Start () 
        {
            _progressionManager = this.gameObject.AddComponent<ProgressionManager>();
        }
        public static void SavSpi ( Spi spi )
        {
            PlayerPrefs.SetInt( "Spi_Health", spi.Health );
        }

        public static void GetSavSpi ( Spi spi )
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

        public static List<Action> LoadAllSkills () 
        {

            return new List<Action>();
        }

        public static void InitializeSkillList () 
        {
            PlayerPrefs.SetString("Skill", "1111111111");
        
        }

        public static void SaveNewSkill () 
        {
            //PlayerPrefs.Set("key", "value");
        }



    }
}
