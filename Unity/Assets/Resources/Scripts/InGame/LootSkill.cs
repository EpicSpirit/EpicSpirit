using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class LootSkill : Loot
    {
        public Skill Skill;
        SaveManager saveManager;

        public void Start()
        {
            saveManager = GameObject.Find( "SaveManager" ).GetComponent<SaveManager>();
            if ( saveManager.IsSkillUnlock( Skill ) ) Destroy( this );
        }

        public override void OnTriggerStay ( Collider collider )
        {
            if ( enableCollect && collider.tag == "Player" )
            {
                saveManager.UnlockSkill( Skill );
                SaveManager.SetIconAttack( SaveManager.IconType.ActualSkill_1, Skill );

                // Solution temporaire :
                var uiAction = GameObject.Find( "Skill_1" ).GetComponent<UIAction>();
                GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>().AddAction( Skill, 1 );
                uiAction.Awake();
                uiAction.Start();

                Destroy( this.gameObject );
            }

        }


    }
}