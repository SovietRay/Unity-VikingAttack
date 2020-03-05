#if (UNITY_EDITOR) 
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
//inspired from https://gist.github.com/thebeardphantom/ea6362139ee195a8abce

public class LazyShortcuts : MonoBehaviour
{
    public static bool enableResetTransform = true, enableResetName = true, enableRevertPrefab = true, enableSaveChangesToPrefab = true, enableToggleDebugNormalView = true, enableToggleLockUnlockInspector = true;
    
    [MenuItem("Tools/LazyShortcuts/Reset Transform &r")]
    static void ResetTransform()
    {
        GameObject[] selection = Selection.gameObjects;
        if (selection.Length < 1) return;
        Undo.RegisterCompleteObjectUndo(selection, "Zero Position");
        foreach (GameObject go in selection)
        {
            InternalZeroPosition(go);
            InternalZeroRotation(go);
            InternalZeroScale(go);
        }
    }

    [MenuItem("Tools/LazyShortcuts/Reset Transform &r", true)]
    static bool ResetTransformValidation()
    {
        return enableResetTransform;
    }

    [MenuItem("Tools/LazyShortcuts/Reset Name &n")]
    static void ResetName()
    {
        GameObject[] selection = Selection.gameObjects;
        if (selection.Length < 1) return;

        Undo.RegisterCompleteObjectUndo(selection, "Reset Name");
        foreach (GameObject go in selection)
        {
            Rename(go);
        }
    }

    [MenuItem("Tools/LazyShortcuts/Reset Name &n", true)]
    static bool ResetNameValidation()
    {
        return enableResetName;
    }

    [MenuItem("Tools/LazyShortcuts/Revert Prefab &p")]
    static void RevertPrefab()
    {
        GameObject[] selection = Selection.gameObjects;
        if (selection.Length < 1) return;
        Undo.RegisterCompleteObjectUndo(selection, "Revert Prefab");
        foreach (GameObject go in selection)
        {
            if (PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
            {
                PrefabUtility.RevertPrefabInstance(go);
            }
        }
    }

    [MenuItem("Tools/LazyShortcuts/Revert Prefab &p", true)]
    static bool RevertPrefabValidation()
    {
        return enableRevertPrefab;
    }

    [MenuItem("Tools/LazyShortcuts/Apply changes to Prefab &a")]
    static void SaveChangesToPrefab()
    {
        GameObject[] selection = Selection.gameObjects;
        if (selection.Length < 1) return;
        Undo.RegisterCompleteObjectUndo(selection, "Apply Prefab");
        
        foreach (GameObject go in selection)
        {
            if (PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
            {
                PrefabUtility.ReplacePrefab(go, PrefabUtility.GetPrefabParent(go));
                PrefabUtility.RevertPrefabInstance(go);
            }
        }
    }

    [MenuItem("Tools/LazyShortcuts/Apply changes to Prefab &a", true)]
    static bool SaveChangesToPrefabValidation()
    {
        return enableSaveChangesToPrefab;
    }

    [MenuItem("Tools/LazyShortcuts/Toggle Debug or Normal &d")]
    static void ToggleDebugNormalView()
    {
        EditorWindow inspectorWindow;
        EditorWindow[] editorWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
        foreach (EditorWindow editorWindow in editorWindows)
        {
            if (editorWindow.GetType().Name == "InspectorWindow")
            {
                if (EditorWindow.focusedWindow == editorWindow)
                {
                    inspectorWindow = editorWindow;
                    Type inspector = inspectorWindow.GetType();
                    MethodInfo methodInfo = inspector.GetMethod("SetMode", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (inspector.GetField("m_InspectorMode").GetValue(inspectorWindow).ToString() == "Debug")
                    {
                        methodInfo.Invoke(inspectorWindow, new object[] { InspectorMode.Normal });
                    }
                    else methodInfo.Invoke(inspectorWindow, new object[] { InspectorMode.Debug });
                    break;
                }
            }
        }
    }

    [MenuItem("Tools/LazyShortcuts/Toggle Debug or Normal &d", true)]
    static bool ToggleDebugNormalViewValidation()
    {
        return enableToggleDebugNormalView;
    }

    [MenuItem("Tools/LazyShortcuts/Toggle Lock Inspector &l")]
    static void ToggleLockUnlockInspector()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.activeEditors[0].Repaint();
    }

    [MenuItem("Tools/LazyShortcuts/Toggle Lock Inspector &l", true)]
    static bool ToggleLockUnlockInspectorValidation()
    {
        return enableToggleLockUnlockInspector;
    }
        private static void Rename(GameObject go)
    {
        int start = go.name.IndexOf("(");
        print(start);
        int end = go.name.IndexOf(")");
        print(end);
        if (start != -1 && end != -1 && start < end)
        {
            print("rename");
            go.name = go.name.Substring(0, start);
        }
    }

    private static void InternalZeroPosition(GameObject go)
    {
        go.transform.localPosition = Vector3.zero;
    }

    private static void InternalZeroRotation(GameObject go)
    {
        go.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private static void InternalZeroScale(GameObject go)
    {
        go.transform.localScale = Vector3.one;
    }
}
#endif