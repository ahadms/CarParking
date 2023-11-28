using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlasticPipe.PlasticProtocol.Messages;

public class WayPointMAnagerWindow : EditorWindow
{

    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WayPointMAnagerWindow>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if(waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform Must Be Selected. please assign a root transform.",MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButton();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void DrawButton()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
         if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<WayPoint>())
        {
            if(GUILayout.Button("Add BranchWaypoint"))
            {
                CreateBranch();
            }
            if(GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }
            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }
            if (GUILayout.Button("RemoveWayPoint"))
            {
                RemoveWaypoint();
            }
        }
    }

  void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint waypoint = waypointObject.GetComponent<WayPoint>();
        if(waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<WayPoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;

            //place the waypoint at last position
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }
        Selection.activeGameObject = waypoint.gameObject;
    }

    void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject("WayPoint"+ waypointRoot.childCount,typeof(WayPoint));
        waypointObject.transform.SetParent (waypointRoot, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if(selectedWaypoint.previousWaypoint != null)
        {
            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;
        }

        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("WayPoint" + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if(selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }

        selectedWaypoint.nextWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void RemoveWaypoint()
    {
        WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();

        if(selectedWaypoint.nextWaypoint != null )
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }
        if(selectedWaypoint.previousWaypoint != null )
        {
            selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
        }
        Destroy( selectedWaypoint.gameObject );
    }

  void CreateBranch()
    {
        GameObject waypointObject = new GameObject("WayPoint" + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot,false);

        WayPoint waypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint branchedfrom = Selection.activeGameObject.GetComponent<WayPoint>();
        branchedfrom.branches.Add(waypoint);

        waypoint.transform.position = branchedfrom.transform.position;
        waypoint.transform.forward = branchedfrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }
}
