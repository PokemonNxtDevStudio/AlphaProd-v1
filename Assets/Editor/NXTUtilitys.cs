using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
#if UNITY_EDITOR
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

    [MenuItem("NXT/Tools/SetToGoundLevel %g")]
    public static void SetToGoundLevel()
    {
        Transform[] trans = Selection.transforms;
        
        foreach(Transform myTransforms in trans)
        {
            RaycastHit hit;
           
            if(Physics.Raycast(myTransforms.position, - Vector3.up,out hit))
            {
                Vector3 targetposition = hit.point;
                myTransforms.position = targetposition;

            }
        }
    }
}
#endif