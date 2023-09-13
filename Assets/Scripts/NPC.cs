using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public Transform ChatBackGround;
    public Transform NPCCharacter;

    private DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    private void Update()
    {
        // Verifica si se hizo clic en el modelo 3D
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si se ha hecho clic en el modelo 3D, muestra el diálogo
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                dialogueSystem.EnterRangeOfNPC();
                dialogueSystem.Names = Name;
                dialogueSystem.dialogueLines = sentences;
                dialogueSystem.NPCName();
            }
        }

        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 175;
        ChatBackGround.position = Pos;
    }
}


