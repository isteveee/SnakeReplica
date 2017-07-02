using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour {
    	
    bool gameOn = false; //is game started?
    bool canSwitchDir = true;
    bool pause = false;

    [SerializeField]float updRate = 0.5f; //CustomUpdate Rate
    float rw = 20, lw = -20; //walls x coords
    float uw = 20, dw = -20; //walls y coords
    float spawnStamp = 0f; //last food spawn time
    [SerializeField]float spawnRate = 5f; //how freq-ly spawn food
    [SerializeField]int maxFood = 2;

    Vector2 dir = new Vector2(1f, 0f); //MoveSnake direction
    public List<Vector2> foodLoc = new List<Vector2>(); //list of existing food locations
    public List<GameObject> foodList = new List<GameObject>(); //list of existing food obj
    public List<Vector2> lastPos = new List<Vector2>(); //for tail 
    public List<GameObject> tailList = new List<GameObject>(); //existing tail;

    [SerializeField] GameObject food;
    [SerializeField] GameObject tail;

    [SerializeField] Text welcomeText;
    [SerializeField] Text endgameText;
    [SerializeField] Text pauseText;
    [SerializeField] GameObject overlay;

    [SerializeField] Settings settings;

    void Start () {
        overlay.SetActive(true);//TODO: Set Lang Screen
        //welcomeText.enabled = true;

        lastPos.Insert(0, transform.position);
        lastPos.Insert(1, transform.position);
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gameOn) {                
                gameOn = !gameOn;
                overlay.SetActive(false);

                if (welcomeText.enabled) {
                    welcomeText.enabled = false;
                    Spawn(food);
                    StartCoroutine(CustomUpdate());
                }
                else if (endgameText.enabled) {
                    SceneManager.LoadScene("Main");
                }
            }
            else if (gameOn) {
                if (!pause) {
                    Time.timeScale = 0;
                    overlay.SetActive(true);
                    pauseText.enabled = true;
                }
                else if (pause) {
                    Time.timeScale = 1;
                    overlay.SetActive(false);
                    pauseText.enabled = false;
                }
                pause = !pause;
            }
        }
        
        if (gameOn) {

            if (canSwitchDir) {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                    if (dir != new Vector2(0f, -1f) && dir != new Vector2(0f, 1f)) {
                        dir = new Vector2(0f, 1f);
                        canSwitchDir = false;
                    }                      
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                    if (dir != new Vector2(0f, 1f) && dir != new Vector2(0f, -1f)) {
                        dir = new Vector2(0f, -1f);
                        canSwitchDir = false;
                    }  
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                    if (dir != new Vector2(1f, 0f) && dir != new Vector2(-1f, 0f)) {
                        dir = new Vector2(-1f, 0f);
                        canSwitchDir = false;
                    }
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                    if (dir != new Vector2(-1f, 0f) && dir != new Vector2(1f, 0f)) {
                        dir = new Vector2(1f, 0f);
                        canSwitchDir = false;
                    }
                }
            }           

            if (Time.time >= spawnStamp + spawnRate && foodList.Count < maxFood) {
                Spawn(food);
            }

            if (tailList.Count == 10 || tailList.Count == 20 || tailList.Count == 30 || tailList.Count == 40 || tailList.Count == 50 ||
                 tailList.Count == 60 || tailList.Count == 70 || tailList.Count == 80 || tailList.Count == 90 || tailList.Count == 100) {
                maxFood = (int)Mathf.Floor((tailList.Count / 10) * 3);
            }
        }
	}

    void GameOver () {
        gameOn = !gameOn; //stop game process

        overlay.SetActive(true);
        endgameText.enabled = true;
        endgameText.text = "GAME OVER!\n\nSCORE: " + /*TODO VAR HERE*/ "" + "\n\n<b>PRESS [SPACE] TO RESTART</b>" + "\n\n\n\n\n\n\n\n\n\n\n" +
            "ИГРА ОКОНЧЕНА!\n\nСЧЕТ: " + "" + "\n\n<b>НАЖМИТЕ [SPACE] ДЛЯ НОВОЙ ИГРЫ</b>";
        //TODO: vars for Lang selection
    }

    void Spawn (GameObject obj) {
        float _x = Mathf.Floor(Random.Range(lw, rw+1));
        float _y = Mathf.Floor(Random.Range(dw, uw+1));
        bool sameLocation = false;

        for (int j = 0; j < foodLoc.Count; j++) {
            if (foodLoc[j].x == _x && foodLoc[j].y == _y) {
                sameLocation = true;
            }
        }

        for (int i = 1; i < lastPos.Count; i++) {
            if (lastPos[i].x == _x && lastPos[i].y == _y) {
                sameLocation = true;
            }
        }

        if (sameLocation) {            
            Spawn(obj);
            return;
        }
        
        var z = Instantiate(obj, new Vector2(_x, _y), new Quaternion());
        foodList.Add(z);
        foodLoc.Add(new Vector2(_x, _y));
        spawnStamp = Time.time;
    }

    void MoveSnake () {
        string wall = CheckWall();
        if (wall != "") {
            if (wall == "rw" && dir.x == 1 || wall == "ruw" && dir.x == 1 || wall == "rdw" && dir.x == 1) {
                transform.position = new Vector2(lw-1, transform.position.y);
            }
            else if (wall == "lw" && dir.x == -1 || wall == "luw" && dir.x == -1 || wall == "ldw" && dir.x == -1) {
                transform.position = new Vector2(rw+1, transform.position.y);
            }
            else if (wall == "uw" && dir.y == 1 || wall == "ruw" && dir.y == 1 || wall == "luw" && dir.y == 1) {
                transform.position = new Vector2(transform.position.x, dw-1);
            }
            else if (wall == "dw" && dir.y == -1 || wall == "rdw" && dir.y == -1 || wall == "ldw" && dir.y == -1) {
                transform.position = new Vector2(transform.position.x, uw+1);
            }
        }

        if (lastPos.Count > 1) { //move positions of tail
            for (int i = 0; i <= lastPos.Count - 2; i++) {
                lastPos[i] = lastPos[i + 1];                               
            }
        }

        transform.Translate(dir); //move head
        lastPos[lastPos.Count - 1] = transform.position;

        for (int i = 0; i < lastPos.Count - 2; i++) { //move tail
            tailList[i].transform.position = lastPos[i + 1];
        }

        canSwitchDir = true;
    }

    void EnlargeSnake () {
        var z = Instantiate(tail, lastPos[0], new Quaternion());
        tailList.Add(z);
        lastPos.Insert(0, new Vector2());
    }

    void Eat (GameObject obj, int ind) {
        Destroy(obj);
        foodLoc.RemoveAt(ind);
        foodList.RemoveAt(ind);
        EnlargeSnake();

        updRate -= (updRate * 2) / 100;
        spawnRate -= (spawnRate * 2) / 1000;
        //if (updRate > 0.02f)
        //    updRate -= 0.01f;
        //else
        //    updRate = 0.02f;

    }
    
    void CheckFood () {
        if (foodLoc.Count != 0) {
            for (int i = 0; i < foodLoc.Count; i++) {
                if (transform.position.x == foodLoc[i].x && transform.position.y == foodLoc[i].y) {
                    Eat(foodList[i],i);
                }
            }
        }
    }

    void CheckTail () {
        if (tailList.Count != 0) {
            for (int i = 0; i < tailList.Count; i++) {
                if (transform.position.x == tailList[i].transform.position.x && 
                     transform.position.y == tailList[i].transform.position.y) {
                    GameOver();
                }
            }
        }
    }
    
    string CheckWall () {        
        float tx = transform.position.x;
        float ty = transform.position.y;        

        if (tx == rw) {
            if (ty == uw)
                return "ruw";
            else if (ty == dw)
                return "rdw";
            else
                return "rw";
        }
        else if (tx == lw) {
            if (ty == uw)
                return "luw";
            else if (ty == dw)
                return "ldw";
            else
                return "lw";
        }
        else if (ty == uw) {
            if (tx == rw)
                return "ruw";
            else if (tx == lw)
                return "luw";
            else
                return "uw";
        }
        else if (ty == dw) {
            if (tx == rw)
                return "rdw";
            else if (tx == lw)
                return "ldw";
            else
                return "dw";
        }
        
        return "";
    }    

    IEnumerator CustomUpdate () {
        if (gameOn) {
            MoveSnake();
            CheckFood();
            CheckTail();
        
            yield return new WaitForSeconds(updRate);
            StartCoroutine(CustomUpdate());
        }
    }
}
 