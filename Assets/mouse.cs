using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; // Set this to be the distance you want the object to be placed in front of the camera.
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objectPos;
    }
}
