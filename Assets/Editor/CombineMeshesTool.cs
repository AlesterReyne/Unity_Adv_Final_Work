using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public static class CombineMeshesTool
{
    [MenuItem("Tools/Combine Selected Meshes")]
    static void CombineSelectedMeshes()
    {
        GameObject[] selected = Selection.gameObjects;
        if (selected.Length < 2)
        {
            Debug.LogWarning("Select at least two objects to combine.");
            return;
        }

        Dictionary<Material, List<MeshFilter>> groups = new Dictionary<Material, List<MeshFilter>>();

        foreach (GameObject go in selected)
        {
            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            MeshFilter mf = go.GetComponent<MeshFilter>();

            if (mr == null || mf == null || mr.sharedMaterial == null)
                continue;

            if (!groups.ContainsKey(mr.sharedMaterial))
                groups[mr.sharedMaterial] = new List<MeshFilter>();

            groups[mr.sharedMaterial].Add(mf);
        }

        foreach (var group in groups)
        {
            CombineInstance[] combines = new CombineInstance[group.Value.Count];

            for (int i = 0; i < group.Value.Count; i++)
            {
                combines[i].mesh = group.Value[i].sharedMesh;
                combines[i].transform = group.Value[i].transform.localToWorldMatrix;
                group.Value[i].gameObject.SetActive(false);
            }

            Mesh combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(combines);

            GameObject combinedGO = new GameObject("CombinedMesh_" + group.Key.name);
            combinedGO.AddComponent<MeshFilter>().sharedMesh = combinedMesh;
            combinedGO.AddComponent<MeshRenderer>().sharedMaterial = group.Key;
        }

        Debug.Log("Meshes combined successfully.");
    }
}