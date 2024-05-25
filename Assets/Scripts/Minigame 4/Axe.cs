using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private GameObject blood;
    private Vector3 target;
    private Vector3 start = new Vector3(0, -8, 0);
    private Vector3 direction;
    private float throwSpeed = 32f;

    private float minSize = 0.6f;
    private float maxSize = 1.5f;

    private float rotationSpeed = 800f;

    private float maxThrowDistance = 15f;

    private GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager4.GetIsThrowing())
            return;

        if (target != null)
        {
            if (targetObject != null)
            {
                direction = (targetObject.transform.position - transform.position).normalized;
            }
            float distance = (target - transform.position).magnitude;
            transform.position += Time.fixedDeltaTime * throwSpeed * direction;

            float scaleFactor = maxThrowDistance - (transform.position.y + 9f);
            float scale = Mathf.Lerp(minSize, maxSize, Mathf.InverseLerp(0, maxThrowDistance, scaleFactor));
            transform.localScale = new Vector3(scale, scale, scale);

            transform.Rotate(Vector3.forward, rotationSpeed * Time.fixedDeltaTime);

            if ((transform.position.y > target.y - 1f) || (targetObject && (targetObject.transform.position - transform.position).magnitude < 0.2f))
            {
                if (targetObject)
                {
                    Instantiate(blood, new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + 1, targetObject.transform.position.z), Quaternion.identity);
                    Destroy(targetObject);
                }
                Destroy(gameObject);
            }
        }

    }
    public void SetTarget(Vector3 position)
    {
        target = position;
        direction = (target - start).normalized;
    }

    public void SetTargetObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
    }
}
