using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target1, target2,camerafocus;
    Camera cam;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        if (distance > 10)
            cam.orthographicSize = distance / 2.5f;
        else
            cam.orthographicSize = 4;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target1.position, target2.position);
        camerafocus.position = new Vector3((target1.position.x + target2.position.x) / 2, (target1.position.y + target2.position.y) / 2, -10);
        if (distance > 10)
            cam.orthographicSize = distance / 2.5f;
    }
}
