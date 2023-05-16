using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
public class FireBaseLeaderboard : MonoBehaviour
{
    private FirebaseFirestore db;
    private static string name = "";
    private static int score = 0;
    private static int deaths = 0;
    public static FireBaseLeaderboard Instance;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance){
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        db = FirebaseFirestore.DefaultInstance;
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
    public void displayTop(LeaderboardCanvas s){
        string temp = "";
        Query query = db.Collection("Scores").OrderByDescending("Score").Limit(100);
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) => {
            int place = 1;
            foreach(DocumentSnapshot doc in querySnapshotTask.Result.Documents){
                Dictionary<string,object> sc = doc.ToDictionary();
                Debug.Log(place+"| Name: " + sc["Name"] + " | Score: " + sc["Score"]);
                temp+= place+". Score: " +sc["Score"]+  " | Name: " +sc["Name"] +" | Deaths: " +sc["Deaths"]+"\n";
                place++;
                s.setLeaderboardText(temp);
            }
        });
    }
    public IEnumerator checkName(string name,CheckUsername script){
        bool check = true;
        yield return new WaitForSeconds(0.05f);
        Query query = db.Collection("Scores").OrderByDescending("Score");
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) => {
            foreach(DocumentSnapshot doc in querySnapshotTask.Result.Documents){
                Dictionary<string,object> sc = doc.ToDictionary();
                if(name==doc.Id){
                    check = false;
                }
            }
            script.activate(name,check);   
        });
    }
}
