using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour
{
    Image _loadingImage;
    Text _loadingText;

    public Sprite _loadingSprite;
    public string _loadingString = "Loading...";

    /*
    /// <summary>
    /// Sprite from the loading GUI Image
    /// </summary>
    public Sprite LoadingImage
    {
        get { return _loadingImage.sprite; }
        set
        {
            _loadingImage.sprite = value;
            if ( value == new Sprite() )
            {
                _loadingImage.color = Color.black;
            }
        }
    }
    /// <summary>
    /// Text from the GUI Text
    /// </summary>
    public string LoadingText
    {
        get { return _loadingText.text; }
        set { _loadingText.text = value; }
    }
     * */

    void Awake ()
    {
        // Load image and text into children
        _loadingImage = transform.FindChild( "Background" ).gameObject.AddComponent<Image>();
        _loadingText = transform.FindChild( "Text" ).gameObject.AddComponent<Text>();

        // Load an image if specified. If not, use black background
        if ( _loadingSprite != null )
        {
            _loadingImage.sprite = _loadingSprite;
        }
        else
        {
            _loadingImage.color = Color.black;
        }

        // Text properties
        _loadingText.fontStyle = FontStyle.Italic;
        _loadingText.font = ( Font ) Resources.Load( "UI/BLKCHCRY" );
        _loadingText.color = Color.white;
        _loadingText.alignment = TextAnchor.MiddleCenter;
        _loadingText.resizeTextForBestFit = true;
        _loadingText.resizeTextMaxSize = 120;
        _loadingText.resizeTextMinSize = 10;

        // Load a text if specified. If not, use text "Loading..."
        if ( _loadingString != "" )
        {
            _loadingText.text = _loadingString;
        }
        else
        {
            _loadingText.text = "Loading...";
        }

        DontDestroyOnLoad( this );
        hide();
    }
    void Update ()
    {
        if ( Application.isLoadingLevel )
        {
            show();
            Debug.Log( "true" );
        }

        else
        {
            Debug.Log( "false" );
            hide();
        }


    }
    public void show ()
    {
        _loadingImage.enabled = true;
        _loadingText.enabled = true;
    }

    public void hide ()
    {
        _loadingImage.enabled = false;
        _loadingText.enabled = false;
    }
}