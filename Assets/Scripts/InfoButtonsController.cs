using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButtonsController : MonoBehaviour
{
    [SerializeField] Toggle Toggle;
    public void CloseInfo()
    {
        ScenesController.ShowHomeLevel();
    }

    public void DontShow()
    {
        SaveLoadSystem _saveLoadSystem = GameObject.FindWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();
        _saveLoadSystem.SaveDontShowInfo(Toggle.isOn);
    }
}
