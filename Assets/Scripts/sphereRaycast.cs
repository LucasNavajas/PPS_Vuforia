using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class SphereRaycast : MonoBehaviour

{

    public Button zoomButton; // Asigna el bot�n en el Inspector

    private void Start()
    {
        zoomButton.gameObject.SetActive(false);

    }

    private void OnMouseDown()
    {
        if (!zoomButton.gameObject.activeSelf)
        {
            //Meter los dialogos
            zoomButton.gameObject.SetActive(true);

        }
        else
        {
            //Sacar los dialogos
            Camera.main.transform.position = Vector3.zero; // O la posici�n inicial de tu c�mara AR
            zoomButton.gameObject.SetActive(false);
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
                        if (!zoomButton.gameObject.activeSelf)
                        {
                            //Meter los dialogos
                            zoomButton.gameObject.SetActive(true);
                        }
                        else
                        {
                            //Sacar los dialogos
                            Camera.main.transform.position = Vector3.zero; // O la posici�n inicial de tu c�mara AR
                            zoomButton.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}


