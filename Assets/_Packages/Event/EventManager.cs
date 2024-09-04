using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : BaseMonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [SerializeField] private EventDay[] eventDays;
    [SerializeField] private EventWeek[] eventWeeks;
    [SerializeField] private EventMonth[] eventMonths;

    private TimeData timeData;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEventDays();
        LoadEventWeeks();
        LoadEventMonths();
    }

    private void LoadEventMonths()
    {
        eventMonths = GetComponentsInChildren<EventMonth>();
    }

    private void LoadEventWeeks()
    {
        eventWeeks = GetComponentsInChildren<EventWeek>();
    }

    private void LoadEventDays()
    {
        eventDays = GetComponentsInChildren<EventDay>();
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        TimeRequest.OnTimeRequestSuccess += CheckTime;
        TimeManager.OnApplicationFocusTimeChanged += CheckTime;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        TimeRequest.OnTimeRequestSuccess -= CheckTime;
        TimeManager.OnApplicationFocusTimeChanged -= CheckTime;
    }

    private void CheckTime()
    {
        timeData ??= SaveManager.Instance.DataSave.TimeData;

        int year = TimeManager.Instance.DateTimeOffset.Year;
        CheckNextDay(year);
        CheckNextWeek(year);
        CheckNextMonth(year);
        CheckNextYear(year);
    }

    private bool IsNextYear(int year)
    {
        return year > timeData.Year;
    }

    private void CheckNextDay(int year)
    {
        int dayOfYear = TimeManager.Instance.GetDayOfYear();
        if (IsNextYear(year) || dayOfYear > timeData.Day)
        {
            timeData.Day = dayOfYear;
            OnNextDay();
        }
        else
        {
            CheckEventStatus(eventDays);
        }
    }

    private void CheckNextWeek(int year)
    {
        int weekOfYear = TimeManager.Instance.GetWeekOfYear();
        if (IsNextYear(year) || weekOfYear > timeData.Week)
        {
            timeData.Week = weekOfYear;
            OnNextWeek();
        }
        else
        {
            CheckEventStatus(eventWeeks);
        }
    }

    private void CheckNextMonth(int year)
    {
        int monthOfYear = TimeManager.Instance.GetMonthInYear();
        if (IsNextYear(year) || monthOfYear > timeData.Month)
        {
            timeData.Month = monthOfYear;
            OnNextMonth();
        }
        else
        {
            CheckEventStatus(eventMonths);
        }
    }

    private void CheckNextYear(int year)
    {
        if (IsNextYear(year))
        {
            timeData.Year = year;
            OnNextYear();
        }
    }

    private void OnNextYear()
    {
        Debug.Log($"datdb - OnNextYear");
    }

    private void OnNextMonth()
    {
        Debug.Log($"datdb - OnNextMonth");
        foreach (var item in eventMonths)
        {
            item.CheckEventStatus();
            item.NextEvent();
        }
    }

    private void OnNextWeek()
    {
        Debug.Log($"datdb - OnNextWeek");
        foreach (var item in eventWeeks)
        {
            item.CheckEventStatus();
            item.NextEvent();
        }
    }

    private void OnNextDay()
    {
        Debug.Log($"datdb - OnNextDay");
        foreach (var item in eventDays)
        {
            item.CheckEventStatus();
            item.NextEvent();
        }
    }

    private void CheckEventStatus(BaseEvent[] baseEvents)
    {
        foreach (var item in baseEvents)
        {
            item.CheckEventStatus();
        }
    }
}
