using UnityEngine;

public class SoundOn : MonoBehaviour
{
    public AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audio.mute)
            {
                audio.mute = false;
            }
            audio.Play();
        }
    }
}
