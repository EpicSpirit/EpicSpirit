using UnityEngine;
using System.Collections;


namespace EpicSpirit.Game
{
    public class HealthHUD : MonoBehaviour
    {
        void Start()
        {

        }
        void Update()
        {
            GetComponentInChildren<TextMesh>().text = "HP : " + GameObject.FindWithTag( "Player" ).GetComponent<Character>().Health.ToString();
        }
    }
}