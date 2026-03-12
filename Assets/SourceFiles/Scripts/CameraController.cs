using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;

    [SerializeField] private float m_minZ = -20f;
    [SerializeField] private float m_maxZ = 20f;

    [SerializeField] private float m_minY = -10f;
    [SerializeField] private float m_maxY = 10f;
    private float m_fixedX;

    // Get the initial X position of the camera
    void Start()
    {
        m_fixedX = transform.position.x;
    }

    // Update the camera position based on player input
    void Update()
    {
        float moveZ = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(0, moveY, -moveZ);

        // Limits
        newPosition.x = m_fixedX;
        newPosition.y = Mathf.Clamp(newPosition.y, m_minY, m_maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, m_minZ, m_maxZ);

        transform.position = newPosition;
    }
}
