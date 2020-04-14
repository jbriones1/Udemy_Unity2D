using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

public class UnityEditColliderHotkey : MonoBehaviour
{
    [MenuItem("Edit/Custom/Edit Collider Mode #_e")] // This is Shift + e
    private static void EditCollider()
    {
        var sel = Selection.activeGameObject;
        var col = sel.GetComponent<Collider2D>();

        if (!col)
            return;

        if (UnityEditorInternal.EditMode.editMode == EditMode.SceneViewEditMode.Collider)
        {
            UnityEditorInternal.EditMode.ChangeEditMode(UnityEditorInternal.EditMode.SceneViewEditMode.None, new Bounds(), null);
        }
        else
        {
            Type colliderEditorBase = System.Type.GetType("UnityEditor.ColliderEditorBase,UnityEditor.dll");
            Editor[] colliderEditors = Resources.FindObjectsOfTypeAll(colliderEditorBase) as Editor[];

            if (colliderEditors == null || colliderEditors.Length <= 0)
                return;

            UnityEditorInternal.EditMode.ChangeEditMode(UnityEditorInternal.EditMode.SceneViewEditMode.Collider, col.bounds, colliderEditors[0]);
        }

        //Debug.Log("EditMode: " + UnityEditorInternal.EditMode.editMode);
    }
}
