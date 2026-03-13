using UnityEngine;

public class VirusSpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_virusPrefabs;
    [SerializeField] private float m_spawnInterval = 2f;
    [SerializeField] private Vector3 m_zoneSize = new Vector3(10f, 10f, 10f);

    private float m_spawnTimer;

    private void Update()
    {
        m_spawnTimer -= Time.deltaTime;
        if (m_spawnTimer <= 0)
        {
            SpawnVirus();
            m_spawnTimer = m_spawnInterval;
        }
    }

    private void SpawnVirus()
    {
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-m_zoneSize.x / 2, m_zoneSize.x / 2),
            Random.Range(-m_zoneSize.y / 2, m_zoneSize.y / 2),
            Random.Range(-m_zoneSize.z / 2, m_zoneSize.z / 2)
        );

        GameObject randomVirus = m_virusPrefabs[Random.Range(0, m_virusPrefabs.Length)];
        Instantiate(randomVirus, randomPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, m_zoneSize);
    }
}