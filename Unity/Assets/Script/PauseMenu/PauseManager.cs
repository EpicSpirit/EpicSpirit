using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
	public class PauseManager : MonoBehaviour 
	{

		internal static void BlockEveryCharacter(bool value)
		{
			// Desactive tout les ennemis
			AIController[] allAIController = FindObjectsOfType<AIController>();
			foreach ( AIController aiController in allAIController )
			{
				aiController.enabled = !value;
				//aiController.GetComponent<Character>().AnimationManager( "idle" );
			}
			// Desactive toute l'UI
			List<GameObject> uis = new List<GameObject>(GameObject.FindGameObjectsWithTag( "UI" ));
			foreach (GameObject ui in uis) 
			{
				MonoBehaviour[] allBehaviour = ui.GetComponentsInChildren<MonoBehaviour> ();
				foreach (var behaviour in allBehaviour) 
				{
					if (behaviour != null) 
					{
						behaviour.enabled = !value;
					}
				}
			}
			// Desactive la gestion des touches
			GameObject.Find( "Controller" ).GetComponent<PlayerController>().enabled = !value;
			
			// Met à Spi l'Idle
			//GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>().AnimationManager( "idle" );
		}
	}
}
