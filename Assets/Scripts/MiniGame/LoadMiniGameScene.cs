using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGame
{
    public class LoadMiniGameScene : MonoBehaviour
    {
        const string MINI_GAME_SCENE = "MiniGame";

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if(!SceneManager.GetSceneByName(MINI_GAME_SCENE).IsValid())
            {
                SceneManager.LoadScene(MINI_GAME_SCENE, LoadSceneMode.Additive);
            }
        }
    }
}
