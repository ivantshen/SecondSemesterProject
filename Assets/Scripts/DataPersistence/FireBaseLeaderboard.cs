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
    private int uid;
    // Start is called before the first frame update
    void Start()
    {
        app = FirebaseApp.Create();
        db = FirebaseFirestore.GetInstance(app);
        addScore("test1",15);
    }
    public void addScore(string name, int score){
        DocumentReference docRef = db.Collection("Scores").Document(name);
        Dictionary<string,object> data = new Dictionary<string,object>{
            {"Name",name},
            {"Score",score}
            };
        docRef.SetAsync(data, SetOptions.MergeAll);
    }
    public void displayTop(){
        DocumentReference docRef = db.Collection("Scores").Document("userscores");
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            DocumentSnapshot snap = task.Result;
            if(snap.Exists){
                Dictionary<string,object> scores = snap.ToDictionary();
            }
        });
    }
}
