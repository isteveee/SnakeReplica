using UnityEngine;

public class Settings : MonoBehaviour {

    void Awake () {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    public string Lang = "EN";

}
