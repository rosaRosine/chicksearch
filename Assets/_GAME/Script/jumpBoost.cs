using UnityEngine;

public class jumpBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
{
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().SetMaxJump();
            Destroy(gameObject);
        }
   }
}
