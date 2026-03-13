using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float m_vitesse = 20f;
    [SerializeField] private float m_dureeDeVie = 5f;

    private Vector3 m_rotationSpeed;
    private Vector3 m_direction;

    private void Start()
    {
        m_rotationSpeed = new Vector3(
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f)
        );
        m_direction = transform.forward;
        Destroy(gameObject, m_dureeDeVie);
    }

    private void Update()
    {
        transform.Rotate(m_rotationSpeed * Time.deltaTime, Space.Self);
        transform.position += m_direction * m_vitesse * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("virus"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.AjouterScore(100);
        }
        Destroy(gameObject);
    }
}