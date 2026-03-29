using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision){
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(player != null && player.currentForm is StoneForm){
            Destroy(gameObject);
        }
    }

}
