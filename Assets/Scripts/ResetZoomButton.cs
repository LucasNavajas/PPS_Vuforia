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
        // Devuelve la posición de la cámara a su posición original
        Camera.main.transform.position = Vector3.zero; // O la posición inicial de tu cámara AR
        // Devuelve la posición y escala del modelo 3D a su estado original
        targetTransform.localPosition = Vector3.zero; // O la posición inicial del modelo 3D
        targetTransform.localScale = Vector3.one; // O la escala inicial del modelo 3D
        gameObject.SetActive(false);
    }
}
