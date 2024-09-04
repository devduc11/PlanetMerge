using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : BaseMonoBehaviour
{
    private static SaveManager instance;
    public static SaveManager Instance => instance;

    public static readonly string FILE_SAVE = "Save.bin";
    private readonly string SAVE_GAME = "save_game";

    [SerializeField] private DataSave dataSave;
    public DataSave DataSave { get => dataSave; private set => dataSave = value; }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    #region Singleton
    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Clear Save
    protected override void Reset()
    {
        base.Reset();
        ClearSave();
    }

    [ContextMenu("Clear")]
    private void ClearSave()
    {
        DataSave dataSave = new();
        SaveData(dataSave);
        SaveSystem.Clear(FILE_SAVE);
    }
    #endregion

    #region Save
    [ContextMenu("Save Data")]
    private void SaveDataInInspector()
    {
        SaveData();
    }
    #endregion

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
        }
    }

    private void Init()
    {
        SaveSystem.Initialize(FILE_SAVE);
        LoadData();
    }

    private void LoadData()
    {
        string jsonString = SaveSystem.GetString(SAVE_GAME);
        dataSave = JsonUtility.FromJson<DataSave>(jsonString);
        dataSave ??= new DataSave();
    }

    public void SaveData()
    {
        SaveData(dataSave);
    }

    private void SaveData(DataSave dataSave)
    {
        string json = JsonUtility.ToJson(dataSave);
        SaveSystem.SetString(SAVE_GAME, json);
        SaveSystem.SaveToDisk();
    }
}
