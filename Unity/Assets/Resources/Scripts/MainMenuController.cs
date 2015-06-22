using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class MainMenuController : MonoBehaviour
    {
        enum StateOfAnim
        {
            Classique,
            Lightning1,
            Lightning2,
            End
        }

        StateOfAnim _soa;
        public List<GameObject> _backgroundImages;
        public GameObject title;
        public GameObject sword;
        int counter;


        void Start ()
        {
            counter = 0;
            _soa = StateOfAnim.Classique;
            title.SetActive( false );
            sword.SetActive( false );
            _backgroundImages [0].SetActive( true );
            _backgroundImages [1].SetActive( true );
            _backgroundImages [2].SetActive( true );

            if ( _backgroundImages.Count != 3 ) Debug.Log( "Nombre d'images de background étrange. 3 images attendues" );
            else 
            {
                Invoke( "ChangeIMG", 3f );
            }
        }

        
        void ChangeIMG ()
        {
            float timeNextInvoke=0f;
            switch(_soa)
            {
                case StateOfAnim.Classique :
                    title.SetActive( false );
                    _backgroundImages [0].SetActive( false );
                    timeNextInvoke = 0.2f;
                    _soa = StateOfAnim.Lightning1;
                    break;
                case StateOfAnim.Lightning1 :
                    _backgroundImages [1].SetActive( false );
                    timeNextInvoke = 0.2f;
                    _soa = StateOfAnim.Lightning2;
                    break;
                case StateOfAnim.Lightning2 :
                    counter++;
                    _backgroundImages [0].SetActive( true );
                    _backgroundImages [1].SetActive( true );
                    _backgroundImages [2].SetActive( true );
                    timeNextInvoke = 2f;
                    _soa = StateOfAnim.Classique;
                    title.SetActive( true );
                    if ( counter == 2 )
                    {
                        sword.SetActive( true );
                        AnimSword();
                        _soa = StateOfAnim.End;
                    }
                    break;
                case StateOfAnim.End :
                    // Nothing
                    break;
                default :
                    Debug.Log("Erreur enum");
                    break;
            }
            Invoke( "ChangeIMG", timeNextInvoke );
        }

        public void Continue()
        {
            Application.LoadLevel( "overworld" );
        }
        public void NewGame ()
        {
            GameObject.Find( "SaveManager" ).GetComponent<SaveManager>().ResetSave();
            Application.LoadLevel( "overworld" );
        }
        public void Option ()
        {
            Debug.Log("Pas encore de menu d'option pour le moment.");
        }

        private void AnimSword ()
        {
            Debug.Log("a");
            sword.transform.Translate( -Vector2.up * 50 );
            if(sword.transform.position.y >= title.transform.position.y)
                Invoke( "AnimSword", 0.01f);
        }
    }
}