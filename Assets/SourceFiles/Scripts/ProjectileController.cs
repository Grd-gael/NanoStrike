using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float m_vitesse = 20f;
    [SerializeField] private float m_dureeDeVie = 5f;
    [SerializeField] private float m_rotationSpeedX = 0;
    [SerializeField] private float m_rotationSpeedY = 0;
    [SerializeField] private float m_rotationSpeedZ = 0;

    // [SerializeField] private ShootController m_shootController;

    private Vector3 m_rotationSpeed;

    private void Start()
    {
        m_rotationSpeed = new Vector3(m_rotationSpeedX, m_rotationSpeedY, m_rotationSpeedZ);
        Destroy(gameObject, m_dureeDeVie);
    }

    private void Update()
    {
        transform.position += transform.forward * m_vitesse * Time.deltaTime;
        // transform.Rotate(m_rotationSpeed * Time.deltaTime, Space.Self);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("virus"))
        {
            Destroy(collision.gameObject);
            // m_shootController.m_score += 100;
            // m_shootController.m_scoreText.text = m_shootController.m_score.ToString();
        }
    }
}