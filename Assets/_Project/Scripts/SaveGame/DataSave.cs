using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Khi lưu các model thì cần thê thêm [System.Serializable] 
 * ví dụ
 * [System.Serializable]
 * public class TestModel
 * {
 * 
 * }
 * 
 * Các enum sẽ lưu thành số theo thứ tự các enum 
 * 
 */

/*
 * Các biến khai báo phải là public 
 */

[System.Serializable]
public class DataSave
{
    public bool isMusicOn = true;
    public bool isSfxOn = true;

    public TimeData TimeData = new();
}
