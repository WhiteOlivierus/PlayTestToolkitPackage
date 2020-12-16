using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Dutchskull.Utilities.Extensions
{
    public static class SceneManagementExtension
    {
        public static IEnumerable<Scene> GetAllLoadedScenes()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
                yield return SceneManager.GetSceneAt(i);
        }
    }
}
