using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState 
{
  
  #region bool isDay
  private static bool _isDay = true;
  public static bool isDay { 
    get => _isDay; 
    set
    {
      if(_isDay != value)
      {
        _isDay = value;
        Notify(nameof(isDay));
      }
    }
  }
  #endregion

  #region bool isFpv
  private static bool _isFpv = true;
  public static bool isFpv
  {
    get => _isFpv;
    set
    {
      if (_isFpv != value)
      {
        _isFpv = value;
        Notify(nameof(isFpv));
      }
    }
  }
  #endregion
  
  
  #region bool isKey1InTime
  private static bool _isKey1InTime = true;
  public static bool isKey1InTime
  {
    get => _isKey1InTime;
    set
    {
      if (_isKey1InTime != value)
      {
        _isKey1InTime = value;
        Notify(nameof(isKey1InTime));
      }
    }
  }
  #endregion
  
  #region bool isKey2InTime
  private static bool _isKey2InTime = true;
  public static bool isKey2InTime
  {
    get => _isKey2InTime;
    set
    {
      if (_isKey2InTime != value)
      {
        _isKey2InTime = value;
        Notify(nameof(isKey2InTime));
      }
    }
  }
  #endregion
  
  #region bool isKey3InTime
  private static bool _isKey3InTime = true;
  public static bool isKey3InTime
  {
    get => _isKey3InTime;
    set
    {
      if (_isKey3InTime != value)
      {
        _isKey3InTime = value;
        Notify(nameof(isKey3InTime));
      }
    }
  }
  #endregion
  
  #region bool isKey4InTime
  private static bool _isKey4InTime = true;
  public static bool isKey4InTime
  {
    get => _isKey4InTime;
    set
    {
      if (isKey4InTime != value)
      {
        _isKey4InTime = value;
        Notify(nameof(isKey4InTime));
      }
    }
  }
  #endregion
  
  #region bool isKey1Collected
  private static bool _isKey1Collected = false;
  public static bool isKey1Collected
  {
    get => _isKey1Collected;
    set
    {
      if (_isKey1Collected != value)
      {
        _isKey1Collected = value;
        Notify(nameof(isKey1Collected));
      }
    }
  }
  #endregion
  
  
  #region bool isKey2Collected
  private static bool _isKey2Collected = true;
  public static bool isKey2Collected
  {
    get => _isKey2Collected;
    set
    {
      if (_isKey2Collected != value)
      {
        _isKey2Collected = value;
        Notify(nameof(isKey2Collected));
      }
    }
  }
  #endregion
  
    
  #region bool isKey3Collected
  private static bool _isKey3Collected = true;
  public static bool isKey3Collected
  {
    get => _isKey3Collected;
    set
    {
      if (_isKey3Collected != value)
      {
        _isKey3Collected = value;
        Notify(nameof(isKey3Collected));
      }
    }
  }
  #endregion
  
  
  #region bool isKey4Collected
  private static bool _isKey4Collected = true;
  public static bool isKey4Collected
  {
    get => _isKey4Collected;
    set
    {
      if (_isKey4Collected != value)
      {
        _isKey4Collected = value;
        Notify(nameof(isKey4Collected));
      }
    }
  }
  #endregion


  #region Change Notifier
  private static List<Action<string>> listeners = new List<Action<string>>();
  public static void AddListener(Action<string> listener)
  {
    listeners.Add(listener);
  }

  public static void RemoveListener(Action<string> listener)
  {
    listeners.Remove(listener);
  }

  private static void Notify(string fieldName)
  {
    foreach(Action<string> listener in listeners)
    {
        listener.Invoke(fieldName);
    }
  }
  #endregion

  public static void SetProperty(string name, object value)
  {
    var prop = typeof(GameState).GetProperty(name,System.Reflection.BindingFlags.Static  | System.Reflection.BindingFlags.Public);
    if (prop == null)
    {
      Debug.LogError($"Error prop setting: name not found '{name}', (value='{value}');");
    }
    else
    {
      prop.SetValue(null, value);
      
    }

  }
}

