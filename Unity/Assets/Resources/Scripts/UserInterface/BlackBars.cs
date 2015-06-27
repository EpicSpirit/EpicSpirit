using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
	public class BlackBars : MonoBehaviour 
	{
		Text _topSubtitle;
        Text _bottomSubtitle;

		Image _topBlackBar;
        Image _bottomBlackBar;


        public string TopSubtitleText
        {
            get { return _topSubtitle.text; }
            set { _topSubtitle.text = value; }
        }
        public string BottomSubtitleText
        {
            get { return _bottomSubtitle.text; }
            set { _bottomSubtitle.text = value; }
        }
        public bool EnabledBlackBars
        {
            get { return _topBlackBar.enabled; }
            set 
            {
                _topBlackBar.enabled = value;
                _bottomBlackBar.enabled = value;
            }
        }
        public bool EnabledSubtitles
        {
            get { return _topSubtitle.enabled; }
            set
            {
                _topBlackBar.enabled = value;
                _bottomBlackBar.enabled = value;
                _topSubtitle.enabled = value;
                _bottomSubtitle.enabled = value;
            }
        }
        public bool EnabledSubtitlesAndBlackBars
        {
            set
            {
                _topSubtitle.enabled = value;
                _bottomSubtitle.enabled = value;
            }
        }
		void Awake()
		{
            _topBlackBar = GameObject.Find( "Top Black Bar" ).AddComponent<Image>();
            _bottomBlackBar = GameObject.Find( "Bottom Black Bar" ).AddComponent<Image>();
            _topSubtitle = GameObject.Find( "Top Subtitle" ).AddComponent<Text>();
            _bottomSubtitle = GameObject.Find( "Bottom Subtitle" ).AddComponent<Text>();

            _topBlackBar.color = Color.black;
            _bottomBlackBar.color = Color.black;

			_topSubtitle.fontSize = 40;
			_topSubtitle.fontStyle = FontStyle.Italic;
			_topSubtitle.font = (Font)Resources.Load("UI/BLKCHCRY");
			_topSubtitle.color = Color.white;
            _topSubtitle.alignment = TextAnchor.MiddleCenter;
            _topSubtitle.resizeTextForBestFit = true;
            _topSubtitle.resizeTextMinSize = 20;
            _topSubtitle.resizeTextMaxSize = 40;

            _bottomSubtitle.fontSize = 40;
            _bottomSubtitle.fontStyle = FontStyle.Italic;
            _bottomSubtitle.font = (Font)Resources.Load( "UI/BLKCHCRY" );
            _bottomSubtitle.color = Color.white;
            _bottomSubtitle.alignment = TextAnchor.MiddleCenter;
            _bottomSubtitle.resizeTextForBestFit = true;
            _bottomSubtitle.resizeTextMinSize = 20;
            _bottomSubtitle.resizeTextMaxSize = 40;

            EnabledSubtitlesAndBlackBars = false;
			
		}

	}
}1