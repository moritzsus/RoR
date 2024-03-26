using UnityEngine;

public class Egg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // TODO show Egg picked up Message, place egg at finish, change chicken colliders?
            Game2Manager.SetEggStolen(true);
            Destroy(gameObject);
        }
    }
}
