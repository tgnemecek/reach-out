using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public interface State
{
    State Run();
}

public abstract class LongState: State
{
    //flags 0- start; 1- during 2- End 
    int state = 0;

    public abstract State Start();

    public abstract State During();

    public abstract State Finish();

    public State Run()
    {
        switch (state)
        {
            case 0:
                return Start();
            case 1:
                return During();
            case 2:
                return Finish();
            default:
                return null;
        }
    }

    protected void NextSubState()
    {
        state++;
        if (state > 2)
        {
            state = 0;
        }
    }
}

//---- FadeIn State:

public class FadeInState: LongState
{
    private State nextState;
    private Image fadePanel;
    private float fadeTime;
    private float counter = 0f;
    // AUDIO VARIABLES
    private FMOD.Studio.EventInstance audioEventInstance;
    private GameObject audioIntroLines = GameObject.Find("AUDIO_IntroLines");

    public FadeInState(Image fadePanel, float fadeTime, FMOD.Studio.EventInstance audioEventInstance)
    {
        this.fadePanel = fadePanel;
        this.fadeTime = fadeTime;
        this.audioEventInstance = audioEventInstance;
        this.nextState = null;
    }

    public FadeInState(Image fadePanel, float fadeTime, FMOD.Studio.EventInstance audioEventInstance, State nextState)
    {
        this.fadePanel = fadePanel;
        this.fadeTime = fadeTime;
        this.audioEventInstance = audioEventInstance;
        this.nextState = nextState;
    }

    public override State Start()
    {
        Color fadePanelColor = fadePanel.color;
        fadePanelColor.a = 1;
        fadePanel.color = fadePanelColor;
        audioEventInstance.triggerCue();

        NextSubState();
        return this;
    }

    public override State During()
    {
        counter = counter + Time.deltaTime;
        Color fadePanelColor = fadePanel.color;
        fadePanelColor.a = 1 - counter/fadeTime;
        fadePanel.color = fadePanelColor;
        if (counter >= fadeTime)
        {
            NextSubState();
        }
        return this;
    }

    public override State Finish()
    {
        Color fadePanelColor = fadePanel.color;
        fadePanelColor.a = 0;
        fadePanel.color = fadePanelColor;
        NextSubState();
        return this.nextState;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
}

//---- FadeOut State

public class FadeOutState: LongState
{
    private State nextState;
    private Image fadePanel;
    private float fadeTime;
    private float counter = 0f;

    public FadeOutState(Image fadePanel, float fadeTime)
    {
        this.fadePanel = fadePanel;
        this.fadeTime = fadeTime;
        this.nextState = null;
    }

    public FadeOutState(Image fadePanel, float fadeTime, State nextState)
    {
        this.fadePanel = fadePanel;
        this.fadeTime = fadeTime;
        this.nextState = nextState;
    }

    public override State Start()
    {
        Color fadePanelColor = fadePanel.color;
        fadePanelColor.a = 0;
        fadePanel.color = fadePanelColor;
        NextSubState();
        return this;
    }

    public override State During()
    {
        counter = counter + Time.deltaTime;
        Color fadePanelColor = fadePanel.color;
        fadePanelColor.a = counter / fadeTime;
        fadePanel.color = fadePanelColor;
        if (counter >= fadeTime)
        {
            NextSubState();
        }
        return this;
    }

    public override State Finish()
    {
        Color fadePanelColor = fadePanel.color;
        fadePanelColor.a = 1;
        fadePanel.color = fadePanelColor;
        NextSubState();
        return nextState;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
}

//---- MoveObject State

public class ObjectTranslateState: LongState
{
    private GameObject toTranslate;
    private Transform target;
    private State nextState;
    private float translationSpeed;
    private float count = 0f;

    public ObjectTranslateState(GameObject toTranslate, Transform target, float translationSpeed)
    {
        this.toTranslate = toTranslate;
        this.target = target;
        this.translationSpeed = translationSpeed;
        this.nextState = null;
    }

    public ObjectTranslateState(GameObject toTranslate, Transform target, float translationSpeed, State nextState)
    {
        this.toTranslate = toTranslate;
        this.target = target;
        this.translationSpeed = translationSpeed;
        this.nextState = nextState;
    }

    public override State During()
    {
        count += Time.deltaTime;
        //toTranslate.transform.position = Vector3.MoveTowards(toTranslate.transform.position, target.position, translationSpeed * Time.deltaTime);
        toTranslate.transform.position = Vector3.Lerp(toTranslate.transform.position, target.position, count / translationSpeed);
        if (Vector3.Distance(toTranslate.transform.position, target.position) < 0.1)
        {
            NextSubState();
            return this;
        }
        return this;
    }

