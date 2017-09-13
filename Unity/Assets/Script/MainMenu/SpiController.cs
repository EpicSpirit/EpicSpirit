using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class SpiController : MonoBehaviour
    {
        public void Fall( Vector3 direction )
        {
            this.GetComponent<Character>().Move( direction );
        }
    }
}
