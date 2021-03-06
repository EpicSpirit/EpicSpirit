﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class HealthAmount : MonoBehaviour
    {
		Image _hp;
        Character _character;

        byte _count;
        void Awake()
        {
            _hp = gameObject.AddComponent<Image>();
            _hp.type = Image.Type.Filled;
            _hp.sprite = Resources.Load<Sprite>("UI/Images/health_top");
            _hp.color = Color.white;

            _hp.fillMethod = Image.FillMethod.Horizontal;
            _hp.fillOrigin = 0;
            _hp.fillAmount = 1;
            _hp.preserveAspect = true;

            _character = GameObject.FindWithTag( "Player" ).GetComponent<Character>();
            _count = 0;
        }

        void Update()
        {
			_count++;
			if (_count >= 20) 
			{
                UpdateHealthBar();
				_count = 0;
			}
        }
        private void UpdateHealthBar()
        {
            _hp.fillAmount = ( 0.9f * (float)_character.CurrentHealth / (float)_character.MaxHealth ) + 0.1f;
            if ( _hp.fillAmount >= 0.5 )
            {
                _hp.color = Color.green;
            }
            else if ( _hp.fillAmount >= 0.2 )
            {
                _hp.color = Color.yellow;
            }
            else
            {
                _hp.color = Color.red;
            }
        }
    }
}