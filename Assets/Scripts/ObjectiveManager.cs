using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public int numberOfBoxes;
    public GameObject boxPrefab;
    public GameObject[] spawnPoints;

    private List<GameObject> boxes = new List<GameObject>();
    [SerializeField] private int itemsCollected;

    private void Start()
    {
        itemsCollected = 0;

        int boxWithItem = (int)Random.Range(0, numberOfBoxes - 1);
        for(int i = 0; i < numberOfBoxes; i++)
        {
            boxes.Add(Instantiate(boxPrefab, spawnPoints[i].transform.position, Quaternion.identity));

            if (i == boxWithItem)
            {
                boxes[i].GetComponent<DestructibleObject>().holdsItem = true;
            }
        }
    }

    public void incrementItems()
    {
        itemsCollected += 1;

        if(itemsCollected >= 3)
        {
            Debug.Log("3 items collected!");
        }
    }
}
