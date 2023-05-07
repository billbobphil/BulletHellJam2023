using UnityEngine;

namespace Utilities
{
    public class MusicChanger : MonoBehaviour
    {
        [SerializeField] private AudioSource sourceToChangeTo;
        private MusicPlayer _musicPlayer;
        
        private void Start()
        {
            _musicPlayer = GameObject.FindWithTag("Music").GetComponent<MusicPlayer>();
            // _musicPlayer.
        }
    }
}
