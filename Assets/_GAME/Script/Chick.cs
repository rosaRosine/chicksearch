using UnityEngine;

public class Chick : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
{
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().CollectChick();
            Destroy(gameObject);
        }
   }
}
