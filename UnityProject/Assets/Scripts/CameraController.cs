using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target1, target2,camerafocus;
    Camera cam;
    float distance;
    [SerializeField] gameController GM;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        if (distance > 10)
            cam.orthographicSize = distance / 2.5f;
        else
            cam.orthographicSize = 7;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target1.position, target2.position);
        if (distance > 17.5 && !GM.finished)
            cam.orthographicSize = distance / 2.5f;
        if (GM.finished)
        {
            camerafocus.position = Vector3.Lerp(transform.position, new Vector3(target1.position.x, target1.position.y, -10),.1f);
            cam.orthographicSize = 4;
        }
        else
            camerafocus.position = new Vector3((target1.position.x + target2.position.x) / 2, (target1.position.y + target2.position.y) / 2, -10);
    }
}
