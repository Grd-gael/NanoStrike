using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Vie")]
    [SerializeField] private float m_vieMax = 100f;
    [SerializeField] private RectTransform m_vieRestante;

    [SerializeField] private GameObject m_gameOverScreen;

    [Header("Score")]
    [SerializeField] private TMP_Text m_scoreText;

    private float m_vie;
    private float m_largeurMax;
    private int m_score = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_vie = m_vieMax;
        m_largeurMax = m_vieRestante.sizeDelta.x;
        MettreAJourScore();
        MettreAJourBarre();
    }

    public void SubirDegats(float degats)
    {
        m_vie -= degats;
        m_vie = Mathf.Max(0, m_vie);
        MettreAJourBarre();

        if (m_vie <= 0)
            GameOver();
    }

    private void MettreAJourBarre()
    {
        float ratio = m_vie / m_vieMax;
        m_vieRestante.sizeDelta = new Vector2(m_largeurMax * ratio, m_vieRestante.sizeDelta.y);
    }

    public void AjouterScore(int points)
    {
        m_score += points;
        MettreAJourScore();
    }

    private void MettreAJourScore()
    {
        m_scoreText.text = m_score.ToString("D6");
    }

    private void GameOver()
    {
        m_gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Rejouer()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}