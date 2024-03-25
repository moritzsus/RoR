using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    private float bgLength, startpos;
    public float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        bgLength = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + bgLength) startpos += bgLength;
        else if (temp < startpos - bgLength) startpos -= bgLength;
    }
}
