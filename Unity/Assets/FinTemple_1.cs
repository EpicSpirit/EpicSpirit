using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class FinTemple_1 : MonoBehaviour
    {
        public void OnTriggerEnter ( Collider c )
        {
            var fireball = GameObject.Find( "ProgressionManager" ).GetComponent<FireBall>();
            GameObject.Find( "SaveManager" ).GetComponent<SaveManager>().UnlockSkill(fireball);

            Application.LoadLevel( "end_level" );
        }


    }
}