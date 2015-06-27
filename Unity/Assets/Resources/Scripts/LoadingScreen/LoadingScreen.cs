using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour 
{
    Image _loadingImage;
    Text _loadingText;

    public Sprite _loadingSprite;
    public string _loadingString;

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
    static bool init=false;
	void Awake () 
    {
        
        init = true;
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
        _loadingImage.color = new Color( 0, 0, 0, 0 );

        // Text properties
        _loadingText.fontStyle = FontStyle.Italic;
        _loadingText.font = ( Font ) Resources.Load( "UI/BLKCHCRY" );
        _loadingText.color = new Color( 1, 1, 1, 0 );
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

        DontDestroyOnLoad( this.gameObject );
        hide();
        
        
	}
    void Update()
    {
        if ( Application.isLoadingLevel )
        {
            show();
        }
        /*   
        else
        {
            hide();
        }
         * */
            

    }
    public void show()
    {
        var pas = 0.05f;
        _loadingImage.enabled = true;
        _loadingText.enabled = true;
        
        if(_loadingImage.color.a != 1)
        {
            var c = _loadingImage.color;
            
            if ( c.a + pas > 1 ) _loadingImage.color = new Color( c.r, c.g, c.b, 1 );
            else _loadingImage.color = new Color( c.r, c.g, c.b, c.a + pas );

            c = _loadingText.color;
            if ( c.a + pas > 1 ) _loadingText.color = new Color( c.r, c.g, c.b, 1 );
            else _loadingText.color = new Color( c.r, c.g, c.b, c.a + pas );
            
        }
        if(_loadingImage.color.a == 1)
        {
            hide();
        }
        else
        { 
            Invoke("show",0.1f);
        }

    }

    public void hide()
    {
        var pas = 0.05f;

        _loadingImage.enabled = false;
        _loadingText.enabled = false;

        if ( _loadingImage.color.a != 0 )
        {
            var c = _loadingImage.color;
            if ( c.a + pas < 0 ) _loadingImage.color = new Color( c.r, c.g, c.b, 0 );
            else _loadingImage.color = new Color( c.r, c.g, c.b, c.a - pas );

            c = _loadingText.color;
            if ( c.a + pas < 0 ) _loadingText.color = new Color( c.r, c.g, c.b, 0 );
            else _loadingText.color = new Color( c.r, c.g, c.b, c.a - pas );

        }
        
    }
}
