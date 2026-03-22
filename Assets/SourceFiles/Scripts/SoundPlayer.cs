using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static void PlaySound(AudioClip m_audio, Vector3 m_position)
    {
        GameObject m_soundObj = new GameObject("DestroySound");
        m_soundObj.transform.position = m_position;
        AudioSource m_source = m_soundObj.AddComponent<AudioSource>();
        m_source.clip = m_audio;
        m_source.spatialBlend = 0f;
        m_source.volume = 1f;
        m_source.Play();
        Destroy(m_soundObj, m_audio.length);
    }
}