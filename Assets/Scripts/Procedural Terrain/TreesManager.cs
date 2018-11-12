﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesManager : MonoBehaviour
{

    #region Singleton

    public static TreesManager instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(gameObject);
    }

    #endregion Singleton

    [System.Serializable]
    public struct Tree
    {
        [Header("Prefab Stats")]
        public GameObject prefab;
        public int maxInstanceCount;

        [Header("Placement Range")]
        [Range(0, 1)]
        public float minHeightRange;
        [Range(0, 1)]
        public float maxHeightRange;

        [HideInInspector]
        public List<GameObject> instantiatedTrees;
    }

    public List<Tree> trees;

    public GameObject RequestTree(float heightValue)
    {
        for (int i = 0; i < trees.Count; i++)
        {
            if (trees[i].minHeightRange <= heightValue && trees[i].maxHeightRange >= heightValue)
            {
                int maxInstancesPossible = trees[i].maxInstanceCount;
                int currentInstanceCount = trees[i].instantiatedTrees.Count;

                if (currentInstanceCount >= maxInstancesPossible)
                {
                    for (int j = 0; j < trees[i].instantiatedTrees.Count; j++)
                    {
                        if (!trees[i].instantiatedTrees[j].activeSelf)
                            return trees[i].instantiatedTrees[j];
                    }

                    return null;
                }

                GameObject treeInstance = Instantiate(trees[i].prefab, Vector3.zero, Quaternion.identity);
                treeInstance.SetActive(false);
                treeInstance.transform.SetParent(transform);

                trees[i].instantiatedTrees.Add(treeInstance);
                return treeInstance;
            }
        }

        return null;
    }
}
