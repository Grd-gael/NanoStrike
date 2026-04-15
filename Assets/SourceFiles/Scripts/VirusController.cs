using UnityEngine;

public class VirusController : MonoBehaviour
{
    [SerializeField] private float m_vitesse = 2f;
    [SerializeField] private float m_degats = 10f;
    [SerializeField] private float m_degatsInterval = 1f;
    [SerializeField] private string m_tagCiblePrincipale = "poumons";
    [SerializeField] private string m_tagCibleSecondaire = "coeur";

    private Transform m_cible;
    private Transform m_cible2;

    private bool m_accroche = false;
    private float m_degatsTimer;
    private Vector3 m_rotationSpeed;

    private void Start()
    {
        if (!string.IsNullOrWhiteSpace(m_tagCiblePrincipale))
            m_cible = GameObject.FindWithTag(m_tagCiblePrincipale)?.transform;

        if (!string.IsNullOrWhiteSpace(m_tagCibleSecondaire))
            m_cible2 = GameObject.FindWithTag(m_tagCibleSecondaire)?.transform;

        if (m_cible == null)
        {
            Debug.LogError($"VirusController: cible principale introuvable avec le tag '{m_tagCiblePrincipale}'.", this);
            enabled = false;
            return;
        }

        m_rotationSpeed = new Vector3(
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f)
        );

    }


    private void Update()
    {
        if (m_cible == null)
            return;

        if (!m_accroche)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                m_cible.position,
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
        if (collision.gameObject.CompareTag("poumons") || collision.gameObject.CompareTag("coeur") || collision.gameObject.CompareTag("estomac"))
        {
            m_accroche = true;
            transform.SetParent(collision.transform);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

}