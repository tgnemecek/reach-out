﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroSequence : MonoBehaviour
{
    private bool hasSkipped = false;
    string[] narrationText =
    {
        "...a violent and ruthless mercenary who would do anything to get the job done.",
        "However, one day, you realized that you like people and want to get to know them better in a friendly sort of way.",
        "This was also the day that your ship’s gun simply wouldn’t stop firing.",
        "Reach out"
    };
    private FMOD.Studio.EventInstance audioEventInstance;
    State currentState;
    [SerializeField]
    Image fadePannel;
    [SerializeField]
    TextMeshProUGUI narration;
    [SerializeField]
    Transform bottomTarget;
    [SerializeField]
    Transform topTarget;
    [SerializeField]
    Transform midTarget;
    [SerializeField]
    CanvasGroup firstImages;
    [SerializeField]
    CanvasGroup secondImages;
    [SerializeField]
    CanvasGroup thirdImages;
    [SerializeField]
    CanvasGroup fourthImage;
    [SerializeField]
    CanvasGroup textAlpha;


    FadeInState start;
    WaitState LetTextStay1;
    ObjectTranslateState moveTextDown;
    FadeUIInState ShowFirstImages;
    WaitState LetImagesStay1;
    FadeOutState hideFirstScreen;
    FadeUIOutState hideFirstImages;
    TextChangeState changeToSecondNarration;
    SetObjectPositionSTate ResetNarrationPosition;


    FadeInState showSecondNarration;
    WaitState letNarrationStay2;
    ObjectTranslateState moveSecondNarrationDown;
    FadeUIInState showSecondImages;
    WaitState LetImagesStay2;
    FadeOutState hideSecondScreen;
    FadeUIOutState hideSecondImages;
    TextChangeState changeToThirdNarration;
    SetObjectPositionSTate ResetNarrationPosition2;


    FadeInState showThirdNarration;
    WaitState letNarrationStay3;
    ObjectTranslateState moveThirdNarrationDown;
    FadeUIInState showThirdImages;
    WaitState LetImagesStay3;
    FadeOutState hideThirdScreen;
    FadeUIOutState hideThirdImages;
    TextChangeState changeToFinalNarration;
    SetObjectPositionSTate setFinalNarrationPosition;
    FadeInState showFinalNarration;
    WaitState letFinalNarrationStay;

    FadeUIOutState hideFinalText;
    FadeUIInState showInstruction;
    WaitState letInstructionsStay;
    FadeUIOutState hideInstructions;
    TextChangeState SetFinalInstruction;
    FadeUIInState showFinalInstruction;
    WaitState letFinalInstructionStay;

    FadeOutState hideToNextLevel;
    NextLevelState toGame;

    // Start is called before the first frame update
    void Start()
    {
        audioEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Sound Effects/Speech/Intro");
        audioEventInstance.start();

        //Set states up
        start = new FadeInState(fadePannel, 0.6f, audioEventInstance);
        LetTextStay1 = new WaitState(2);
        moveTextDown = new ObjectTranslateState(narration.gameObject, bottomTarget, 5);
        ShowFirstImages = new FadeUIInState(firstImages, 0.6f);
        LetImagesStay1 = new WaitState(2);
        hideFirstScreen = new FadeOutState(fadePannel, 0.3f);
        hideFirstImages = new FadeUIOutState(firstImages, 0.1f);
        changeToSecondNarration = new TextChangeState(narration, narrationText[0]);
        ResetNarrationPosition = new SetObjectPositionSTate(narration.gameObject, topTarget);


        showSecondNarration = new FadeInState(fadePannel, 0.3f, audioEventInstance);
        letNarrationStay2 = new WaitState(2);
        moveSecondNarrationDown = new ObjectTranslateState(narration.gameObject, bottomTarget, 5);
        showSecondImages = new FadeUIInState(secondImages, 0.3f);
        LetImagesStay2 = new WaitState(2);
        hideSecondScreen = new FadeOutState(fadePannel, 0.3f);
        hideSecondImages = new FadeUIOutState(secondImages, 0.1f);
        changeToThirdNarration = new TextChangeState(narration, narrationText[1]);
        ResetNarrationPosition2 = new SetObjectPositionSTate(narration.gameObject, topTarget);

        showThirdNarration = new FadeInState(fadePannel, 2.0f, audioEventInstance);
        letNarrationStay3 = new WaitState(2);
        moveThirdNarrationDown = new ObjectTranslateState(narration.gameObject, bottomTarget, 5);
        showThirdImages = new FadeUIInState(thirdImages, 0.3f);
        LetImagesStay3 = new WaitState(2);
        hideThirdScreen = new FadeOutState(fadePannel, 0.3f);
        hideThirdImages = new FadeUIOutState(thirdImages, 0.01f);
        changeToFinalNarration = new TextChangeState(narration, narrationText[2]);
        setFinalNarrationPosition = new SetObjectPositionSTate(narration.gameObject, midTarget);
        showFinalNarration = new FadeInState(fadePannel, 2.0f, audioEventInstance);
        letFinalNarrationStay = new WaitState(2);

        hideFinalText = new FadeUIOutState(textAlpha, 2.0f);
        showInstruction = new FadeUIInState(fourthImage, 0.3f);
        letInstructionsStay = new WaitState(1);
        hideInstructions = new FadeUIOutState(fourthImage, 0.3f);
        SetFinalInstruction = new TextChangeState(narration, narrationText[3]);
        showFinalInstruction = new FadeUIInState(textAlpha, 0.3f);
        letFinalInstructionStay = new WaitState(2);

        hideToNextLevel = new FadeOutState(fadePannel, 0.6f);
        toGame = new NextLevelState();

        

        //Connect States
        start.SetNextState(LetTextStay1);
        LetTextStay1.SetNextState(moveTextDown);
        moveTextDown.SetNextState(ShowFirstImages);
        ShowFirstImages.SetNextState(LetImagesStay1);
        LetImagesStay1.SetNextState(hideFirstScreen);
        hideFirstScreen.SetNextState(hideFirstImages);
        hideFirstImages.SetNextState(changeToSecondNarration);
        changeToSecondNarration.SetNextState(ResetNarrationPosition);
        ResetNarrationPosition.SetNextState(showSecondNarration);

        showSecondNarration.SetNextState(letNarrationStay2);
        letNarrationStay2.SetNextState(moveSecondNarrationDown);
        moveSecondNarrationDown.SetNextState(showSecondImages);
        showSecondImages.SetNextState(LetImagesStay2);
        LetImagesStay2.SetNextState(hideSecondScreen);
        hideSecondScreen.SetNextState(hideSecondImages);
        hideSecondImages.SetNextState(changeToThirdNarration);
        changeToThirdNarration.SetNextState(ResetNarrationPosition2);
        ResetNarrationPosition2.SetNextState(showThirdNarration);

        showThirdNarration.SetNextState(letNarrationStay3);
        letNarrationStay3.SetNextState(moveThirdNarrationDown);
        moveThirdNarrationDown.SetNextState(showThirdImages);
        showThirdImages.SetNextState(LetImagesStay3);
        LetImagesStay3.SetNextState(hideThirdScreen);
        hideThirdScreen.SetNextState(hideThirdImages);
        hideThirdImages.SetNextState(changeToFinalNarration);
        changeToFinalNarration.SetNextState(setFinalNarrationPosition);
        setFinalNarrationPosition.SetNextState(showFinalNarration);
        showFinalNarration.SetNextState(letFinalNarrationStay);
        letFinalNarrationStay.SetNextState(hideFinalText);

        hideFinalText.SetNextState(showInstruction);
        showInstruction.SetNextState(letInstructionsStay);
        letInstructionsStay.SetNextState(hideInstructions);
        hideInstructions.SetNextState(SetFinalInstruction);
        SetFinalInstruction.SetNextState(showFinalInstruction);
        showFinalInstruction.SetNextState(letFinalInstructionStay);
        letFinalInstructionStay.SetNextState(hideToNextLevel);

        
        hideToNextLevel.SetNextState(toGame);

        currentState = start;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState = currentState.Run();
        }

        if (Input.anyKey)
        {
            currentState = hideToNextLevel;
        }
    }
}
