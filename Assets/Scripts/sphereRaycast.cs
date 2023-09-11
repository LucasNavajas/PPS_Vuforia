using UnityEngine;
using Vuforia;

public class SphereRaycast : MonoBehaviour

{

    private Camera arCamera;
    private Transform targetTransform;
    private bool isZoomed = false;
    public Canvas canvas1; // Asigna el Canvas 1 en el Inspector

    private void Start()
    {
        arCamera = Camera.main;
        targetTransform = transform.parent; // Obt�n el Transform del contenedor del modelo 3D
        canvas1.enabled = false;

    }

    private void OnMouseDown()
    {
        if (!isZoomed)
        {
            // Obt�n la posici�n de la esfera en el espacio del mundo
            Vector3 spherePosition = transform.position;

            // Calcula una nueva posici�n para la c�mara que enfoque la esfera
            Vector3 cameraPosition = spherePosition - arCamera.transform.forward * 2f; // Puedes ajustar la distancia de zoom seg�n tus preferencias

            // Mueve la c�mara a la nueva posici�n
            arCamera.transform.position = cameraPosition;

            // Ajusta la posici�n del modelo 3D para que coincida con la esfera
            targetTransform.position = spherePosition;

            canvas1.enabled = true; // Activa el Canvas 1


            isZoomed = true;
        }
        else
        {
            Camera.main.transform.position = Vector3.zero; // O la posici�n inicial de tu c�mara AR
                                                           // Devuelve la posici�n y escala del modelo 3D a su estado original
            targetTransform.localPosition = Vector3.zero; // O la posici�n inicial del modelo 3D
            targetTransform.localScale = Vector3.one; // O la escala inicial del modelo 3D
            canvas1.enabled = false; // Desactiva el Canvas 1
            isZoomed = false;
        }
    }
    void Update()
    {
        // Verificar si hay toques en la pantalla
        if (Input.touchCount > 0)
        {
            // Obtener el primer toque (puedes iterar sobre Input.touches para manejar m�ltiples toques)
            Touch touch = Input.GetTouch(0);

            // Verificar si el toque est� en fase "Began" (comenz�)
            if (touch.phase == TouchPhase.Began)
            {
                // Realizar acciones cuando se toca la pantalla
                // Por ejemplo, puedes verificar si el toque colisiona con el objeto
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        if (!isZoomed)
                        {
                            // Obt�n la posici�n de la esfera en el espacio del mundo
                            Vector3 spherePosition = transform.position;

                            // Calcula una nueva posici�n para la c�mara que enfoque la esfera
                            Vector3 cameraPosition = spherePosition - arCamera.transform.forward * 2f; // Puedes ajustar la distancia de zoom seg�n tus preferencias

                            // Mueve la c�mara a la nueva posici�n
                            arCamera.transform.position = cameraPosition;

                            // Ajusta la posici�n del modelo 3D para que coincida con la esfera
                            targetTransform.position = spherePosition;
                            canvas1.enabled = true; // Activa el Canvas 1
                            isZoomed = true;
                        }
                        else
                        {
                            Camera.main.transform.position = Vector3.zero; // O la posici�n inicial de tu c�mara AR
                            // Devuelve la posici�n y escala del modelo 3D a su estado original
                            targetTransform.localPosition = Vector3.zero; // O la posici�n inicial del modelo 3D
                            targetTransform.localScale = Vector3.one; // O la escala inicial del modelo 3D
                            canvas1.enabled = false; // Desactiva el Canvas 1
                            isZoomed = false;
                        }
                    }
                }
            }
        }
    }
}


