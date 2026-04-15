using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Vie")]
    [SerializeField] private float m_vieMax = 100f;
    [SerializeField] private RectTransform[] m_viesRestantes;

    [SerializeField] private GameObject m_gameOverScreen;

    [Header("Score")]
    [SerializeField] private TMP_Text[] m_scoreTexts;

    private float m_vie;
    private float[] m_largeursMax;
    private int m_score = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (m_viesRestantes == null)
            m_viesRestantes = new RectTransform[0];

        if (m_scoreTexts == null)
            m_scoreTexts = new TMP_Text[0];

        m_vie = m_vieMax;
        m_largeursMax = new float[m_viesRestantes.Length];

        for (int i = 0; i < m_viesRestantes.Length; i++)
        {
            if (m_viesRestantes[i] == null)
                continue;

            m_largeursMax[i] = m_viesRestantes[i].sizeDelta.x;
        }

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

        for (int i = 0; i < m_viesRestantes.Length; i++)
        {
            RectTransform barreVie = m_viesRestantes[i];
            if (barreVie == null)
                continue;

            barreVie.sizeDelta = new Vector2(m_largeursMax[i] * ratio, barreVie.sizeDelta.y);
        }
    }

    public void AjouterScore(int points)
    {
        m_score += points;
        MettreAJourScore();
    }

    private void MettreAJourScore()
    {
        string scoreFormate = m_score.ToString("D6");

        for (int i = 0; i < m_scoreTexts.Length; i++)
        {
            TMP_Text scoreText = m_scoreTexts[i];
            if (scoreText == null)
                continue;

            scoreText.text = scoreFormate;
        }
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