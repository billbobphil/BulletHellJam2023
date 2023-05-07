using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class MusicChanger : MonoBehaviour
    {
        [SerializeField] private AudioClip newSong;

        private void Start()
        {
            GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("Music");
            
            foreach(GameObject musicPlayer in musicPlayers)
            {
                musicPlayer.GetComponent<AudioSource>().Stop();
                musicPlayer.GetComponent<AudioSource>().clip = newSong;
                musicPlayer.GetComponent<AudioSource>().Play();
            }
        }
    }
}