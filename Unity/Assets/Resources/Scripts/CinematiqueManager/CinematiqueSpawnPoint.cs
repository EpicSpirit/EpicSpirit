using UnityEngine;
using System.Collections;

public class CinematiqueSpawnPoint : MonoBehaviour {

    public GameObject Prefab;

    public void Spawn()
    {
        GameObject.Instantiate( Prefab, this.transform.position, this.transform.rotation );
    }
        
}
