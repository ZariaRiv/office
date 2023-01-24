using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    public AudioSource button; 
    public AudioSource click;
    public AudioSource error;
    public AudioSource fail;
    public AudioSource projectSuccess;
    public AudioSource nextLevel;
    public AudioSource ui;
    public AudioSource chainsaw;
    public AudioSource circularSaw;
    public AudioSource officeUpgrade;
    public AudioSource notification;
    public AudioSource notificationLowering;
    public AudioSource notificationRising;
    public AudioSource openMenu;
    public AudioSource tromboneFail;
    public AudioSource baseMusic;
    public AudioSource minigameMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayButton() {
        button.Play ();
    }

    public void PlayClick() {
        click.Play ();
    }

    public void PlayError() {
        error.Play ();
    }

    public void PlayFail() {
        fail.Play ();
    }

    public void PlayProjectSuccess() {
        projectSuccess.Play ();
    }

    public void PlayNextLevel() {
        nextLevel.Play ();
    }

    public void PlayUI() {
        ui.Play ();
    }

    public void PlayChainsaw() {
        chainsaw.Play ();
    }

    public void PlayCircularSaw() {
        circularSaw.Play ();
    }

    public void PlayOfficeUpgrade() {
        officeUpgrade.Play ();
    }

    public void PlayNotification() {
        notification.Play ();
    }

    public void PlayNotificationLowering() {
        notificationLowering.Play ();
    }

    public void PlayNotificationRising() {
        notificationRising.Play ();
    }

    public void PlayOpenMenu() {
        openMenu.Play ();
    }

    public void PlayTromboneFail() {
        tromboneFail.Play ();
    }

    public void PlayBaseMusic() {
        baseMusic.Play ();
    }

    public void PlayMinigameMusic() {
        minigameMusic.Play ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
