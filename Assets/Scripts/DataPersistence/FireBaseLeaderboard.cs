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
    private DocumentReference docRef;
    // Start is called before the first frame update
    void Start()
    {
        app = FirebaseApp.Create();
        db = FirebaseFirestore.GetInstance(app);
        addScore("test1",15);
    }
    public void addScore(string name, int score){
        docRef = db.Collection("Scores").Document("userscores");
        Dictionary<string,int> data = new Dictionary<string,int>{
            {name,score}
            };
        docRef.SetAsync(data, SetOptions.MergeAll);
    }
}
