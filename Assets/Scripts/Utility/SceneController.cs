using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void Load(string name)
    {
        PlayerStats temp = FindObjectOfType<PlayerStats>();
    
        if (name.Equals("Environment") && temp != null && temp.hatObject == null)
        {
            return;
        }
        
        SceneManager.LoadScene(name);
    }
}