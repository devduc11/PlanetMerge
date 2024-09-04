using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public enum TimeType
{
    UTC, LOCAL
}

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;
    [SerializeField] private bool IsUseLocalTime;
    [SerializeField, GetComponent()] private TimeRequest timeRequest;

    private DateTimeOffset dateTimeOffset;
    private DateTimeOffset startDateTimeOffset;

    private double realTimeSinceStartup = 0;

    private bool isInitialed;

    public DateTime TimeStart = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static TimeManager Instance => instance;

    public static event Action OnApplicationFocusTimeChanged;

    public bool IsInitialed { get => isInitialed; private set => isInitialed = value; }
    public DateTimeOffset DateTimeOffset { get => dateTimeOffset; set => dateTimeOffset = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Reset()
    {
        timeRequest = GetComponent<TimeRequest>();
    }

    public void Request()
    {
        timeRequest.Request();
    }

    private void Update()
    {
        if (dateTimeOffset != null)
        {
            dateTimeOffset = startDateTimeOffset.AddSeconds(Time.realtimeSinceStartupAsDouble - realTimeSinceStartup);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus && isInitialed)
        {
            StartCoroutine(ApplicationFocusCoroutine());
        }
    }

    private IEnumerator ApplicationFocusCoroutine()
    {
        yield return new WaitForEndOfFrame();
        OnApplicationFocusTimeChanged?.Invoke();
    }

    public void Init(double seconds)
    {
        isInitialed = true;
        if (IsUseLocalTime)
        {
            seconds = GetTotalSeconds(DateTime.UtcNow);
        }
        realTimeSinceStartup = Time.realtimeSinceStartupAsDouble;
        startDateTimeOffset = dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)seconds);
    }

    public double GetTotalSeconds(DateTime dateTime)
    {
        return (dateTime - TimeStart).TotalSeconds;
    }

    public DateTime GetDateTime(TimeType type = TimeType.UTC)
    {
        return type == TimeType.UTC ? dateTimeOffset.DateTime : dateTimeOffset.LocalDateTime;
    }

    public double GetCurrentTime(TimeType type = TimeType.UTC)
    {
        return (GetDateTime(type) - TimeStart).TotalSeconds;
    }

    public int GetDayOfWeek(TimeType type = TimeType.UTC)
    {
        return (int)GetDateTime(type).DayOfWeek;
    }

    public int GetDayOfMonth(TimeType type = TimeType.UTC)
    {
        return GetDateTime(type).Day;
    }

    public int GetDayOfYear(TimeType type = TimeType.UTC)
    {
        return GetDateTime(type).DayOfYear;
    }

    public int GetWeekOfYear(TimeType type = TimeType.UTC, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
    {
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        Calendar calendar = cultureInfo.Calendar;
        int weekOfYear = calendar.GetWeekOfYear(GetDateTime(type), cultureInfo.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
        return weekOfYear;
    }

    public int GetMonthInYear(TimeType type = TimeType.UTC)
    {
        return GetDateTime(type).Month;
    }
}
