using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;



public class tasks_script : MonoBehaviour
{
        public StartSound mySoundScript;

        void Start() 
        {
                mySoundScript = GameObject.FindObjectOfType(typeof(StartSound)) as StartSound;
        }

        public void AcceptTask(){
                //Destroy(thing, 1);
                mySoundScript.PlayButton();
                // Thread.Sleep(1000);
                SceneManager.LoadScene("minigame_holder_scene");
        }

        public void WinGameButton(){
                mySoundScript.PlayButton();
                // Thread.Sleep(1000);
                SceneManager.LoadScene("ProjectFinished");
        }

        public void CloseWindow(){
                mySoundScript.PlayButton();
                // Thread.Sleep(1000);
                SceneManager.LoadScene("SampleScene");
        }

        public void DeclineProject(GameObject thing){
                // Thread.Sleep(100);
                mySoundScript.PlayButton();
                Destroy(thing, 1);
}

void Update()
    {
        
    }
 }