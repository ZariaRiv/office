using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class tasks_script : MonoBehaviour
{
public void AcceptTask(GameObject thing){
        Destroy(thing, 1);
        SceneManager.LoadScene("minigame_holder_scene");
    }

public void WinGameButton(){
        SceneManager.LoadScene("ProjectFinished");
}

public void CloseWindow(){
        SceneManager.LoadScene("SampleScene");
}

public void DeclineProject(GameObject thing){
        Destroy(thing, 1);
}

void Update()
    {
        
    }
 }