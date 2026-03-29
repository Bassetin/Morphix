using UnityEngine;

public class WindZone : MonoBehaviour
{
   public float force = 10f;

   void OnTriggerStay2D(Collider2D other){
    PlayerController player = other.GetComponent<PlayerController>();

    if(player != null && player.currentForm is LightForm){
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * force);
    }
   }
}
