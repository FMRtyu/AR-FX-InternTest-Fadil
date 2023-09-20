using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = (Camera)FindObjectOfType(typeof(Camera));

        if (camera)
        {
            transform.LookAt(camera.gameObject.transform);
        }
    }
}
