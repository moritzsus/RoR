using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawner4 : MonoBehaviour
{
    [SerializeField] private GameObject guard;

    private float[] heightValues = { -6f, -1f, 2.5f };
    private float xSpawnPosLeft = -14.6f;
    private float xSpawnPosRight = 14.6f;
    private float xSpawnPosRightTopLevel = -3.3f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGuard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnGuard()
    {
        float timeToSpawn = Random.Range(1f, 3f);
        int heightLevel = Random.Range(0, 3);
        int side = Random.Range(0, 2);


        GameObject spawnedGuard = Instantiate(guard, new Vector3(0, heightValues[heightLevel], 0), Quaternion.identity);
        SpriteRenderer sr = spawnedGuard.GetComponent<SpriteRenderer>();
        if (heightLevel == 0)
            sr.sortingLayerName = "Player0";
        else if (heightLevel == 1)
            sr.sortingLayerName = "Player1";
        else
            sr.sortingLayerName = "Player2";

        if (side == 0)
        {
            spawnedGuard.transform.position = new Vector3(xSpawnPosLeft, heightValues[heightLevel], 0);
            spawnedGuard.transform.localScale = new Vector3(-1, 1, 1);
            GuardMovement4 guardScript = spawnedGuard.GetComponent<GuardMovement4>();
            guardScript.SetMoveRight(true);
        }
        else
        {
            if (heightLevel == 2)
                spawnedGuard.transform.position = new Vector3(xSpawnPosRightTopLevel, heightValues[heightLevel], 0);
            else
                spawnedGuard.transform.position = new Vector3(xSpawnPosRight, heightValues[heightLevel], 0);

            spawnedGuard.transform.localScale = new Vector3(1, 1, 1);
            GuardMovement4 guardScript = spawnedGuard.GetComponent<GuardMovement4>();
            guardScript.SetMoveRight(false);
        }

        Invoke("SpawnGuard", timeToSpawn);
    }
}
