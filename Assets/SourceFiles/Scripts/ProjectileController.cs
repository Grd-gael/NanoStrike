using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] private float m_vitesse = 20f;
    [SerializeField] private float m_dureeDeVie = 5f;

    private Vector3 m_rotationSpeed;
    private Vector3 m_direction;

    // Sound
    public AudioClip m_destroySound;
    private AudioSource m_audioSource;

    private Vector3 m_soundPosition = new Vector3(-3, 10, 0);


    private void Start()
    {
        m_rotationSpeed = new Vector3(
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f)
        );
        m_direction = transform.forward;
        Destroy(gameObject, m_dureeDeVie);

        // Sound
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(m_rotationSpeed * Time.deltaTime, Space.Self);
        transform.position += m_direction * m_vitesse * Time.deltaTime;
    }

    // Destroy projectile on collision and play sound if it hits a virus
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("virus"))
        {
            if (m_audioSource != null && m_destroySound != null)
            {
                SoundPlayer.PlaySound(m_destroySound, m_soundPosition);
            }
            Destroy(collision.gameObject);
            GameManager.Instance.AjouterScore(100);
        }
        Destroy(gameObject);
    }
}