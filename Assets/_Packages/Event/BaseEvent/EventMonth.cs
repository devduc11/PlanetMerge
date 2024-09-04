using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class EventMonth : BaseEvent
{
    private int daysInMonth;

    protected override void Awake()
    {
        base.Awake();

        int year = TimeManager.Instance.DateTimeOffset.Year;
        int month = TimeManager.Instance.DateTimeOffset.Month;
        daysInMonth = DateTime.DaysInMonth(year, month);
    }

    protected override TimeSpan BreakTime()
    {
        return TimeSpan.FromDays(0);
    }

    protected override TimeSpan EventDuration()
    {
        return TimeSpan.FromDays(daysInMonth);
    }
}