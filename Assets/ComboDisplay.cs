using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboDisplay : MonoBehaviour
{
    public TMP_Text tm;
    public void setComboText(string txt){
        tm.text = txt;
    }
}
