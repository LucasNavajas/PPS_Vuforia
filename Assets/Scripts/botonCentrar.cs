using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonCentrar : MonoBehaviour
{
    public Transform modelo;
    public Transform dialogo;

    public void RotarEnXPositivo()
    {
        if (modelo != null)
        {
            modelo.Rotate(10f, 0f, 0f); // Rota 10 grados en el eje X.
        }
        else
        {
            Debug.LogWarning("El modelo no está asignado en el Inspector.");
        }
    }
    public void RotarEnXNegativo()
    {
        if (modelo != null)
        {
            modelo.Rotate(-10f, 0f, 0f); // Rota 10 grados en el eje X.
        }
        else
        {
            Debug.LogWarning("El modelo no está asignado en el Inspector.");
        }
    }

    public void RotarEnYPositivo()
    {
        if (modelo != null)
        {
            modelo.Rotate(0f, 10f, 0f); // Rota 10 grados en el eje Y.
        }
        else
        {
            Debug.LogWarning("El modelo no está asignado en el Inspector.");
        }
    }

    public void RotarEnYNegativo()
    {
        if (modelo != null)
        {
            modelo.Rotate(0f, -10f, 0f); // Rota 10 grados en el eje Y.
        }
        else
        {
            Debug.LogWarning("El modelo no está asignado en el Inspector.");
        }
    }

    public void RotarEnZPositivo()
    {
        if (modelo != null)
        {
            modelo.Rotate(0f, 0f, 10f); // Rota 10 grados en el eje Z.
        }
        else
        {
            Debug.LogWarning("El modelo no está asignado en el Inspector.");
        }
    }
    public void RotarEnZNegativo()
    {
        if (modelo != null)
        {
            modelo.Rotate(0f, 0f, -10f); // Rota 10 grados en el eje Z.
        }
        else
        {
            Debug.LogWarning("El modelo no está asignado en el Inspector.");
        }
    }

    public void ZoomPositivo()
    {
        if (modelo != null)
        {
            // Aumenta la escala en 0.1 unidades en todos los ejes.
            modelo.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            dialogo.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            Debug.LogWarning("El modelo no está asignado en el Inspector.");
        }
    }

    public void ZoomNegativo()
    {
        if (modelo != null)
        {
            // Disminuye la escala en 0.1 unidades en todos los ejes.
            modelo.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            dialogo.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            Debug.LogWarning("El modelo no está asignado en el Inspector.");
        }
    }
}
