using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class TempleSwitchPoleSpawnEnemies : TempleSwitchPole
    {
        [SerializeField]
        List<CinematicSpawnPoint> _spawnPoints;

        public List<CinematicSpawnPoint> SpawnPoints
        {
            get { return _spawnPoints; }
            set { _spawnPoints = value; }
        }

        public void Awake()
        {
            if ( SpawnPoints.Count == 0 )
            {
                foreach ( CinematicSpawnPoint spawnPoint in GetComponentsInChildren<CinematicSpawnPoint>() )
                {
                    Debug.Log( "ajout" );
                    SpawnPoints.Add( spawnPoint );
                }
            }
        }

        internal override void takeDamage( int force )
        {
         
            foreach ( CinematicSpawnPoint spawnPoint in SpawnPoints )
            {
                spawnPoint.Spawn();
                Debug.Log( "pese" );
            }

            base.takeDamage( force );
        }
    }
}