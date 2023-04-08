using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class KeybindTextOnStart : MonoBehaviour
{
    [SerializeField] private TMP_Text t;
    [SerializeField] private InputActionReference act;
    [SerializeField] private string firstText;
    [SerializeField] private string secondText;
    // Start is called before the first frame update
    void Start()
    {
        string key =InputControlPath.ToHumanReadableString(act.action.bindings[0].effectivePath,InputControlPath.HumanReadableStringOptions.OmitDevice);
        t.text = firstText + " " + key + " " + secondText;
    }

}
