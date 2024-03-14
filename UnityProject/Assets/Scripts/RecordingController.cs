using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingController : MonoBehaviour
{
    int direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(direction * 0.02f, 0));
        if (Mathf.Abs(transform.position.x) > 21)
            direction *= -1;
    }
}
