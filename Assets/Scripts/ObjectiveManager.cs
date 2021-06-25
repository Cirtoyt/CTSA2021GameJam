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

        int[] randNums = new int[] { -1, -1, -1 };

        for (int i = 0; i < randNums.Length; i++)
        {
            while (NotUnique(randNums[i], i))
            {
                randNums[i] = Random.Range(0, 100);
            }
        }

        bool NotUnique(int _currentValue, int _currentIndex)
        {
            if (_currentValue > 0)
            {
                for (int i = 0; i < randNums.Length; i++)
                {
                    // if not the current element and the values match
                    if (i != _currentIndex && _currentValue == randNums[i])
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        int boxWithItem = (int)Random.Range(0, numberOfBoxes - 1);
        for(int i = 0; i < numberOfBoxes; i++)
        {
            boxes.Add(Instantiate(boxPrefab, spawnPoints[i].transform.position, Quaternion.identity));
            for (i = 0; i < randNums.Length; i++)
            {
                if (i == boxWithItem)
                {
                    boxes[i].GetComponent<DestructibleObject>().holdsItem = true;
                }
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
