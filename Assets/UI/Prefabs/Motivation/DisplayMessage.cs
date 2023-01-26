using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using static DGP.Utility;
using TMPro;

public class DisplayMessage : MonoBehaviour {

    [Tooltip("Textbox should be defined here")]
    public TextMeshProUGUI textbox;
    
    [SerializeField]
    private RectTransform UItransform;

    [SerializeField]
    private float showTime = 10f;
    
    [SerializeField]
    private float transitionTime = 0.3f;

    private void OnValidate() {
        if (UItransform == null) {
            UItransform = GetComponent<RectTransform>();
        }

        if (textbox == null) {
            textbox = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Awake() {
        transitionOut();    
    }

    public void displayMessageMomentarely(string message) {
        textbox.SetText(message);
        StartCoroutine(displayMessage());
    }

    private IEnumerator displayMessage() {
        yield return StartCoroutine(transitionIn(transitionTime));
        yield return new WaitForSeconds(showTime); // Is unscaled time the time that can be set in the editor?
        yield return StartCoroutine(transitionOut(transitionTime));
    }

    private IEnumerator transitionIn(float transitionTime = 0.1f) => transition(Vector3.zero, Vector3.one, transitionTime);
    private IEnumerator transitionOut(float transitionTime = 0.1f) => transition(Vector3.one, Vector3.zero, transitionTime);

    private IEnumerator transition(Vector3 start, Vector3 end, float transitionTime) {
        UItransform.localScale = start;

        float time = Time.time;
        while (Time.time - transitionTime <= time) {
            float timeSpent = Time.time - time;
            float t = timeSpent / transitionTime;

            UItransform.localScale = Vector3.Slerp(start, end, t);
            yield return new WaitForEndOfFrame();
        }

        UItransform.localScale = end;
    }
}