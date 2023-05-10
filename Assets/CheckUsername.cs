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
    private List<string> profaneWords;
    [SerializeField] private TextAsset badWordFile;
    [SerializeField] private TMP_Text errorMessage;
    // Start is called before the first frame update
    void Start()
    {
        profaneWords = new List<string>();
        string[] lines =badWordFile.text.Split('\n');
        foreach(string line in lines){
            if(!string.IsNullOrEmpty(line)){
             profaneWords.Add(line);   
            }
        }
        button.onClick.AddListener(check);
        StartCoroutine(assign());
    }

    public void check(){
        string name = field.text;
        bool hasBadWord = false;
        foreach(string line in profaneWords){
            if(name.Contains(line)){
                Debug.Log("Word: " + line + " Username: " + name);
            hasBadWord = true;
            }
        }
        if(!string.IsNullOrEmpty(name)&&!hasBadWord){
            StartCoroutine(fblb.checkName(name,this));
        }else if(string.IsNullOrEmpty(name)){
            errorMessage.text = "Please enter in a username to play!";
        }else if(hasBadWord){
            errorMessage.text = "Your username contains profane language. Please try another one!";
        }
    }
    public void activate(string name,bool check){
        if(check){
        fblb.assignName(name);
        nameCanvas.SetActive(false);    
        fblb.changeScore(0);    
        }else{
        errorMessage.text = "This username has already been taken!";
        }
        
    }
    private IEnumerator assign(){
        yield return new WaitForSeconds(0.5f);
        fblb = FireBaseLeaderboard.Instance;
    }
}
