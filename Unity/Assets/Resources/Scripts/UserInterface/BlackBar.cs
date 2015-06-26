using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
	public class BlackBar : MonoBehaviour 
	{
		Text _subtitle;
		Image _image;
		void Awake()
		{
			_image = gameObject.AddComponent<Image>();
			_image.color = Color.black;

			//TODO Opti
			/*GameObject subtitle = gameObject.GetComponentInChildren<GameObject>();
			_subtitle = subtitle.AddComponent<Text>();
			_subtitle.fontSize = 40;
			_subtitle.fontStyle = FontStyle.Italic;
			_subtitle.font = (Font)Resources.Load("UI/BLKCHCRY");
			_subtitle.color = Color.white;
			*/
		}

	}
}