using System;
using UnityEngine;

public class TimeRequest : MonoBehaviour
{
    private bool internetDisconected = false;
    private bool isRequest;

    private int countRequest;

    public static event Action OnTimeRequestSuccess;
    public static event Action OnTimeRequestFailed;

    public void Request()
    {
        countRequest = 0;
        internetDisconected = Application.internetReachability == NetworkReachability.NotReachable;
        if (!internetDisconected)
        {
            GetTime();
        }
        else
        {
            Invoke(nameof(NoInternet), 1f);
        }
    }

    private void NoInternet()
    {
        OnTimeRequestFailed?.Invoke();
    }

    private void Update()
    {
        if (internetDisconected && Application.internetReachability != NetworkReachability.NotReachable)
        {
            internetDisconected = false;
            if (!isRequest)
            {
                GetTime();
            }
        }
        else if (!internetDisconected && Application.internetReachability == NetworkReachability.NotReachable)
        {
            StopAllCoroutines();
            internetDisconected = true;
            countRequest = 0;
        }
    }

    private void GetTime()
    {
        countRequest++;
        RequestNetWorkTime();
    }

    private void RequestNetWorkTime()
    {
        try
        {
            NtpClient client = new("time.windows.com");
            using (client)
            {
                isRequest = true;
                DateTime dt = client.GetNetworkTime();
                TimeManager.Instance.Init(TimeManager.Instance.GetTotalSeconds(dt));
                Debug.Log($"datdb - {dt.ToLongDateString()} {dt.ToLongTimeString()}");
                OnTimeRequestSuccess?.Invoke();
            }
        }
        catch (Exception)
        {
            if (countRequest > 3)
            {
                OnTimeRequestFailed?.Invoke();
            }
            else
            {
                Invoke(nameof(GetTime), 1);
            }
        }
    }
}