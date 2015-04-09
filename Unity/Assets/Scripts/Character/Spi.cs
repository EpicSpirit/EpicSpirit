using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Spi : Character
{
    int _compteur_attack;
    float _lastAttack;
    float _comboAttackInterval;
	
	void Start()
	{
		Initialisation();
		
		_life = 20;
		_lookaroundcount = 300;
        _compteur_attack=0;
        _comboAttackInterval = 0.9f;
        _lastAttack = Time.fixedTime;
	}
	
	void Update()
	{
		Gravity();
	}
    {
    
    public override void Attack()
    {
        
        // Gestion du tick
		if(!isAttacking()) {
			
            // Dans tout les cas on regarde dans quel coup on est
            if (_lastAttack + _comboAttackInterval > Time.fixedTime)
            {
                _compteur_attack++;
            }
            else {
                _compteur_attack = 0;
            }
            // MaJ de la date de la dernière attaque
            _lastAttack = Time.fixedTime;


			List<Character> list_cible = GetListofCible(this.gameObject);
			foreach(Character adv in list_cible) {
				adv.takeDamage(_compteur_attack+1);
			}
			
			// Dans tout les cas on met l'anim d'attaque
			AnimationManager("bim_2");

            if(_compteur_attack >= 2) {
                _compteur_attack = 0;
                AnimationManager("bim_2");
            } else {
                AnimationManager("bim");
            }
		}
    }
    

	
	
}

