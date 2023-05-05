using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
public class FireBaseLeaderboard : MonoBehaviour
{
    private FirebaseApp app;
    private FirebaseFirestore db;
    private string name = "";
    private int score = 0;
    private int deaths = 0;
    public static FireBaseLeaderboard Instance;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance){
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        app = FirebaseApp.Create();
        db = FirebaseFirestore.GetInstance(app);
        displayTop();
    }
    public void assignName(string aaa){
        name = aaa;
        Debug.Log(name);
    }
    public void changeScore(int amt){
        score +=amt;
        DocumentReference docRef = db.Collection("Scores").Document(name);
        Dictionary<string,object> data = new Dictionary<string,object>{
            {"Name",name},
            {"Score",score},
            {"Deaths",deaths}
            };
        docRef.SetAsync(data, SetOptions.MergeAll);
    }
    public void addDeaths(int amt){
        if(amt>=0){
            deaths+=amt;
        }
        DocumentReference docRef = db.Collection("Scores").Document(name);
        Dictionary<string,object> data = new Dictionary<string,object>{
            {"Name",name},
            {"Score",score},
            {"Deaths",deaths}
            };
        docRef.SetAsync(data, SetOptions.MergeAll);
    }
    public void displayTop(){
        Query query = db.Collection("Scores").OrderByDescending("Score").Limit(10);
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) => {
            int place = 1;
            foreach(DocumentSnapshot doc in querySnapshotTask.Result.Documents){
                Dictionary<string,object> sc = doc.ToDictionary();
                Debug.Log(place+"| Name: " + sc["Name"] + " | Score: " + sc["Score"]);
                place++;
            }
        });
    }
    public IEnumerator checkName(string name,CheckUsername script){
        bool check = false;
        yield return new WaitForSeconds(0.05f);
        Query query = db.Collection("Scores").OrderByDescending("Score");
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) => {
            foreach(DocumentSnapshot doc in querySnapshotTask.Result.Documents){
                Dictionary<string,object> sc = doc.ToDictionary();
                if(name==doc.Id){
                    check = true;
                }
            }
            if(!check){
            script.activate(name);    
            }
        });
    }
}
