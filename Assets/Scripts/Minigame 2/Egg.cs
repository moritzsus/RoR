using UnityEngine;

public class Egg : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // TODO show Egg picked up Message, place egg at finish, change chicken colliders?
            // TODO only finish when egg collected
            Game2Manager.GetInstance().SetEggStolen(true);
            Destroy(gameObject);
        }
    }
}
