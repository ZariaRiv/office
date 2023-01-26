using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SendMessage : MonoBehaviour {

    [Tooltip("Add all possible messages here")]
    public List<string> messages;
    [Tooltip("Set the minimum time in between messages here (in minutes)")]
    public float minimumTime;
    [Tooltip("Set the maximum time in between messages here (in minutes)")]
    public float maximumTime;

    [SerializeField]
    private DisplayMessage displayer;

    private void OnValidate() {
        if (displayer == null) {
            displayer = GetComponent<DisplayMessage>();
        }
    }

    private void Awake() {
        StartCoroutine(showMessage());
    }

    // Start is called before the first frame update
    
    private IEnumerator showMessage() {
        float timeToWait = Random.Range(minimumTime, maximumTime);
        int show = Random.Range(0, messages.Count);
        string message = messages[show];

        yield return new WaitForSeconds(timeToWait);

        displayer.displayMessageMomentarely(message);

        StartCoroutine(showMessage());
    }
}
