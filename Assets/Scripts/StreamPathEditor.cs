//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

////[CustomEditor(typeof(CharacterClass))]
//public class StreamPathEditor : Editor
//{
//    SerializedProperty filePath;
//    SerializedProperty streamingAsset;

//    const string kAssetPrefix = "Assets/Resources";

//    void OnEnable()
//    {
//        filePath = serializedObject.FindProperty("filePath");
//        streamingAsset = serializedObject.FindProperty("streamingAsset");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        EditorGUILayout.PropertyField(streamingAsset);
//        EditorGUILayout.PropertyField(filePath);

//        if (streamingAsset.objectReferenceValue == null)
//        {
//            return;
//        }

//        string assetPath = AssetDatabase.GetAssetPath(streamingAsset.objectReferenceValue.GetInstanceID());
//        Debug.Log("AssetPath:" + assetPath);
//        if (assetPath.StartsWith(kAssetPrefix))
//        {
//            assetPath = assetPath.Substring(kAssetPrefix.Length);
//        }
//        filePath.stringValue = assetPath;
//        serializedObject.ApplyModifiedProperties();
//    }
//}
