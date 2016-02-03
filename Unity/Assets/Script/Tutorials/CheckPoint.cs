using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	public class CheckPoint : MonoBehaviour 
	{
		public virtual void OnTriggerEnter(Collider collider)
		{
			if (collider.tag == "Player") 
			{
				GameObject.Destroy(gameObject);
			}
		}

	}
}