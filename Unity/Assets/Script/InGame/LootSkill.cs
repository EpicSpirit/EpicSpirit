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
                SaveManager.SetIconAttack( SaveManager.IconType.ActualSkill_3, Skill );

                // Solution temporaire :
                GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>().AddAction( Skill, 4 );
                GameObject.Find("Actions").GetComponent<UIManager>().EnableAction(4);

                Destroy( this.gameObject );
            }

        }


    }
}
