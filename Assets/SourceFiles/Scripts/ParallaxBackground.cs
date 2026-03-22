using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float m_parallaxStrength = 0.1f;
    [SerializeField] private GameObject m_cam;
    private Vector3 m_lastCamPos;

    void Start()
    {
        m_lastCamPos = m_cam.transform.position;
    }

    void Update()
    {
        Vector3 delta = m_cam.transform.position - m_lastCamPos;
        transform.position += new Vector3(delta.x * m_parallaxStrength, delta.y * m_parallaxStrength, 0);
        m_lastCamPos = m_cam.transform.position;
    }
}