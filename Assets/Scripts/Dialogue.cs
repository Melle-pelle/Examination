using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiegeticDialogue : MonoBehaviour
{
    [Header("UI References")]
    public Text dialogueText;
    public GameObject dialoguePanel;
    
    [Header("Dialogue Settings")]
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    
    private int currentLine = 0;
    private bool isTyping = false;
    private bool dialogueActive = false;
    
    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
    
    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLine];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }
    
    public void StartDialogue()
    {
        dialogueActive = true;
        currentLine = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }
    
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        
        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        isTyping = false;
    }
    
    void NextLine()
    {
        currentLine++;
        
        if (currentLine < dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
    
    void EndDialogue()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);
    }
}