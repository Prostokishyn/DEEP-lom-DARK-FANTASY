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

    private Vector3 touchStart; // Початкова позиція дотику для пересування камери
    private float velocity; // Поточна швидкість зміни масштабу

    public GameObject pauseInterface;

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
                Vector3 newPosition = transform.position - new Vector3(touchDeltaPosition.x * moveSpeed, touchDeltaPosition.y * moveSpeed, 0);

                // Обмеження координат камери
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                transform.position = newPosition;
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

            // Зміна масштабу камери з плавним переходом
            float targetOrthoSize = Camera.main.orthographicSize + deltaMagnitudeDiff * zoomSpeed;
            Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetOrthoSize, ref velocity, 0.2f);

            // Обмеження масштабу
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
    }
}
