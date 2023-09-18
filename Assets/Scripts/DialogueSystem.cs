using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI numeroPag;

    public Transform dialogueBoxGUI;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.F;
    public List<NPC> npcList = new List<NPC>();

    public string Names;

    public string[] dialogueLines;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;
    public bool ambiental = false;

    public AudioClip audioClip;
    AudioSource audioSource;

    public AudioClip sonidoAmbiental;
    public AudioSource sourceSonidoAmbiental;

    private bool isMobile = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
        dialogueText.text = "";

        // Detectar si la plataforma es móvil
        if (Application.isMobilePlatform)
        {
            isMobile = true;
        }
    }

    void Update()
    {
        if (sonidoAmbiental && !ambiental)
        {
            StopAllAudioSources();
            // Asigna el clip de audio al AudioSource
            sourceSonidoAmbiental.clip = sonidoAmbiental;

            // Configura el AudioSource para que reproduzca en bucle
            sourceSonidoAmbiental.loop = true;

            // Inicia la reproducción del sonido ambiental
            sourceSonidoAmbiental.Play();
            ambiental = true;
        }

        if (isMobile)
        {
            HandleMobileInput();
        }
        else
        {
            HandlePCInput();
        }
    }
    public void StopAllAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
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
                    if (!dialogueActive)
                    {
                        dialogueActive = true;
                        StartCoroutine(StartDialogue());
                    }
                }
            }
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
                if (!dialogueActive)
                {
                    dialogueActive = true;
                    StartCoroutine(StartDialogue());
                }
            }
        }
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;

        if (dialogueActive && !letterIsMultiplied)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayString(dialogueLines[0]));
        }
        else if (!dialogueActive)
        {
            StartCoroutine(StartDialogue());
        }
    }

    private IEnumerator StartDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));
                    numeroPag.SetText("(" + currentDialogueIndex + "/" + dialogueLength + ")");
                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                        numeroPag.SetText("(" + currentDialogueIndex + "/" + dialogueLength + ")");
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (Input.GetMouseButtonDown(0) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    float delay = isMobile ? letterDelay * letterMultiplier * 0.3F : letterDelay * 0.3F;
                    yield return new WaitForSeconds(delay);

                    if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (isMobile ? Input.touchCount > 0 : Input.GetMouseButtonDown(0))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    public void DropDialogue()
    {
        dialogueBoxGUI.gameObject.SetActive(false);
        sourceSonidoAmbiental.Stop();
        ambiental = false;
        sourceSonidoAmbiental = null;
        sonidoAmbiental = null;
        foreach (var npc in npcList)
        {
            npc.gameObject.SetActive(true);
            npc.ResetIsClicked();
            Collider npcCollider = npc.GetComponent<Collider>();
            if (npcCollider != null)
            {
                npcCollider.enabled = true;
            }
        }
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogueBoxGUI.gameObject.SetActive(false);
            sourceSonidoAmbiental.Stop();
            ambiental = false;
            sourceSonidoAmbiental = null;
            sonidoAmbiental = null;
            foreach (var npc in npcList)
            {
                npc.gameObject.SetActive(true);
                npc.ResetIsClicked();
                Collider npcCollider = npc.GetComponent<Collider>();
                if (npcCollider != null)
                {
                    npcCollider.enabled = true;
                }
            }
        }
    }
}


