using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FireBaseLeaderboard : MonoBehaviour
{
    private FirebaseFirestore db;
    private DocumentReference docRef;
    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        addScore("test",10);
    }
    public void addScore(string name, int score){
        docRef = db.Collection("Scores").Document(name);
        Dictionary<string,int> data = new Dictionary<string,int>{
            {"Score",score}
            };
        docRef.SetAsync(data, SetOptions.MergeAll);
    }
}
