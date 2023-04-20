using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera camera;

    public Transform player;
    public SpriteRenderer background;

    float cameraHeight;
    float cameraWidth;


    void Start()
    {
    }
    void Update()
    {
        Debug.Log("test");
        cameraHeight = camera.orthographicSize;
        cameraWidth = cameraHeight * camera.aspect;
    }

    void LateUpdate()
    {
        Vector3 targetPos = player.position;

        float minX = background.bounds.min.x + cameraWidth;
        Debug.Log("minX" + minX);
        float maxX = background.bounds.max.x - cameraWidth;
        Debug.Log("maxX" + maxX);
        float minY = background.bounds.min.y + cameraHeight;
        Debug.Log("minY" + minY);
        float maxY = background.bounds.max.y - cameraHeight;
        Debug.Log("maxY" + maxY);
        //Limit camera movement range

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        Debug.Log("targetPos.x : " + targetPos.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);
        Debug.Log("targetPos.y : " + targetPos.y);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
        // Relatively smooth tracking of playr positions


    }




    /*public Vector3 targetPosition;    // ī�޶� �̵��� ��ǥ
    public float moveSpeed = 20f;      // ī�޶� �̵� �ӵ�

    private bool isMoving = false;    // �̵� ������ ����

    void Update()
    {
        // Ŭ�� �̺�Ʈ�� �̵� ����� �޾��� ��
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast�� ���콺 Ŭ���� ��ġ�� ȭ�� ��ǥ���� ���� ��ǥ�� ��ȯ
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Ŭ���� ��ġ�� �̵� ����� ����
                targetPosition = hit.point;
                isMoving = true;
            }
        }

        // �̵� ����� ������ �̵� ó��
        if (isMoving)
        {
            // ��� ��ǥ���� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // �̵� �Ϸ� �� �̵� ��� �ʱ�ȭ
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }*/ //saved it for just in case
}