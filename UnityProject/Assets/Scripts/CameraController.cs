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
        if (distance > 9)
            cam.orthographicSize = distance / 3;
        else
            cam.orthographicSize = 3;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(target1.position.x) + Mathf.Abs(target2.position.x);
        camerafocus.position = new Vector3((target1.position.x + target2.position.x) / 2, (target1.position.y + target2.position.y) / 2, -10);
        if (distance > 9)
            cam.orthographicSize = distance / 3;
    }
}
