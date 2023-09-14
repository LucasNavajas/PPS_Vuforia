using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
        dialogueText.text = "";
    }

    void Update()
    {
        if (sonidoAmbiental && !ambiental)
        {
            // Asigna el clip de audio al AudioSource
            sourceSonidoAmbiental.clip = sonidoAmbiental;

            // Configura el AudioSource para que reproduzca en bucle
            sourceSonidoAmbiental.loop = true;

            // Inicia la reproducción del sonido ambiental
            sourceSonidoAmbiental.Play();
            ambiental = true;
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
        if (Input.GetMouseButtonDown(0))
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }
        StartDialogue();
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

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
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
                    if (Input.GetMouseButtonDown(0))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier * 0.3F);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay * 0.3F);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
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
            npc.ResetIsClicked();
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
                npc.ResetIsClicked();
            }
        }
    }
}

