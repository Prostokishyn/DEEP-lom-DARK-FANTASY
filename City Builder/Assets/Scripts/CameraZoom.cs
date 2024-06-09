using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 0.025f; // �������� ��������� / ��������� ��������
    public float moveSpeed = 0.025f; // �������� ����������� ������

    public float minZoom = 2f; // ̳�������� ������� ������
    public float maxZoom = 10f; // ������������ ������� ������

    public float minX = -10f; // ̳������� �������� X ���������� ������
    public float maxX = 10f; // ����������� �������� X ���������� ������
    public float minY = -10f; // ̳������� �������� Y ���������� ������
    public float maxY = 10f; // ����������� �������� Y ���������� ������

    private Vector3 touchStart; // ��������� ������� ������ ��� ����������� ������
    private float velocity; // ������� �������� ���� ��������

    public GameObject pauseInterface;

    void Update()
    {
        if (pauseInterface != null && pauseInterface.activeSelf)
        {
            return;
        }

        // �������� �������� ������ �� �����
        if (Input.touchCount == 1)
        {
            // ����������� ������ ������� �� ������
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                Vector3 newPosition = transform.position - new Vector3(touchDeltaPosition.x * moveSpeed, touchDeltaPosition.y * moveSpeed, 0);

                // ��������� ��������� ������
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                transform.position = newPosition;
            }
        }

        // �������� ���� ������ ��� ����������� / ���������
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ���� �������� ������ � ������� ���������
            float targetOrthoSize = Camera.main.orthographicSize + deltaMagnitudeDiff * zoomSpeed;
            Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetOrthoSize, ref velocity, 0.2f);

            // ��������� ��������
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
    }
}