    public override State Finish()
    {
        return nextState;
    }

    public override State Start()
    {
        NextSubState();
        return this;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
}

//---- Set Object Position

public class SetObjectPositionSTate: State
{
    GameObject toPosition;
    Transform target;
    State nextState;

    public SetObjectPositionSTate(GameObject toPosition, Transform target)
    {
        this.toPosition = toPosition;
        this.target = target;
        this.nextState = null;
    }

    public SetObjectPositionSTate(GameObject toPosition, Transform target, State nextState)
    {
        this.toPosition = toPosition;
        this.target = target;
        this.nextState = nextState;
    }

    public State Run()
    {
        toPosition.transform.position = target.position;
        return nextState;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
}

//---- Waiting State

public class WaitState: LongState
{
    private float waitTime;
    private float count = 0f;
    private State nextState;

    public WaitState(float waitTime)
    {
        this.waitTime = waitTime;
        this.nextState = null;
    }

    public WaitState(float waitTime, State nextState)
    {
        this.waitTime = waitTime;
        this.nextState = null;
    }

    public override State During()
    {
        count = count + Time.deltaTime;
        if (count >= waitTime)
        {
            NextSubState();
        }
        return this;
    }

    public override State Finish()
    {
        Debug.Log("wait went to the next state");
        count = 0;
        return nextState;
    }

    public override State Start()
    {
        count = 0;
        NextSubState();
        return this;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
}

//---- Fade UI in

public class FadeUIInState: LongState
{
    private CanvasGroup toTransition;
    private float transitionTime;
    private float count = 0;
    private State nextState;

    public FadeUIInState(CanvasGroup toTransition, float transitionTime)
    {
        this.toTransition = toTransition;
        this.transitionTime = transitionTime;
        this.nextState = null;
    }

    public FadeUIInState(CanvasGroup toTransition, float transitionTime, State nextState)
    {
        this.toTransition = toTransition;
        this.transitionTime = transitionTime;
        this.nextState = nextState;
    }

    public override State During()
    {
        count = count + Time.deltaTime;
        toTransition.alpha = count/transitionTime;
        if (count >= transitionTime)
        {
            NextSubState();
        }
        return this;
    }

    public override State Finish()
    {
        count = 0;
        toTransition.alpha = 1;
        NextSubState();
        return nextState;
    }

    public override State Start()
    {
        count =0;
        toTransition.alpha = 0;
        NextSubState();
        return this;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
}

//---- Fade UI out

public class FadeUIOutState : LongState
{
    private CanvasGroup toTransition;
    private float transitionTime;
    private float count = 0;
    private State nextState;

    public FadeUIOutState(CanvasGroup toTransition, float transitionTime)
    {
        this.toTransition = toTransition;
        this.transitionTime = transitionTime;
        this.nextState = null;
    }

    public FadeUIOutState(CanvasGroup toTransition, float transitionTime, State nextState)
    {
        this.toTransition = toTransition;
        this.transitionTime = transitionTime;
        this.nextState = nextState;
    }

    public override State During()
    {
        count = count + Time.deltaTime;
        toTransition.alpha = 1 - count / transitionTime;
        if (count >= transitionTime)
        {
            NextSubState();
        }
        return this;
    }

    public override State Finish()
    {
        count = 0;
        toTransition.alpha = 0;
        NextSubState();
        return nextState;
    }

    public override State Start()
    {
        count = 0;
        toTransition.alpha = 1;
        NextSubState();
        return this;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
}

//---- Change Text content

public class TextChangeState: State
{
    TextMeshProUGUI toChange;
    string newContent;
    State nextState;

    public TextChangeState(TextMeshProUGUI toChange, string newContent)
    {
        this.toChange = toChange;
        this.newContent = newContent;
        this.nextState = null;
    }

    public TextChangeState(TextMeshProUGUI toChange, string newContent, State nextState)
    {
        this.toChange = toChange;
        this.newContent = newContent;
        this.nextState = nextState;
    }

    public State Run()
    {
        toChange.text = newContent;
        return nextState;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;

    }
}

//---- To the actual Level state

public class NextLevelState : State
{
    public State Run()
    {
        SceneManager.LoadScene(SceneManager.sceneCount + 1);
        return null;
    }
}