using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


[CustomEditor(typeof(EnvironmentSettingsManager))]
public class LightingEditor : Editor
{
    
    private static EnvironmentSettingsManager EnvironmentSettingsManager;


    #if UNITY_EDITOR
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    
        EnvironmentSettingsManager esm = (EnvironmentSettingsManager)target;
        
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Day Mode"))
        {
            esm.DayMode();
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        
        if (GUILayout.Button("Night Mode"))
        {
            esm.NightMode();
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        
        GUILayout.EndHorizontal();

        
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Clear"))
        {
            esm.Clear();
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        
        if (GUILayout.Button("Rain"))
        {
            esm.Rain();
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        
        if (GUILayout.Button("Snow"))
        {
            esm.Snow();
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        
        if (GUILayout.Button("Fog"))
        {
            esm.Fog();
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        
        if (GUILayout.Button("Windy"))
        {
            esm.Windy();
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        
        
        
        GUILayout.EndHorizontal();
        
    }
    
    #endif
    
    
    
}
