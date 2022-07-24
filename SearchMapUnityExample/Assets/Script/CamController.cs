using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    
    Transform playerTransform;

    public float cameraMoveSpeed;

    private int camMode;//0: 1인칭 1: 3인칭
    public float maxDistance;
    public float minDistance ;
    public  float cameraDistance =5.0f;//3인칭 카메라 거리
    public float turnSpeed = 4.0f; // 마우스 회전 속도    
    private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )
    Vector3 cameraPosition;
    private void Start()
    {
        cameraPosition = new Vector3(0, cameraDistance, -5);
    }
    public void SetPlayer(Transform transform)
    {
        playerTransform = transform;
        ChangeMode(1);
    }
    public void ChangeMode(int mode)
    {
        camMode = mode;
        if (camMode == 0)
        {
            transform.position = playerTransform.Find("viewPoint").position;
        }
        else if(camMode == 1)
        {
            transform.position = playerTransform.position - playerTransform.forward + Vector3.up;
            transform.LookAt(playerTransform.position);
            transform.position = playerTransform.position - playerTransform.forward *cameraDistance;


        }
    }
    public int GetCamMode()
    {
        return camMode;
    }
    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            if (camMode == 0)
            {
                float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
                // 현재 y축 회전값에 더한 새로운 회전각도 계산
                float yRotate = transform.eulerAngles.y + yRotateSize;
                
                // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
                float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
                // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
                // Clamp 는 값의 범위를 제한하는 함수
                xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

                // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
                transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
                transform.position = playerTransform.Find("viewPoint").position;

            }
            else if (camMode == 1)
            {
                float wheel = Input.GetAxis("Mouse ScrollWheel");
                cameraDistance -= wheel * 4f;
                cameraDistance = Mathf.Clamp(cameraDistance, minDistance, maxDistance);

                Vector2 mouseMove = Vector2.zero;
                if (Input.GetMouseButton(1))
                {
                    mouseMove.y = Input.GetAxis("Mouse X") * turnSpeed;
                    mouseMove.x = -Input.GetAxis("Mouse Y") * turnSpeed;

                    
                    //transform.rotation = Quaternion.Euler(xmove, ymove, 0);
                    //transform.eulerAngles = new Vector3(mouseMove.x, mouseMove.y, 0);
                    if (mouseMove.magnitude != 0)
                    {
                        Quaternion q = transform.rotation;
                        float newx =  Mathf.Clamp(q.eulerAngles.x + mouseMove.x, 0, 70);
                        q.eulerAngles = new Vector3(newx, q.eulerAngles.y + mouseMove.y , q.eulerAngles.z);
                        transform.rotation = q;

                    }
                    transform.position = playerTransform.position - transform.forward * cameraDistance;

                    /*
                    방향 벡터 구해서 
                    해당 위치로 원모양으로 돌려주는 코드(초기 방식)
                    //transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
                    Vector3 tmp = Vector3.Normalize(transform.position - playerTransform.position);
                    tmp.y = 0;
                    Quaternion q = Quaternion.Euler(new Vector3(xRotate, yRotateSize, 0f));
                    mode1CamVec = q *  mode1CamVec ;

                    transform.position = Vector3.Lerp(transform.position, playerTransform.position + mode1CamVec,
                 Time.deltaTime * cameraMoveSpeed);

                    */

                }
                else
                {
                    transform.position = playerTransform.position - transform.forward * cameraDistance;
                }

            }
        }


    }
}
