using UnityEngine;
using System.Collections;
namespace EpicSpirit.Game
{
    public class Test : MonoBehaviour
    {
        void Start ()
        {
            var t = GameObject.Find( "Weapon" ).GetComponent<UIAction>();
            var t2 = GameObject.Find( "Canvas" ).GetComponent<UIManager>();

            t2.DisableAction( t );
            t2.EnableAction( t );
        }

        void Update ()
        {

        }
    }
}