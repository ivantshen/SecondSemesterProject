using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CheckUsername : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_InputField field;
    private FireBaseLeaderboard fblb;
    [SerializeField] private GameObject nameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(check);
        StartCoroutine(assign());
    }

    public void check(){
        string name = field.text;
        if(!string.IsNullOrEmpty(name)){
            fblb.assignName(name);
            nameCanvas.SetActive(false);
        }
    }
    private IEnumerator assign(){
        yield return new WaitForSeconds(0.5f);
        fblb = FireBaseLeaderboard.Instance;
    }
}
