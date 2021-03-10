using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButtonsController : MonoBehaviour
{
    [SerializeField] Toggle Toggle;
    public void CloseInfo()
    {
        Debug.Log("click");
        ScenesController.ShowHomeLevel();
    }

    public void DontShow()
    {
        string show = Toggle.isOn ? "no" : "yes";
        PlayerPrefs.SetString("showInfo", show);
    }
}
