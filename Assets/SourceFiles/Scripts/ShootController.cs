using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class ShootController : MonoBehaviour
{
    [SerializeField] private GameObject m_projectile;

    [SerializeField] private GameObject m_cameraMouvement;

    [SerializeField] private GameObject m_cameraInitial;
    private InputAction m_shootAction;
    private InputAction m_reloadAction;

    [SerializeField] private float m_cooldown;
    private float m_shootTimer;
    private float m_shootRemaining;
    [SerializeField] private float m_shootNumber;

    [SerializeField] private TMP_Text m_shootRemainingText;
    [SerializeField] private TMP_Text m_reloadInstructionText;

    [SerializeField] public TMP_Text m_scoreText;
    [SerializeField] public string m_score;

    private void Start()
    {

        // Initialize Text
        m_shootRemaining = m_shootNumber;
        m_shootRemainingText.text = m_shootRemaining.ToString();
        m_reloadInstructionText.gameObject.SetActive(false);
        // m_score = "000000";
        // m_scoreText.text = m_score.ToString();

        // Initialize Input Actions
        m_shootAction = InputSystem.actions.FindAction("Shoot");
        m_reloadAction = InputSystem.actions.FindAction("Reload");
    }

    private void Update()
    {
        m_shootTimer -= Time.deltaTime;

        if (m_shootAction.triggered)
        {
            if (m_shootTimer <= 0)
            {
                if (m_shootRemaining <= 0)
                {
                    return;
                }
                m_shootTimer = m_cooldown;
                m_shootRemaining--;
                m_shootRemainingText.text = m_shootRemaining.ToString();
                if (m_shootRemaining <= 0)
                {
                    m_reloadInstructionText.gameObject.SetActive(true);
                }
                Shoot();
            }
        }
        if (m_reloadAction.triggered)
        {
            m_shootRemaining = m_shootNumber;
            m_shootRemainingText.text = m_shootRemaining.ToString();
            m_reloadInstructionText.gameObject.SetActive(false);
        }
    }

    private void Shoot()
    {
        if (m_projectile != null)
        {
            Vector3 position = m_cameraInitial.transform.position + m_cameraMouvement.transform.position + m_cameraInitial.transform.forward * 1.5f;
            GameObject projectileInstance = Instantiate(m_projectile, position, m_cameraInitial.transform.rotation);
        }
    }



}