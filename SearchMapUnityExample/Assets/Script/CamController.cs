using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    Vector3 cameraPosition = new Vector3(0, 4, -3);
    Transform playerTransform;

    public float cameraMoveSpeed;

    private float width;
    private float height;
    private void Start()
    {

        height = Camera.main.orthographicSize;
        width = height / Screen.height * Screen.width;
    }
    public void SetPlayer(Transform transform)
    {
        playerTransform = transform;
    }
    private void LateUpdate()
    {
        if (playerTransform != null)
        {


            transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition,
             Time.deltaTime * cameraMoveSpeed);

            /*

            float lx = mapSize.x - width;

            float clampX = Mathf.Clamp(transform.position.x, -lx, lx);

            float ly = mapSize.y - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly, ly);



            transform.position = new Vector3(clampX, clampY, -10f);
            */
        }

    }
}
