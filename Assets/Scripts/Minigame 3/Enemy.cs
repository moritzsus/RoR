using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private PlayerController opponent;

    private Animator anim;

    private readonly string animBoolIsDead = "isDead";

    // Start is called before the first frame update
    void Start()
    {
        if (opponent == null)
        {
            Debug.LogWarning("opponent null: Assign opponent field for enemy in the editor");
        }
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecieveDamage(int amount)
    {
        health -= amount;
        if (health < 1)
        {
            anim.SetBool(animBoolIsDead, true);
        }

        Debug.Log("Enemy Health: " + health);
    }
}
