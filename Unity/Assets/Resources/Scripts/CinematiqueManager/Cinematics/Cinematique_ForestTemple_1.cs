using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Cinematique_ForestTemple_1 : Cinematic
    {
        Spi _spi;
        ParticleSystem _particuleSystem;

        public override void Awake()
        {
            base.Awake();
            _spi = _player.GetComponent<Spi>();
            _particuleSystem = Instantiate<GameObject>( Resources.Load<GameObject>( "Prefab/NewSkillParticule" ) ).GetComponent<ParticleSystem>(); ;
            _particuleSystem.transform.parent = _spi.transform;
            _particuleSystem.transform.position = _spi.transform.position;
        }

        public override void LaunchCinematic ()
        {
            var cameraPoint = GetComponentInChildren<Cinematic_CameraPoint>();
            _cameraController.CameraSpeed = MoveCamera.SLOW;

            _cameraController.CameraRotation = this.transform.rotation;

            cameraPoint.Prepare( _cameraController, 0f );
            BlockEveryCharacter( true );

            Invoke( "Etape1", 0f );
            Invoke( "Etape2", 4f );
            Invoke( "Etape3", 7f );
            Invoke( "Etape4", 8f );

        }
        public void Etape1()
        {
            BlackBars.TopSubtitleText = "Bravo Spi, tu as réussis";
            BlackBars.BottomSubtitleText = "Prend ce pouvoir pour réussir ta quête";
        }

        public void Etape2 ()
        {
            BlackBars.TopSubtitleText = "Fais en bon usage ...";
            BlackBars.BottomSubtitleText = "";
        }

        public void Etape3()
        {
            _spi.AnimationManager( "HealthPotion" );
            _particuleSystem.Play();

        }
        public void Etape4()
        {
            var fireball = GameObject.Find( "ProgressionManager" ).GetComponent<FireBall>();
            GameObject.Find( "SaveManager" ).GetComponent<SaveManager>().UnlockSkill( fireball );

            SaveManager.SetIconAttack( SaveManager.IconType.ActualSkill_1, fireball );

            Application.LoadLevel( "end_level" );
        }


    }
}