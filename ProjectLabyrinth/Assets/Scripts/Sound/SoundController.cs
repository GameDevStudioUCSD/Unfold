using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
    // Takes an audio source and plays a music track on it. Allows looping
    public static void PlayMusic(AudioSource source, AudioClip soundClip)
    {
        if(soundClip == null )
        {
            Debug.LogError("Passed a null sound music clip into PlayMusic!");
            return;
        }
        source.clip = soundClip;
        source.Play();
    }
    // Takes an audio source and plays a single clip at that source
    public static void PlaySound(AudioSource source, AudioClip soundClip)
    {
        source.PlayOneShot(soundClip, 1f);
    }
    // Takes an audio source and a AudioClip array and plays a random clip 
    // at that source
    public static void PlaySound(AudioSource source, AudioClip[] soundArray)
    {
        if (soundArray.Length > 0)
        {
            float i = UnityEngine.Random.Range(0, soundArray.Length);
            AudioClip soundClip = soundArray[(int)Mathf.Floor(i)];
            source.PlayOneShot(soundClip, 1f);
        }
    }
	
}
