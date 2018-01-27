using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UtilFunction
{

    public static List<GameObject> GetPositions(string prefabPath, string prefix)
    {
        List<GameObject> result = new List<GameObject>();

        GameObject parent = UtilFunction.ResourceLoad(prefabPath);
        Transform[] child = parent.GetComponentsInChildren<Transform>();

        int childCount = child.Length;
        for (int i = 0; i < childCount; ++i)
        {
            string name = prefix + i;
            for (int j = 0; j < childCount; ++j)
            {
                if (name.Equals(child[j].name))
                {
                    result.Add(child[j].gameObject);
                }
            }
        }
        return result;
    }

    public static List<Vector3> GetPositionsEx(string prefabPath, string prefix)
    {
        List<Vector3> result = new List<Vector3>();

        GameObject parent = UtilFunction.ResourceLoad(prefabPath);
        Transform[] child = parent.GetComponentsInChildren<Transform>();

        int childCount = child.Length;
        for (int i = 0; i < childCount; ++i)
        {
            string name = prefix + i;
            for (int j = 0; j < childCount; ++j)
            {
                if (name.Equals(child[j].name))
                {
                    result.Add(child[j].position);
                }
            }
        }
        GameObject.Destroy(parent);

        return result;
    }

    public static GameObject ResourceLoad(string path)
    {
        Object o = Resources.Load(path);
        GameObject go = GameObject.Instantiate(o) as GameObject;

        return go;
    }

    public static GameObject ResourceLoadOnPosition(string path, Vector3 v, Quaternion q)
    {
        Object o = Resources.Load(path);
        GameObject go = GameObject.Instantiate(o, v, q) as GameObject;

        return go;
    }

    public static string GetPrefabName(string path)
    {
        string name = Resources.Load(path).name;
        return name;
    }

    public static void GameObjectReset(GameObject go)
    {
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
    }

    public static bool Random01()
    {
        int rand = Random.Range(0, 2);
        if (rand % 2 == 0)
            return false;
        return true;
    }

    public static bool IsReachDistance(Vector3 pos1, Vector3 pos2, float dis)
    {
        if (Vector3.Distance(pos1, pos2) <= dis)
            return true;
        return false;
    }

    public static bool IsReachDistanceXZ(Vector3 v1, Vector3 v2, float dis)
    {
        v1 = new Vector3(v1.x, 0.0f, v1.z);
        v2 = new Vector3(v2.x, 0.0f, v2.z);

        if (Vector3.Distance(v1, v2) <= dis)
            return true;
        return false;
    }

    public static bool IsReachDistanceXYZ(Vector3 v1, Vector3 v2, float dis ,float dis_y=2)
    {
        if (Mathf.Abs(v1.y - v2.y) < dis_y)
        {
            v1 = new Vector3(v1.x, 0.0f, v1.z);
            v2 = new Vector3(v2.x, 0.0f, v2.z);

        if (Vector3.Distance(v1, v2) <= dis)
            return true;
        }
        return false;
    }

    public static bool IsLeaveDistanceXYZ(Vector3 v1, Vector3 v2, float dis, float dis_y)
    {
        if (Mathf.Abs(v1.y - v2.y) < dis_y)
        {
            v1 = new Vector3(v1.x, 0.0f, v1.z);
            v2 = new Vector3(v2.x, 0.0f, v2.z);

            if (Vector3.Distance(v1, v2) >= dis)
                return true;
        }
        return false;
    }


    public static Vector3 GenRandomAreaPosition(Vector3 center, float radius, float height = 0.0f)
    {
        List<Vector3> points = GenRandomAreaPositions(4, center, radius, height);
        float rand = Random.Range(0.0f, 1.0f);

        if (rand < 0.25f)
        {
            return points[0];
        }
        else if (rand < 0.5f)
        {
            return points[1];
        }
        else if (rand < 0.75f)
        {
            return points[2];
        }
        else
        {
            return points[3];
        }

        return points[0];
    }

    public static List<Vector3> GenRandomAreaPositions(int count, Vector3 center, float radius, float height = 0.0f)
    {
        List<Vector3> result = new List<Vector3>();

        int gruopCount = Mathf.CeilToInt(count / 4);

        float[] xBounds = new float[4];
        float[] zBounds = new float[4];

        xBounds[0] = center.x + radius * 0.5f;
        zBounds[0] = center.z + radius * 0.5f;

        xBounds[1] = center.x - radius * 0.5f;
        zBounds[1] = center.z + radius * 0.5f;

        xBounds[2] = center.x - radius * 0.5f;
        zBounds[2] = center.z - radius * 0.5f;

        xBounds[3] = center.x + radius * 0.5f;
        zBounds[3] = center.z - radius * 0.5f;

        int k = 0;
        for (int i = 0; i < count; ++i)
        {
            Vector3 v = new Vector3();
            v.y = height;

            if (i < gruopCount)
            {
                k = 0;
                v.x = Random.Range(center.x, xBounds[k]);
                v.z = Random.Range(center.z, zBounds[k]);
            }
            else if (i < gruopCount * 2)
            {
                k = 1;
                v.x = Random.Range(xBounds[k], center.x);
                v.z = Random.Range(center.z, zBounds[k]);
            }
            else if (i < gruopCount * 3)
            {
                k = 2;
                v.x = Random.Range(xBounds[k], center.x);
                v.z = Random.Range(zBounds[k], center.z);
            }
            else
            {
                k = 3;
                v.x = Random.Range(center.x, xBounds[k]);
                v.z = Random.Range(zBounds[k], center.z);
            }

            result.Add(v);
        }

        return result;
    }


    public static Vector3 GenRandomAreaPositionByBoxColliderWorld(BoxCollider box, float height = 0.0f)
    {
        if (box != null)
        {
            Vector3 center = box.center;
            Vector3 size = box.size;

            float r = UnityEngine.Random.Range(-0.5f, 0.5f);
            float x = UnityEngine.Random.Range(center.x - size.x * r, center.x - size.x * r);

            r = UnityEngine.Random.Range(-0.5f, 0.5f);
            float z = UnityEngine.Random.Range(center.z - size.z * r, center.z - size.z * r);

            float y = height;

            return new Vector3(x, y, z);
        }

        return Vector3.zero;
    }


    public static Vector3 GenRandomAreaPositionByBoxColliderLocal(BoxCollider box, float height = 0.0f)
    {
        if (box != null)
        {
            Vector3 center = box.bounds.center;
            Vector3 size = box.bounds.size;

            float r = UnityEngine.Random.Range(-0.5f, 0.5f);
            float x = UnityEngine.Random.Range(center.x - size.x * r, center.x - size.x * r);

            r = UnityEngine.Random.Range(-0.5f, 0.5f);
            float z = UnityEngine.Random.Range(center.z - size.z * r, center.z - size.z * r);

            float y = height;

            return new Vector3(x, y, z);
        }

        return Vector3.zero;
    }

    public static bool qwe(Vector3 targetPos)
    {

        float dist = Vector3.Distance(new Vector3(PlayerManager.Instance.playerCollider.transform.position.x, 0, PlayerManager.Instance.playerCollider.transform.position.z), targetPos);
        if (dist < 1f)
        {
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
            return true;
        }
        return false;
    }

    public static bool ewq(Vector3 targetPos)
    {

        float dist = Vector3.Distance(new Vector3(PlayerManager.Instance.playerCollider.transform.position.x, PlayerManager.Instance.playerCollider.transform.position.y, PlayerManager.Instance.playerCollider.transform.position.z), targetPos);
        if (dist < 1f)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// 获取子节点
    /// </summary>
    /// <param name="goTrans"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    public static Transform GetChildNode(Transform goTrans,string childName)
    {
        Transform trans = null;
        trans = goTrans.Find(childName);
        if (trans == null)
        {
            foreach (Transform tempTrans in goTrans)
            {
                trans = GetChildNode(tempTrans, childName);
                if (trans != null)
                {
                    return trans;
                }
            }
        }
        return trans;
    }


    /// <summary>
    /// 获取子节点组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="goTrans"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    public static T GetChildComponent<T>(Transform goTrans, string childName)where T : Component
    {
        Transform tempTrans=GetChildNode(goTrans, childName);
        if (tempTrans == null)
        {
            Debug.Log(string.Format("<color=red>获取子节点组件出错{0}</color>",goTrans.name));
            return null;
        }

        return tempTrans.GetComponent<T>();

    }

}
