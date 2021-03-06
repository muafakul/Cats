﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeDialManager : MonoBehaviour {

    public float startingTime;
    public int finalCountdownAmount;

    public Text displayFinalCountdown;
    private AudioSource finalCountdownSound;

	private SoundStatementSet soundStatementSet;

    private Image radialImage;
    private float currentTime;
    private bool paused;
    private int lastFinalCountdown; // Prevents looping the finalCountdownSound by storing the previous amount


    // Use this for initialization
    void Start () {

        paused = false;
        radialImage = GetComponent<Image>();
        currentTime = startingTime;

        displayFinalCountdown.enabled = false;

        finalCountdownSound = GetComponent<AudioSource>();

        lastFinalCountdown = finalCountdownAmount; 

		soundStatementSet = GetComponent<SoundStatementSet>();
	}

	public void FailedGame(){
		if(soundStatementSet) {
			soundStatementSet.RandomStatement();
		}
	}
	// Update is called once per frame
	void Update () {

        if (!paused && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            radialImage.fillAmount = currentTime / startingTime;
//			if(currentTime <= 0) {
//				if(soundStatementSet) {
//					soundStatementSet.RandomStatement();
//				}
//			}

        }

        if(currentTime <= finalCountdownAmount){
            displayFinalCountdown.enabled = true;

            displayFinalCountdown.text = Mathf.CeilToInt(currentTime).ToString();

            //plays sound only when the number changes
            if ((lastFinalCountdown == Mathf.CeilToInt(currentTime)) && (currentTime > 0)){
//                Debug.Log("Played");
                finalCountdownSound.Play();

                lastFinalCountdown--;
            }
      
        }

        if(currentTime <= 0)
        {
            displayFinalCountdown.enabled = false;
        }

    }

	public void ResetTimer(int delay)
    {
        currentTime = startingTime + 11;
		currentTime += delay;
        displayFinalCountdown.enabled = false;
        lastFinalCountdown = finalCountdownAmount;
		Camera.main.GetComponent<AudioSource> ().PlayDelayed (delay);
    }

    public void StartTimer()
    {
        paused = false;
    }

    public void StopTimer()
    {
        paused = true;
    }

    public bool CheckIfTimeIsUp()
    {
        if (currentTime <= 0)
            return true;
        else
            return false;
    }
}
