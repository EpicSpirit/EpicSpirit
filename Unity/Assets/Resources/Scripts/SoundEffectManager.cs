using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour {

    public static void PlaySound(string s) 
    {
        AudioClip audio = Resources.Load<AudioClip>("Audio/SoundEffect/"+s);
        //audio.Play();
    }

}
