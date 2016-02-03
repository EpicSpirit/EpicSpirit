using UnityEngine;
using System.Collections;

public class CinematicSpawnPoint : MonoBehaviour 
{

    public GameObject Prefab;
    internal GameObject _instance;

    public GameObject Instance
    {
        get { return _instance; }
    }

    public GameObject Spawn()
    {
        return _instance = GameObject.Instantiate( Prefab, this.transform.position, this.transform.rotation ) as GameObject;
    }
        
}
