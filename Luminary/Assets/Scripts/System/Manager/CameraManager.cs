using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Vector3 targetPosition;    // ī�޶� �̵��� ��ǥ
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
    }
}