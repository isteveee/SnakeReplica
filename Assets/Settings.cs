using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    void Awake () {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }

        SetLangPack(appLangPack);
    }
    
    public string appLangPack; //TODO: Var to get set from config file

    public const string LANG_EN = "EN";
    public const string LANG_RU = "RU";

    public List<string> defLangPack = new List<string>();

    List<string> enLangPack = new List<string> {
        "WELCOME TO\n\nSNAKE REPLICA\n\n\nPRESS <b>[SPACE]</b> TO START",
        "GAME OVER\n\nSCORE: ", //TODO: SCORES
        "PAUSE"
    };
    List<string> ruLangPack = new List<string>{
        "SNAKE REPLICA\n\nДОБРО ПОЖАЛОВАТЬ\n\n\nДЛЯ НАЧАЛА ИГРЫ НАЖМИТЕ\n<b>[ПРОБЕЛ]</b>",
        "ИГРА ОКОНЧЕНА\n\nСЧЕТ: ",
        "ПАУЗА"
    };

    void SetLangPack (string _lang) {
        switch (_lang) {
            case LANG_EN:
                defLangPack = enLangPack;
                appLangPack = _lang;
                break;
            case LANG_RU:
                defLangPack = ruLangPack;
                appLangPack = _lang;
                break;
            default:
                defLangPack = ruLangPack;
                appLangPack = LANG_RU;
                break;
        }        
    }
}

//endgameText.text = "GAME OVER!\n\nSCORE: " + /*TODO VAR HERE*/ "" + "\n\n<b>PRESS [SPACE] TO RESTART</b>" + "\n\n\n\n\n\n\n\n\n\n\n" +
//    "ИГРА ОКОНЧЕНА!\n\nСЧЕТ: " + "" + "\n\n<b>НАЖМИТЕ [SPACE] ДЛЯ НОВОЙ ИГРЫ</b>";