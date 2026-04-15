using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [Header("Écran Poumon/Coeur")]
    [SerializeField] private CameraController m_cameraControllerPoumon;
    [SerializeField] private ShootController m_shootControllerPoumon;
    [SerializeField] private CanvasGroup m_overlayInactifPoumon;
    [SerializeField] private AudioListener m_audioListenerPoumon;

    [Header("Écran Estomac")]
    [SerializeField] private CameraController m_cameraControllerEstomac;
    [SerializeField] private ShootController m_shootControllerEstomac;
    [SerializeField] private CanvasGroup m_overlayInactifEstomac;
    [SerializeField] private AudioListener m_audioListenerEstomac;

    private int m_ecranActif = 0; // 0 = poumon, 1 = estomac

    private void Start()
    {
        AppliquerEtat();
        OscManager.Instance.AddCallback("/switch", OnSwitch);
    }

    private void OnDestroy()
    {
        OscManager.Instance.RemoveCallback("/switch", OnSwitch);
    }

    private void OnSwitch(string address, OscJack.OscDataHandle data)
    {
        m_ecranActif = m_ecranActif == 0 ? 1 : 0;
        AppliquerEtat();
    }

    private void AppliquerEtat()
    {
        bool poumonActif = m_ecranActif == 0;

        m_cameraControllerPoumon.enabled = poumonActif;
        m_shootControllerPoumon.enabled = poumonActif;
        m_cameraControllerEstomac.enabled = !poumonActif;
        m_shootControllerEstomac.enabled = !poumonActif;

        m_overlayInactifPoumon.alpha = poumonActif ? 0f : 1f;
        m_overlayInactifPoumon.blocksRaycasts = !poumonActif;
        m_overlayInactifEstomac.alpha = poumonActif ? 1f : 0f;
        m_overlayInactifEstomac.blocksRaycasts = poumonActif;

        m_audioListenerPoumon.enabled = poumonActif;
        m_audioListenerEstomac.enabled = !poumonActif;
    }
}