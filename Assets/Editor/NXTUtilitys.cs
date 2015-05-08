using UnityEngine;
using UnityEditor;
using System.Collections;

public class NXTUtilitys : Editor
{
    [MenuItem ("NXT/OpenScene/Assets")]
    public static void OpenAssetsScene()
    {
        if(EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorApplication.OpenScene("Assets/Scenes/Development/Holykiller/Assets.unity");
        }
    }
    [MenuItem("NXT/OpenScene/Tutorial Level")]
    public static void OpenTutorialLevel()
    {
        if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorApplication.OpenScene("Assets/Scenes/Development/TutorialLevel/TutorialLevel.unity");
        }
    }

    private void OpenScene(string SceneName)
    {
        if(EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorApplication.OpenScene("Assets/Scenes/" + SceneName + ".unity");
        }
    }
}
