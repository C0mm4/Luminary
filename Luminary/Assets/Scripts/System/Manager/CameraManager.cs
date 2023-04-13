using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Vector3 targetPosition;    // 카메라가 이동할 좌표
    public float moveSpeed = 20f;      // 카메라 이동 속도

    private bool isMoving = false;    // 이동 중인지 여부

    void Update()
    {
        // 클릭 이벤트로 이동 명령을 받았을 때
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast로 마우스 클릭한 위치를 화면 좌표에서 월드 좌표로 변환
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 클릭한 위치로 이동 명령을 받음
                targetPosition = hit.point;
                isMoving = true;
            }
        }

        // 이동 명령이 있으면 이동 처리
        if (isMoving)
        {
            // 대상 좌표까지 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 이동 완료 후 이동 명령 초기화
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}