using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOffice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // level counters
    public int levelTR = 0;    // technology & realisation
    public int levelCA = 0;    // creativity & aesthetics
    public int levelMDC = 0;   // math, data & computing
    public int levelUS = 0;    // user & society
    public int levelBE = 0;    // business & entrepeneurship

    // This void can be called whenever a minigame is passed, 
    // it then automatically updates that part of the office
    void UpdateLevel(string department)
    {
        switch(department)
        {
            case "TR":
                if (levelTR < 5){
                    levelTR += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "CA":
                if (levelCA < 5){
                    levelCA += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "MDC":
                if (levelMDC < 5){
                    levelMDC += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "US":
                if (levelUS < 5){
                    levelUS += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;

            case "BE":
                if (levelBE < 5){
                    levelBE += 1;
                }
                else {
                    Debug.Log("Level is already at 5, cannot be higher.");
                }
                break;
        }
    }

    public void UpdateOffices(string department){
        // Update the level counter
        UpdateLevel(department);

        switch(department){
            case "TR":
                switch(levelTR){
                    case 1:
                        //add stuff
                        break;
                    case 2:
                        //more stuff
                        break;
                    case 3:
                        //even more stuff
                        break;
                    case 4:
                        // giant amounts of stuff
                        break;
                    case 5:
                        // almost too much stuff
                        break;
                }
            break;

            //other cases will probably not be done
        }

    }

}
