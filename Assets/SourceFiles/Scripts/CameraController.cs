using UnityEngine;
using OscJack;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float m_minZ = -20f;
    [SerializeField] private float m_maxZ = 20f;
    [SerializeField] private float m_minY = -10f;
    [SerializeField] private float m_maxY = 10f;

    private float m_fixedX;
    private float m_oscAxisZ = 0f;
    private float m_oscAxisY = 0f;
    private bool m_callbacksRegistered;


    void Start()
    {
        m_fixedX = transform.position.x;

        if (OscManager.Instance != null)
        {
            OscManager.Instance.AddCallback("/joystick/x", OnJoystickX);
            OscManager.Instance.AddCallback("/joystick/y", OnJoystickY);
            m_callbacksRegistered = true;
        }
    }

    private void OnDestroy()
    {
        if (!m_callbacksRegistered || OscManager.Instance == null)
            return;

        OscManager.Instance.RemoveCallback("/joystick/x", OnJoystickX);
        OscManager.Instance.RemoveCallback("/joystick/y", OnJoystickY);
    }

    private void OnJoystickX(string address, OscDataHandle data)
    {
        m_oscAxisZ = data.GetElementAsFloat(0);
    }

    private void OnJoystickY(string address, OscDataHandle data)
    {
        m_oscAxisY = data.GetElementAsFloat(0);
    }

    void Update()
    {
        float moveZ = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;

        moveZ += m_oscAxisZ * m_speed * Time.deltaTime;
        moveY += m_oscAxisY * m_speed * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(0, moveY, -moveZ);

        newPosition.x = m_fixedX;
        newPosition.y = Mathf.Clamp(newPosition.y, m_minY, m_maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, m_minZ, m_maxZ);

        transform.position = newPosition;
    }
}