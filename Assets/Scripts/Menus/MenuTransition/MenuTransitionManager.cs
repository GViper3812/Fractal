using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransitionManager : MonoBehaviour
{
    public MenuTransition MT;
    GameObject Player;

    public void Start() {
        Player = GameObject.Find("Character");
    }

    public void TransToInventory() {
        MT.LastScene = SceneManager.GetActiveScene().name;
        if (Player != null) {
            MT.LastPos = Player.transform.position;
        }
        SceneManager.LoadScene("Inventory");
    }

    public void LastScene() {
        if (MT.LastScene != null)
        {
            SceneManager.LoadScene(MT.LastScene);
        } else {
            Debug.LogWarning("Last scene not recorded");
        }
    }
}
