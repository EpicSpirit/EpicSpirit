using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class HealthHUD : MonoBehaviour
    {
        void Start()
        {

        }
        void Update()
        {
            GetComponent<Text>().text = "HP : " + GameObject.FindWithTag( "Player" ).GetComponent<Character>().Health.ToString();
        }
    }
}