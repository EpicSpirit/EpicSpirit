using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	public class CheckPoint : MonoBehaviour 
	{
		public void OnTriggerEnter(Collider collider)
		{
			if (collider.tag == "Player") 
			{
				GameObject.Destroy(gameObject);
			}
		}

	}
}