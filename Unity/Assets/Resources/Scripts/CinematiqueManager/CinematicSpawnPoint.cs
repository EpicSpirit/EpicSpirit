using UnityEngine;
using System.Collections;

public class CinematicSpawnPoint : MonoBehaviour 
{

    public GameObject Prefab;

    public void Spawn()
    {
        GameObject.Instantiate( Prefab, this.transform.position, this.transform.rotation );
    }
        
}
