using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class t : MonoBehaviour
    {
        void Awake()
        {
            Invoke( "End", 5f );
        }

        void End()
        {
            Application.LoadLevel( "overworld" );

        }
    }
}