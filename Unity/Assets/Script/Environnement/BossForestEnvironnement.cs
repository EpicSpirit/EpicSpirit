using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	public class BossForestEnvironnement : Environnement
	{
		void Start ()
		{
			SetEnvironnement(SoundManager.Sound.Music_BossForest);
		}
	}
}
