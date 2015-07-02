using UnityEngine;
using System.Collections;

public class CinematicSpawnPoint : MonoBehaviour 
{

    public GameObject Prefab;

    public GameObject Spawn()
    {
        return GameObject.Instantiate( Prefab, this.transform.position, this.transform.rotation ) as GameObject;
    }
        
}
