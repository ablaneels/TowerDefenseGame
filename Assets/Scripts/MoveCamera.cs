using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 5.0f;

    private void Update()
    {
        if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
                float scroll = Input.GetAxis ("Mouse ScrollWheel");

                transform.Translate(0, scroll * speed, 0, Space.World);


            }

            if (Input.GetAxis ("Mouse ScrollWheel") > 0) {

                float scroll = Input.GetAxis ("Mouse ScrollWheel");
                transform.Translate(0, scroll * speed, 0, Space.World);
            }
    }
}
