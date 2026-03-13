using UnityEngine;
using OscJack;

public class OscManager : MonoBehaviour
{
    public static OscManager Instance;

    [SerializeField] private int m_port = 9000;

    private OscServer m_oscServer;

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
        m_oscServer?.Dispose();
    }

    public void AddCallback(string address, OscMessageDispatcher.MessageCallback callback)
    {
        m_oscServer.MessageDispatcher.AddCallback(address, callback);
    }

    public void RemoveCallback(string address, OscMessageDispatcher.MessageCallback callback)
    {
        m_oscServer.MessageDispatcher.RemoveCallback(address, callback);
    }
}