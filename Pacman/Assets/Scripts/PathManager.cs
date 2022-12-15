using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance;

    private List<int> CanUsePath;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        CanUsePath = new List<int>();
        for (int i = 0; i < transform.childCount; i++)
        {
            CanUsePath.Add(i);
        }
    }
    public Vector3[] GetPath(out int childPathIdx) {
        int randomIdx = Random.Range(0, CanUsePath.Count);
        childPathIdx = CanUsePath[randomIdx];
        CanUsePath.Remove(childPathIdx);
        Transform childPath = transform.GetChild(childPathIdx);
        Vector3[] allPoints = new Vector3[childPath.childCount];
        for (int i = 0; i < allPoints.Length; i++)
        {
            allPoints[i] = childPath.GetChild(i).position;
        }
        return allPoints;
    }
    public void BackPath(int childPathIdx) {
        CanUsePath.Add(childPathIdx);
    }
}
