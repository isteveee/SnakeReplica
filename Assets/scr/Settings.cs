using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    
    public string appLangPack;

    public const string LANG_EN = "EN";
    public const string LANG_RU = "RU";

    public List<string> defLangPack = new List<string>();


    List<string> enLangPack = new List<string> {
        "WELCOME TO\n\nSNAKE REPLICA\n\n\nPRESS <b>[SPACE]</b> TO START\n\n"+
            "<b>[W][A][S][D]</b> / <b>[↑][←][↓][→]</b> - TURN\n\n<b>[ALT]</b> + <b>[F4]</b> - QUIT\n\n<b>[L]</b> - SWITCH LANGUAGE",
        "GAME OVER\n\nSCORE: ",
        "PAUSE"
    };

    List<string> ruLangPack = new List<string>{
        "SNAKE REPLICA\n\nДОБРО ПОЖАЛОВАТЬ\n\n\nДЛЯ НАЧАЛА ИГРЫ НАЖМИТЕ\n<b>[ПРОБЕЛ]</b>\n\n"+
            "<b>[W][A][S][D]</b> / <b>[↑][←][↓][→]</b> - ПОВОРОТ\n\n<b>[ALT]</b> + <b>[F4]</b> - ВЫХОД\n\n<b>[L]</b> - СМЕНИТЬ ЯЗЫК",
        "ИГРА ОКОНЧЕНА\n\nСЧЕТ: ",
        "ПАУЗА"
    };

    void Awake () {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }
    
    public void SetLangPack (string _lang) {
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
 