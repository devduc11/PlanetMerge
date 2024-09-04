using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEvent : BaseMonoBehaviour
{
    private float remainingTime;
    private float nextEventTime;
    private bool isInEvent;

    protected float RemainingTime { get => remainingTime; private set => remainingTime = value; }
    protected float NextEventTime { get => nextEventTime; private set => nextEventTime = value; }
    protected bool IsInEvent { get => isInEvent; private set => isInEvent = value; }

    public virtual void CheckEventStatus()
    {
        DateTime startDateTime = TimeManager.Instance.TimeStart;
        DateTime currentDateTime = TimeManager.Instance.DateTimeOffset.LocalDateTime;
        TimeSpan timeElapsed = currentDateTime - startDateTime;

        isInEvent = (timeElapsed.TotalHours % (EventDuration() + BreakTime()).TotalHours) < EventDuration().TotalHours;

        if (isInEvent)
        {
            DateTime eventStartTime = startDateTime + TimeSpan.FromHours(Math.Floor(timeElapsed.TotalHours / (EventDuration() + BreakTime()).TotalHours) * (EventDuration() + BreakTime()).TotalHours);
            DateTime eventEndTime = eventStartTime + EventDuration();
            remainingTime = (float)(eventEndTime - currentDateTime).TotalSeconds;
            StopAllCoroutines();
            StartCoroutine(CountDownRemainingTime());
        }
        else
        {
            nextEventTime = (float)((EventDuration() + BreakTime()).TotalSeconds - (timeElapsed.TotalSeconds % (EventDuration() + BreakTime()).TotalSeconds));
            StopAllCoroutines();
            StartCoroutine(CountDownTimeToNextEvent());
        }
    }

    private IEnumerator CountDownTimeToNextEvent()
    {
        while (nextEventTime > 0)
        {
            nextEventTime -= Time.deltaTime;
            yield return null;
        }
        CheckEventStatus();
    }

    protected IEnumerator CountDownRemainingTime()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        CheckEventStatus();
    }

    protected abstract TimeSpan EventDuration();

    protected abstract TimeSpan BreakTime();

    public abstract void NextEvent();
}
