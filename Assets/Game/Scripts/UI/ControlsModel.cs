using System;
using UnityEngine;

public sealed class ControlsModel
{
    public Action<bool, int> OnSaved;

    private const string _versionKey = "VERSION_KEY";

    public void SaveVersion(int currentVersion) 
        => PlayerPrefs.SetInt(_versionKey, currentVersion);

    public int LoadVersion() 
        => PlayerPrefs.GetInt(_versionKey);
}
