using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 0.025f; // Швидкість збільшення / зменшення масштабу
    public float moveSpeed = 0.025f; // Швидкість пересування камери

    public float minZoom = 2f; // Мінімальний масштаб камери
    public float maxZoom = 10f; // Максимальний масштаб камери

    public float minX = -10f; // Мінімальне значення X координати камери
    public float maxX = 10f; // Максимальне значення X координати камери
    public float minY = -10f; // Мінімальне значення Y координати камери
    public float maxY = 10f; // Максимальне значення Y координати камери

    private Vector3 targetPosition; // Цільова позиція камери
    private float targetZoom; // Цільовий масштаб камери

    public GameObject pauseInterface;

    void Start()
    {
        targetPosition = transform.position; // Ініціалізація цільової позиції
        targetZoom = Camera.main.orthographicSize; // Ініціалізація цільового масштабу
    }

    void Update()
    {
        if (pauseInterface != null && pauseInterface.activeSelf)
        {
            return;
        }

        // Перевірка наявності дотиків на екрані
        if (Input.touchCount == 1)
        {
            // Пересування камери пальцем по екрану
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                targetPosition -= new Vector3(touchDeltaPosition.x * moveSpeed, touchDeltaPosition.y * moveSpeed, 0);

                // Обмеження координат камери
                targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            }
        }

        // Перевірка двох дотиків для приближення / віддалення
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Оновлення цільового масштабу з обмеженням
            targetZoom = Mathf.Clamp(Camera.main.orthographicSize + deltaMagnitudeDiff * zoomSpeed, minZoom, maxZoom);
        }

        // Плавне пересування камери до цільової позиції
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);

        // Плавна зміна масштабу камери
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, Time.deltaTime * 10);
    }
}
