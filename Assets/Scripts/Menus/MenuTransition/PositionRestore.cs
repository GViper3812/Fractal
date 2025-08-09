using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionRestore : MonoBehaviour
{
    public MenuTransition MT;
    Transform PlaPos;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == MT.LastScene && MT != null)
        {
            GameObject Player = GameObject.Find("Character");
            if (Player != null)
            {
                PlaPos = Player.transform;
                PlaPos.position = MT.LastPos;
            }
        }
    }

}
