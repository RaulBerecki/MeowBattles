using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(-transform.localScale.x*.03f, 0));
        if (Mathf.Abs(transform.position.x) > 100)
            Destroy(this.gameObject);
    }
}
