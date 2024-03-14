using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBirdsController : MonoBehaviour
{
    float timer;
    [SerializeField] GameObject objectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.RandomRange(10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(objectToSpawn, new Vector2(transform.position.x, transform.position.y + Random.RandomRange(-3, 4)),Quaternion.identity);
            timer = Random.RandomRange(10, 20);
        }
    }
}
