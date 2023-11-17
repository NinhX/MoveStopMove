using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public const int With = 1080;
    public const int Height = 1920;

    [SerializeField] private Joystick joystickPrefab;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private AbstractUI[] listUIPrefab;
    [SerializeField] private Transform parentTranform;

    public float CanvasWidth => rectTransform.rect.width;
    public float CanvasHeight => rectTransform.rect.height;

    private Dictionary<Type, AbstractUI> dictUI = new();
    private Joystick joystick;

    private void Awake()
    {
        joystick = Instantiate(joystickPrefab, transform);
        DisableJoystick();
    }

    public Joystick GetJoystick()
    {
        return joystick;
    }

    public void DisableJoystick()
    {
        joystick.gameObject.SetActive(false);
    }

    public void EnableJoystick()
    {
        joystick.gameObject.SetActive(true);
    }

    public void AddPanelEnemy(Transform tf)
    {
        if (TryGetUI(out EnemyPanel enemyPanel))
        {
            enemyPanel.AddTranform(tf);
        }
    }

    public T LoadUI<T>() where T : AbstractUI
    {
        if (TryGetUI(out T panel))
        {
            return panel;
        }
        return null;
    }

    public void ReloadUI<T>() where T : AbstractUI
    {
        if (TryGetUI(out T ui))
        {
            ui.ReloadUI();
        }
    }

    public void OpenUI<T>() where T : AbstractUI
    {
        if (TryGetUI(out T ui))
        {
            ui.Open();
        }
    }

    public void CloseUI<T>() where T : AbstractUI
    {
        if (TryGetUI(out T ui))
        {
            ui.Close();
        }
    }

    public void CloseAllPanel()
    {
        foreach (var ui in dictUI.Values)
        {
            ui.Close();
        }
    }

    private bool TryGetUI<T>(out T ui) where T : AbstractUI
    {
        Type type = typeof(T);
        bool result = dictUI.ContainsKey(type);

        if (result)
        {
            ui = dictUI[type] as T;
        }
        else if (TryCreateUI(out ui))
        {
            dictUI.Add(type, ui);
            result = true;
        }

        return result;
    }

    private bool TryCreateUI<T>(out T ui) where T : AbstractUI
    {
        bool result = false;
        ui = null;

        for (int i = 0; i < listUIPrefab.Length; i++)
        {
            AbstractUI uiPrefab = listUIPrefab[i];
            if (uiPrefab is T)
            {
                ui = Instantiate(uiPrefab, parentTranform) as T;
                ui.gameObject.SetActive(false);
                ui.OnInit();
                result = true;
                break;
            }
        }
        return result;
    }
}
