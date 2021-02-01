using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip regionSong;
    public bool isMetal;
    public bool inRoom = false;

    public void transitionMusic()
    {
        AudioManager.Instance.PlayMusicWithFade(regionSong, 1);
        inRoom = true;

    }
       

}
