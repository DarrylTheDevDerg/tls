using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro

public class TypewriterEffectTMP : MonoBehaviour
{
    // Public variables for dynamic control
    public TextMeshProUGUI dialogueText; // TextMeshProUGUI element
    [TextArea] public string[] dialogueLines; // Array of dialogues
    public float typingSpeed = 0.05f; // Typing speed for characters

    private int currentLineIndex = 0; // Index of the current line being displayed
    private string currentText = "";  // Stores the current text being typed

    // Start typing the first line when the game starts
    void Start()
    {
        if (dialogueLines.Length > 0) // Check if there are dialogue lines
        {
            StartCoroutine(TypeText());
        }
    }

    // Coroutine for typing out the text
    IEnumerator TypeText()
    {
        // Clear the text before starting
        dialogueText.text = "";

        // Grab the current line to type
        string fullText = dialogueLines[currentLineIndex];

        // Loop through each character in the current line
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);  // Get substring up to the current character
            dialogueText.text = currentText;             // Update the TextMeshProUGUI element
            yield return new WaitForSeconds(typingSpeed); // Wait before displaying the next character
        }
    }

    // Function to go to the next line of dialogue
    public void ShowNextLine()
    {
        // Check if there are more lines to show
        if (currentLineIndex < dialogueLines.Length - 1)
        {
            currentLineIndex++; // Move to the next line in the array
            StopAllCoroutines(); // Stop any ongoing typing coroutine
            StartCoroutine(TypeText()); // Start typing the next line
        }
    }

    // Function to start typing a specific line (can be triggered at any time)
    public void StartTyping(string[] newDialogueLines, float newSpeed)
    {
        StopAllCoroutines(); // Stop any currently running typing coroutine

        // Assign new dialogue lines and speed
        dialogueLines = newDialogueLines;
        typingSpeed = newSpeed;
        currentLineIndex = 0; // Reset the index to start at the first line

        // Start typing the first line of the new dialogue
        StartCoroutine(TypeText());
    }
}
