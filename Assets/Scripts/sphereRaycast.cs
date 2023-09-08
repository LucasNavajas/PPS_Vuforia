using UnityEngine;

public class SphereRaycast : MonoBehaviour
{
    void Update()
    {
        // Comprobar si hay al menos un toque en la pantalla.
        if (Input.touchCount > 0)
        {
            // Obtener el primer toque (�ndice 0).
            Touch touch = Input.GetTouch(0);

            // Comprobar si el toque ha comenzado.
            if (touch.phase == TouchPhase.Began)
            {
                // Aqu� puedes realizar acciones cuando el toque comienza.

                // Realizar raycasting en funci�n de las coordenadas del toque.
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Comprobar si el rayo colisiona con la esfera u otro objeto deseado.
                    if (hit.collider.CompareTag("SphereTag"))
                    {
                        // El toque ha comenzado en la esfera.
                        // Puedes realizar acciones espec�ficas aqu�.
                        print("Toque comenzado en la esfera");
                    }
                }
            }
        }
    }
}


