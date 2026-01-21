using UnityEngine;

public class Chick : MonoBehaviour
{
   private Animator animController;
   public GameObject collectEffect;
   private void Awake()
    {
        animController = GetComponent<Animator>();
        animController.SetFloat("Offset", Random.Range(0f, 1f));
    }

   private void OnTriggerEnter(Collider other)
{
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().CollectChick();
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
   }
}
