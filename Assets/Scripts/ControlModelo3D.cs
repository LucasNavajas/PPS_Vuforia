using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlModelo3D : MonoBehaviour
{
    private float rotationSpeed = 10.0f;
    private float minZoom = 0.1f;
    private float maxZoom = 5.0f;
    private Vector2[] lastTouchPositions = new Vector2[2];
    private Vector2[] lastTouchDeltaPositions = new Vector2[2];
    public Transform dialogo;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            // Detectar un gesto de pellizco (pinch gesture)
            Vector2[] newTouchPositions = { Input.GetTouch(0).position, Input.GetTouch(1).position };
            Vector2[] newTouchDeltaPositions = { Input.GetTouch(0).deltaPosition, Input.GetTouch(1).deltaPosition };

            float oldDistance = Vector2.Distance(lastTouchPositions[0], lastTouchPositions[1]);
            float newDistance = Vector2.Distance(newTouchPositions[0], newTouchPositions[1]);

            float zoomDelta = newDistance - oldDistance;

            // Aplicar el zoom
            Vector3 newScale = transform.localScale + Vector3.one * zoomDelta * 0.01f;
            newScale = Vector3.ClampMagnitude(newScale, maxZoom);
            newScale = Vector3.Max(newScale, Vector3.one * minZoom);
            transform.localScale = newScale;
            dialogo.localScale = newScale;

            lastTouchPositions = newTouchPositions;
            lastTouchDeltaPositions = newTouchDeltaPositions;
        }
        else
        {
            // Control de rotación en los ejes X e Y
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 deltaTouch = touch.deltaPosition;
                    transform.Rotate(Vector3.up * -deltaTouch.x * rotationSpeed * Time.deltaTime, Space.World);
                    transform.Rotate(Vector3.right * deltaTouch.y * rotationSpeed * Time.deltaTime, Space.World);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 mouseDelta = (Vector2)Input.mousePosition - lastTouchPositions[0];
                transform.Rotate(Vector3.up * -mouseDelta.x * rotationSpeed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.right * mouseDelta.y * rotationSpeed * Time.deltaTime, Space.World);
            }

            float zoomDeltaMouse = Input.GetAxis("Mouse ScrollWheel") * 1.1f;
            Vector3 newScaleMouse = transform.localScale + Vector3.one * zoomDeltaMouse;
            newScaleMouse = Vector3.ClampMagnitude(newScaleMouse, maxZoom);
            newScaleMouse = Vector3.Max(newScaleMouse, Vector3.one * minZoom);
            transform.localScale = newScaleMouse;
            dialogo.localScale = newScaleMouse;

            // Actualizar la posición del último toque/ratón
            lastTouchPositions[0] = Input.mousePosition;
        }
    }
}







