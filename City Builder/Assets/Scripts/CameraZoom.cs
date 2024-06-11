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

    private Vector3 targetPosition; // ֳ����� ������� ������
    private float targetZoom; // ֳ������ ������� ������

    public GameObject pauseInterface;

    void Start()
    {
        targetPosition = transform.position; // ����������� ������� �������
        targetZoom = Camera.main.orthographicSize; // ����������� ��������� ��������
    }

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
                targetPosition -= new Vector3(touchDeltaPosition.x * moveSpeed, touchDeltaPosition.y * moveSpeed, 0);

                // ��������� ��������� ������
                targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
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

            // ��������� ��������� �������� � ����������
            targetZoom = Mathf.Clamp(Camera.main.orthographicSize + deltaMagnitudeDiff * zoomSpeed, minZoom, maxZoom);
        }

        // ������ ����������� ������ �� ������� �������
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);

        // ������ ���� �������� ������
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, Time.deltaTime * 10);
    }
}
