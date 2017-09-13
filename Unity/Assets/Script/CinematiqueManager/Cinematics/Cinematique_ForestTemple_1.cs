using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
            BlackBars.TopSubtitleText = "Congratulation Spi";
            BlackBars.BottomSubtitleText = "Take this power";
        }

        public void Etape2 ()
        {
            BlackBars.TopSubtitleText = "With great power";
            BlackBars.BottomSubtitleText = "comes great responsibility...";
        }

        public void Etape3()
        {
            _spi.AnimationManager( "HealthPotion" );
            _particuleSystem.Play();

        }
        public void Etape4()
        {
            // On débloque le skill fireball, et on le place en skill_1. Fin du niveau
            var frozenPick = GameObject.Find( "ProgressionManager" ).GetComponent<FrozenPick>();
            GameObject.Find( "SaveManager" ).GetComponent<SaveManager>().UnlockSkill( frozenPick );

            SaveManager.SetIconAttack( SaveManager.IconType.ActualSkill_1, frozenPick );

            SceneManager.LoadScene( "end_level" );
        }


    }
}