using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    WayPoint WayPoint => target as WayPoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.green;
        
        for (int i = 0; i < WayPoint._wayPointList.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            
            Vector3 handleWayPoint = Handles.FreeMoveHandle(WayPoint._wayPointList[i].position, 0.7f, Vector3.zero, 
                Handles.SphereHandleCap);

            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.BoldAndItalic;
            textStyle.fontSize = 14;
            textStyle.normal.textColor = Color.white;

            Vector3 textAllignment = Vector3.down * 0.4f + Vector3.right * 0.4f;

            Handles.Label(WayPoint._wayPointList[i].position + textAllignment, $"{i + 1}", textStyle);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Moving Handle");
                WayPoint._wayPointList[i].position = handleWayPoint;
            }
        }
    }
}
