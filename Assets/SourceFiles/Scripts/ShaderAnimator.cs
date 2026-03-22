using UnityEngine;

public class ShaderAnimator : MonoBehaviour
{
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void OnDisable()
    {
        if (mat != null)
            Destroy(mat);
    }
}