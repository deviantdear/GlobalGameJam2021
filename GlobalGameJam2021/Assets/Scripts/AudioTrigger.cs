using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public GameObject Player;
    public AudioClip regionSong;
    [SerializeField]
    private float distance;
    public bool isMetal;
    public int inRoom = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPosition();

        distance = Vector3.Distance(transform.position, Player.transform.position);
    }
    IEnumerator CheckPosition()
    {
        if (inRoom < 1)
        {

            if (Vector3.Distance(transform.position, Player.transform.position) < 1)
            {
                AudioManager.Instance.PlayMusicWithFade(regionSong, 1);
                inRoom++;
                isMetal = true;
            }
        }

        yield return new WaitForSeconds(.1f);
    }

}
