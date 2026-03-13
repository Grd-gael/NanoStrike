using UnityEngine;

public class VirusController : MonoBehaviour
{
    [SerializeField] private float m_vitesse = 2f;
    [SerializeField] private float m_degats = 10f;
    [SerializeField] private float m_degatsInterval = 1f;

    private Transform m_poumon;
    private bool m_accroche = false;
    private float m_degatsTimer;
    private Vector3 m_rotationSpeed;

    private void Start()
    {
        m_poumon = GameObject.FindWithTag("poumons").transform;
        m_rotationSpeed = new Vector3(
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f)
        );
    }

    private void Update()
    {
        if (!m_accroche)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                m_poumon.position,
                m_vitesse * Time.deltaTime
            );
            transform.Rotate(m_rotationSpeed * Time.deltaTime, Space.Self);

        }
        else
        {
            m_degatsTimer -= Time.deltaTime;
            if (m_degatsTimer <= 0)
            {
                GameManager.Instance.SubirDegats(m_degats);
                m_degatsTimer = m_degatsInterval;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("poumons"))
        {
            m_accroche = true;
            transform.SetParent(collision.transform);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}