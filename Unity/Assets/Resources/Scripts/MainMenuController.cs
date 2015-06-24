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
            Classique2,
            Lightning2,
            End
        }

        StateOfAnim _soa;
        public List<GameObject> _backgroundImages;
        public GameObject epc;
        public GameObject sprit;
        public GameObject sword;
        int counter;


        void Start ()
        {
            counter = 0;
            _soa = StateOfAnim.Classique;
            epc.SetActive( false );
            sprit.SetActive( false );
            sword.SetActive( false );
            _backgroundImages [0].SetActive( false );
            _backgroundImages [1].SetActive( false );

            if ( _backgroundImages.Count != 2 ) Debug.Log( "Nombre d'images de background étrange. 3 images attendues" );
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
                    _backgroundImages [0].SetActive( true );
                    timeNextInvoke = 0.2f;
                    _soa = StateOfAnim.Lightning1;
                    break;

                case StateOfAnim.Lightning1 :
                    _backgroundImages [0].SetActive( false );
                    timeNextInvoke = 0.2f;
                    _soa = StateOfAnim.Classique2;
                    epc.SetActive( true );
                    break;
                case StateOfAnim.Classique2 :
                    epc.SetActive( false );
                    timeNextInvoke = 0.2f;
                    _backgroundImages [1].SetActive( true );
                    _soa = StateOfAnim.Lightning2;
                    break;
                case StateOfAnim.Lightning2 :
                    counter++;
                    epc.SetActive( true );
                    sprit.SetActive( true );
                    _backgroundImages [1].SetActive( false );
                    timeNextInvoke = 0f;
                    _soa = StateOfAnim.End;
                    Invoke( "Lauch", 1f );
                    break;
                case StateOfAnim.End :
                    // Nothing
                    break;
                default :
                    Debug.Log("Erreur enum");
                    break;
            }
            if(timeNextInvoke != 0f)
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
        float swordPositionY;
        float _acceleration;
        public void Lauch()
        {
            _acceleration = 1f;
            swordPositionY=sword.transform.position.y;
            sword.SetActive( true );
            sword.transform.position = new Vector3( sword.transform.position.x, sword.transform.position.y + 470, sword.transform.position.z );
            AnimSword();
            //Invoke( "AnimSword", 1f );
        }

        private void AnimSword ()
        {
            //sword.transform.Translate( -Vector2.up * 10 );
            _acceleration += 0.8f;
            sword.transform.position = new Vector3( sword.transform.position.x, sword.transform.position.y-(6*_acceleration), sword.transform.position.z );
            if ( sword.transform.position.y >= swordPositionY + 30 )
                Invoke( "AnimSword", 0.01f );
            else
                sword.transform.position = new Vector3(sword.transform.position.x,swordPositionY,sword.transform.position.z);
        }
    }
}