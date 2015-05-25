using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace EpicSpirit.Game
{
    public class actionicone : MonoBehaviour
    {

        Action _action;

        public Action Action
        {
            get { return _action; }
            set { _action = value; Refresh(); }
        }
		
        void Refresh()
        {
            Debug.Log("Changementimage ");
            this.GetComponent<Button>().image.sprite = _action.GetSprite;
        }

        
        
    }
}