using UnityEngine;

namespace Singleton
{
  public class Singleton<T> : MonoBehaviour where T : Singleton<T>
  {
    private static T s_Instance;
    
    public static T Inst
    {
      get
      {
        if (!s_Instance)
        {
          s_Instance = FindObjectOfType<T>();
          if (!s_Instance)
          {
            Debug.LogError($"Not Found");
          }
          DontDestroyOnLoad(s_Instance.gameObject);
        }

        return s_Instance;
      }
    }
  }
}