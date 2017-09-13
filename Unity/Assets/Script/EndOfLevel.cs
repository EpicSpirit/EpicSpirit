using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene( "overworld" );

        }
    }
}