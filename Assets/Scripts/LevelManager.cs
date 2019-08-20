using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel (int lvl)//send level as a parameter
    {
        SceneManager.LoadScene(lvl);
        WaveManager._actualLevel = lvl;
    }
}
