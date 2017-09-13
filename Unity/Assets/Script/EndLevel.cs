using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class EndLevel : MonoBehaviour
    {
        public void Next()
        {
            LevelManager.SetParameter("ended",true);
            LevelManager.LoadLevel("overworld");
        }
    }
}
