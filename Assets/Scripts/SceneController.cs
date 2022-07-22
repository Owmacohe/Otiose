using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void Load(string name)
    {
        if (name.Equals("Environment") && FindObjectOfType<PlayerStats>().hatObject == null)
        {
            return;
        }
        
        SceneManager.LoadScene(name);
    }
}