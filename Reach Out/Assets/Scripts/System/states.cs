using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public FadeInState(Image fadePanel, float fadeTime)
    {
        this.fadePanel = fadePanel;
        this.fadeTime = fadeTime;
        this.nextState = null;
    }

    public FadeInState(Image fadePanel, float fadeTime, State nextState)
    {
        this.fadePanel = fadePanel;
        this.fadeTime = fadeTime;
        this.nextState = nextState;
    }

    public override State Start()
    {
        Color fadePanelColor = fadePanel.color;
        fadePanelColor.a = 1;
        fadePanel.color = fadePanelColor;
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
        return nextState;
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

