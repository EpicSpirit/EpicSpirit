using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public static class SaveManager
    {
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


    }
}
