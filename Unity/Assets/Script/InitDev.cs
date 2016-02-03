using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class InitDev : MonoBehaviour
    {

        void Awake()
        {
            Debug.Log("InitDev exécuté");

            GameObject.Find("SaveManager").GetComponent<SaveManager>().ResetSave();

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}