using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;

public class MapGenerator : MonoBehaviour
{
    public float width;
    public float depth;
    public int nbrCheckpoints;
    public float minDistBtwCheckpoints;
    public GameObject pen;
    public GameObject positivePrefab;
    public GameObject negativePrefab;
    private GameObject closestobjectToStart;
    private GameObject closestobjectToEnd;
    private List<GameObject> LstCheckpoints;
    private Transform map;
    int checkpointPenCpt;
    public GameObject checkpoint;
    public GameObject Spawner;
    public GameObject MainMapAnchor;
    public GameObject CustomAnchor;
    bool readyForPath = false;
   
    public void GenerateMap()
    {
        map = GameObject.FindWithTag("Map").transform;
        checkpointPenCpt = 0;
        //if ((Mathf.Approximately(positivePrefab.transform.localScale.x, negativePrefab.transform.localScale.x)) && (Mathf.Approximately(positivePrefab.transform.localScale.z, negativePrefab.transform.localScale.z)))
        //{
            DrawMap();
       // }
        pen.transform.position = closestobjectToStart.transform.position;
        readyForPath = true;
    }
    private void DrawMap()
    {
        float x;
        float z;
        float zmax = 0;
        float xmax = 0;
        for (x = 0; x < width; x = x + positivePrefab.transform.localScale.x)
        {
            for (z = 0; z < depth; z = z + positivePrefab.transform.localScale.z)
            {

                Instantiate(positivePrefab, new Vector3(x, 0, z), Quaternion.identity, map);
            }
            zmax = z - positivePrefab.transform.localScale.z;
        }
        xmax = x;
        DrawPath(xmax, zmax);
    }
    void DrawPath(float x, float z)
    {
        GameObject startPoint = Instantiate(Spawner, new Vector3(x, 0, Random.Range(0, (z / 3))),Quaternion.identity, map);
        GameObject endPoint = new GameObject();
        GameObject tmpcheckpoint = new GameObject();
        LstCheckpoints = new List<GameObject>();

        startPoint.name = "Spawner1";
        startPoint.tag = "Spawner";
        endPoint.name = "Position" + (nbrCheckpoints+1);
        endPoint.tag = startPoint.name;
        endPoint.transform.parent = map;
        endPoint.transform.position = new Vector3(0, 0, Random.Range((z - startPoint.transform.position.z), z));
        for (int i = 0; i < nbrCheckpoints;)
        {
            Vector3 PositionCheckpoint;
            PositionCheckpoint = new Vector3(Random.Range(startPoint.transform.position.x, endPoint.transform.position.x), 0, Random.Range(startPoint.transform.position.z, endPoint.transform.position.z));
            if ((Vector3.Distance(PositionCheckpoint, startPoint.transform.position) > minDistBtwCheckpoints) && (Vector3.Distance(PositionCheckpoint, endPoint.transform.position) > minDistBtwCheckpoints))
            {
               if (FarFromAllListValues(LstCheckpoints, PositionCheckpoint) == 0)
                {
                    tmpcheckpoint = Instantiate(checkpoint, PositionCheckpoint + new Vector3(0, 0.012f, 0), Quaternion.identity, map);
                    tmpcheckpoint.name = "Position";
                    tmpcheckpoint.tag = startPoint.name;
                    LstCheckpoints.Add(tmpcheckpoint);
                    i++;
                }
                
            }
        }
        closestobjectToStart = GetClosestObject(startPoint);
        closestobjectToEnd = GetClosestObject(endPoint);
        closestobjectToStart.GetComponent<Renderer>().material = negativePrefab.GetComponent<Renderer>().sharedMaterial;
        closestobjectToEnd.GetComponent<Renderer>().material = negativePrefab.GetComponent<Renderer>().sharedMaterial;
        if (LstCheckpoints.Count > 0)
        {
            LstCheckpoints.Sort(delegate (GameObject a, GameObject b)
            {
                return (Vector3.Distance(a.transform.position, startPoint.transform.position)).CompareTo(Vector3.Distance(b.transform.position, startPoint.transform.position));
            });
        }
        int j = 1;
        foreach (GameObject checkpoint in LstCheckpoints )
        {
            checkpoint.name = checkpoint.name + j;
            j++;
        }

    }

    int FarFromAllListValues (List<GameObject> LstCheckpointss, Vector3 position)
    {
        int i; 
        for (i = 0; i < LstCheckpoints.Count;)
        {
            if (Vector3.Distance(position, LstCheckpoints[i].transform.position) > minDistBtwCheckpoints)
            {
                i++;
            }
            else
                return (1);
        }
        return (0);
    }

    GameObject GetClosestObject(GameObject theObject)
    {
        Collider[] colliders = Physics.OverlapSphere(theObject.transform.position, 0.009f);
        Collider closestCollider = null;

        foreach (Collider hit in colliders)
        {
            //checks if it's hitting itself
            if (hit.GetComponent<Collider>() == theObject.transform.GetComponent<Collider>() || (hit.GetComponent<Collider>().tag != "Mapcube"))
            {
                continue;
            }
            if (!closestCollider)
            {
                closestCollider = hit;
            }
            //compares distances
            if (Vector3.Distance(transform.position, hit.transform.position) <= Vector3.Distance(transform.position, closestCollider.transform.position))
            {
                closestCollider = hit;
            }
        }
            return closestCollider.gameObject;
    }

    private void FixedUpdate()
    {
        if (readyForPath == true)
        {
        float speed = 1f;
        float step = speed * Time.deltaTime;
        if (LstCheckpoints.Count > 0 && checkpointPenCpt < LstCheckpoints.Count)
        {
           pen.transform.position = Vector3.MoveTowards(pen.transform.position, LstCheckpoints[checkpointPenCpt].transform.position, step);
           if (pen.transform.position == LstCheckpoints[checkpointPenCpt].transform.position)
                checkpointPenCpt++;
        }
        if (checkpointPenCpt == LstCheckpoints.Count)
            pen.transform.position = Vector3.MoveTowards(pen.transform.position, closestobjectToEnd.transform.position, step);
            if (pen.transform.position == closestobjectToEnd.transform.position)
            {
                pen.SetActive(false);
                ChangeMapParent();
                this.enabled = false;
            }
        }
    }

    void ChangeMapParent()
    {
        MainMapAnchor.transform.parent = CustomAnchor.transform;
        MainMapAnchor.transform.localPosition = new Vector3(-0.5f, 0, -0.25f);
        MainMapAnchor.transform.localRotation = Quaternion.identity;
    }
}
                             