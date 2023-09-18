using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public Transform ChatBackGround;
    public Transform NPCCharacter;
    private bool isClicked = false;
    private DialogueSystem dialogueSystem;
    public AudioClip sonidoAmbiental;
    AudioSource sourceSonidoAmbiental;
    public List<NPC> npcList = new List<NPC>();

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    private bool isMobile = false;

    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        sourceSonidoAmbiental = GetComponent<AudioSource>();

        // Detectar si la plataforma es móvil
        if (Application.isMobilePlatform)
        {
            isMobile = true;
        }
    }

    private void Update()
    {
        if (isMobile)
        {
            HandleMobileInput();
        }
        else
        {
            HandlePCInput();
        }
    }

    private void HandleMobileInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    foreach (var npc in npcList)
                    {
                        if (npc != this)
                        {
                            npc.gameObject.SetActive(false);
                        }
                        npc.ResetIsClicked();
                        Collider npcCollider = npc.GetComponent<Collider>();
                        if (npcCollider != null)
                        {
                            npcCollider.enabled = false;
                        }
                    }
                    dialogueSystem.EnterRangeOfNPC();
                    dialogueSystem.Names = Name;
                    dialogueSystem.dialogueLines = sentences;
                    dialogueSystem.sourceSonidoAmbiental = sourceSonidoAmbiental;
                    dialogueSystem.sonidoAmbiental = sonidoAmbiental;
                    dialogueSystem.NPCName();
                    isClicked = true;
                }
            }
        }

        if (isClicked)
        {
            Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
            Pos.y += 275;
            ChatBackGround.position = Pos;
        }
    }

    private void HandlePCInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                foreach (var npc in npcList)
                {
                    if (npc != this)
                    {
                        npc.gameObject.SetActive(false);
                    }
                    npc.ResetIsClicked();
                    Collider npcCollider = npc.GetComponent<Collider>();
                    if (npcCollider != null)
                    {
                        npcCollider.enabled = false;
                    }
                }
                dialogueSystem.EnterRangeOfNPC();
                dialogueSystem.Names = Name;
                dialogueSystem.dialogueLines = sentences;
                dialogueSystem.sourceSonidoAmbiental = sourceSonidoAmbiental;
                dialogueSystem.sonidoAmbiental = sonidoAmbiental;
                dialogueSystem.NPCName();
                isClicked = true;
            }
        }

        if (isClicked)
        {
            Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
            Pos.y += 275;
            ChatBackGround.position = Pos;
        }
    }

    public void ResetIsClicked()
    {
        isClicked = false;
    }
}



