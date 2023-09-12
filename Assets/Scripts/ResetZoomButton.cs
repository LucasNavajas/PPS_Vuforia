using UnityEngine;
using UnityEngine.UI;

public class ResetZoomButton : MonoBehaviour
{
    public Transform targetTransform; // Asigna el Transform del contenedor del modelo 3D en el Inspector

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ResetZoom);
    }

    private void ResetZoom()
    {
        // Devuelve la posici�n de la c�mara a su posici�n original
        Camera.main.transform.position = Vector3.zero; // O la posici�n inicial de tu c�mara AR
        // Devuelve la posici�n y escala del modelo 3D a su estado original
        targetTransform.localPosition = Vector3.zero; // O la posici�n inicial del modelo 3D
        targetTransform.localScale = Vector3.one; // O la escala inicial del modelo 3D
        gameObject.SetActive(false);
    }
}
