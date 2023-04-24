using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public struct FireBaseLeaderboard : MonoBehaviour
{
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    DocumentReference docRef;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void addScore(string name, int score){
        docRef = db.Collection("Scores").Document(name);
        docRef.SetAsync(score, SetOptions.MergeAll);
    }
}
