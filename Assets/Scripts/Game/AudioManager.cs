using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioResource[] m_Sounds;

    public void PlaySound(int sound)
    {
        gameObject.GetComponent<AudioSource>().resource = m_Sounds[sound];
        gameObject.GetComponent<AudioSource>()?.Play();
    }
}
