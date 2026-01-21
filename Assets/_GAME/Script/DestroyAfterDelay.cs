using System.Dynamic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 2f;

    void Start ()
    {
        Invoke("SelfDestruct", delay);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
