using UnityEngine;
using OscJack;

public class OscManager : MonoBehaviour
{
    public static OscManager Instance;

    [SerializeField] private int m_port = 9000;

    private OscServer m_oscServer;
    private bool m_isShuttingDown;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        m_oscServer = new OscServer(m_port);
    }

    private void OnDestroy()
    {
        m_isShuttingDown = true;
        m_oscServer?.Dispose();

        if (Instance == this)
            Instance = null;
    }

    public void AddCallback(string address, OscMessageDispatcher.MessageCallback callback)
    {
        if (m_isShuttingDown || m_oscServer == null)
            return;

        m_oscServer.MessageDispatcher?.AddCallback(address, callback);
    }

    public void RemoveCallback(string address, OscMessageDispatcher.MessageCallback callback)
    {
        if (m_isShuttingDown || m_oscServer == null)
            return;

        m_oscServer.MessageDispatcher?.RemoveCallback(address, callback);
    }
}