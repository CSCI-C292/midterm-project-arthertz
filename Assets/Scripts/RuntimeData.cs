using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "New RuntimeData")]

public class RuntimeData : ScriptableObject
{

    public Bool gameOverActive;

    public Bool pauseMenuActive;

    public Bool debugMenuActive;

    public Bool settingsMenuActive;

    private RuntimeData ancestor;

    private Enemy enemyTarget;

    public RuntimeData(RuntimeData cloneTarget)
    {
        //construct a RuntimeData by copying any persistent data
        //when the game is exited, these are copied back to the ancestor
        ancestor = cloneTarget;
    }


    private void OnApplicationQuit()
    {
        //when the game is exited, persistent data is copied back to the ancestor

    }

    public void StopApplication()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public Enemy GetEnemyTarget()
    {
        return enemyTarget;
    }

    public void SetEnemyTarget(Enemy newTarget)
    {
        enemyTarget = newTarget;
    }

    public void CloseGameOverMenu()
    {
        pauseMenuActive.member = true;
        gameOverActive.member = false;
    }

    public void ClosePauseMenu ()
    {
        pauseMenuActive.member = false;
    }

    public bool LevelPlaying ()
    {
        //The level is playing if it is not paused and the game is not over
        return (pauseMenuActive.eval() || gameOverActive.eval()) == false;
    }
}