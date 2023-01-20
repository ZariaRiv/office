using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SendMessage : MonoBehaviour
{
    [Tooltip("Textbox should be defined here")]
    public TextMeshProUGUI textbox;

    [Tooltip("Add all possible messages here")]
    public List<string> messages;
    [Tooltip("Set the minimum time in between messages here (in minutes)")]
    public float minimalTime;
    [Tooltip("Set the maximum time in between messages here (in minutes)")]
    public float maximumTime;


    private int messageCount;
    private int messageSelect;
    private int previousMessage;
    private string message;

    private float previousTime;
    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        // Count how many messages are set in the list
        messageCount = messages.Count;
        Debug.Log("There are currently "+messageCount+" motivational messages set.");
        previousMessage = 0;

        // Convert the time values into seconds
        // minimalTime *= 60;
        // maximumTime *= 60;
        // Set default values for first run
        previousTime = 0;
        waitTime = minimalTime;
    }

    // Update is called once per frame
    void Update()
    {
        // gameobject.instantiate to show popup
        // previousTime = Time.time;
        // Debug.Log("Current Time = "+previousTime);

        if (Time.time >= previousTime + waitTime) {
            // Select a random message from the list
            messageSelect = Random.Range(0, messageCount);

            // This if statement makes sure the same message is never shown twice in a row
            if (messageSelect != previousMessage) {
                previousMessage = messageSelect;
                message = messages[messageSelect];

                Debug.Log(message);
                // Change the text in the textbox into the new message
                textbox.text = message;

                previousTime = Time.time;
                waitTime = Random.Range(minimalTime, maximumTime);
                Debug.Log("Current Time = "+previousTime+" | Next Wait Time = "+waitTime);
            }

            
        }
        
    }
}
