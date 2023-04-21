using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class FireBaseLeaderboard : MonoBehaviour
{
    public static FireBaseLeaderboard Instance;
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBreference;
    public Credential cd;
    // Start is called before the first frame update
    void Start()
    {
    if(Instance){
        Destroy(this);
    }
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;   
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        Instance = this;
        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
