using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    public string uid;
    public int score = 0;

    public LeaderboardEntry(string uid, int score) {
        this.uid = uid;
        this.score = score;
    }

    public Dictionary<string, object> ToDictionary() {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["uid"] = uid;
        result["score"] = score;

        return result;
    }
}
