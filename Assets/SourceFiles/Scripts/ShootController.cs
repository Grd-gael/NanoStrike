using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using OscJack;


public class ShootController : MonoBehaviour
{
    public static ShootController Instance;
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

    // Sound

    public AudioClip m_shootSound;
    private AudioSource m_audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_shootRemaining = m_shootNumber;
        m_shootRemainingText.text = m_shootRemaining.ToString();
        m_reloadInstructionText.gameObject.SetActive(false);

        m_shootAction = InputSystem.actions.FindAction("Shoot");
        m_reloadAction = InputSystem.actions.FindAction("Reload");

        OscManager.Instance.AddCallback("/shoot", OnOscShoot);
        OscManager.Instance.AddCallback("/reload", OnOscReload);

        // Sound
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        OscManager.Instance.RemoveCallback("/shoot", OnOscShoot);
        OscManager.Instance.RemoveCallback("/reload", OnOscReload);
    }

    private void Update()
    {
        m_shootTimer -= Time.deltaTime;

        if (m_shootAction.triggered)
            TryShoot();

        if (m_reloadAction.triggered)
            Reload();
    }

    private void OnOscShoot(string address, OscDataHandle data)
    {
        TryShoot();
    }

    private void OnOscReload(string address, OscDataHandle data)
    {
        Reload();
    }

    // Try to shoot if we can, otherwise do nothing
    private void TryShoot()
    {
        if (m_shootTimer > 0) return;
        if (m_shootRemaining <= 0) return;

        m_shootTimer = m_cooldown;
        m_shootRemaining--;
        m_shootRemainingText.text = m_shootRemaining.ToString();

        if (m_shootRemaining <= 0)
            m_reloadInstructionText.gameObject.SetActive(true);

        Shoot();
    }


    // Reload the shoot count and update the UI 
    public void Reload()
    {
        m_shootRemaining = m_shootNumber;
        m_shootRemainingText.text = m_shootRemaining.ToString();
        m_reloadInstructionText.gameObject.SetActive(false);
    }

    // Instantiate a projectile in front of the camera and play the shoot sound
    public void Shoot()
    {

        if (m_audioSource != null && m_shootSound != null)
        {
            m_audioSource.PlayOneShot(m_shootSound);
        }

        if (m_projectile != null)
        {
            Vector3 position = m_cameraInitial.transform.position
                 + m_cameraInitial.transform.forward * 1.5f;
            Instantiate(m_projectile, position, m_cameraInitial.transform.rotation);
        }


    }
}