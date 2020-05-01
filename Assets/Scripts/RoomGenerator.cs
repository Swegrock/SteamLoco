using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public List<GameObject> CommonItems;
    public List<GameObject> RareItems;
    public float SpawnSize;
    public int MaxItems;
    public bool ShouldItemsStack;
    public float SizeVariation;
    private List<Transform> spawnPointsCommon;
    private List<Transform> spawnPointsRare;
    void Start()
    {
        spawnPointsCommon = new List<Transform>();
        spawnPointsRare = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++){
            Transform child = transform.GetChild(i);
            if (child.gameObject.tag == "Spawn"){
                spawnPointsCommon.Add(child);
            } else if (child.gameObject.tag == "SpawnRare"){
                spawnPointsRare.Add(child);
            }
        }
        for (int i = 0; i < MaxItems; i++){
            if (spawnPointsCommon.Count <= 0){
                break;
            }
            GameObject itemTemplate = GetItemToAdd();
            Transform point = spawnPointsCommon[Random.Range(0, spawnPointsCommon.Count - 1)];
            if (i % 4 == 0 && RandRange(0,1) > 0.5f){
                itemTemplate = GetRareItemToAdd();
                point = spawnPointsRare[Random.Range(0, spawnPointsRare.Count - 1)];
            }
            Vector3 position = point.position;
            position.x += RandRange(-SpawnSize, SpawnSize);
            position.z += RandRange(-SpawnSize, SpawnSize);
            position.y = 3;
            RaycastHit hit;
            if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Ground"))){
                GameObject item = Instantiate(itemTemplate, new Vector3(position.x, hit.point.y + itemTemplate.GetComponent<Renderer>().bounds.extents.y, position.z), Quaternion.identity);
                item.transform.Rotate(Vector3.up * 90 * ((int)Random.Range(0, 2)), Space.Self);
                item.transform.SetParent(transform);

                if (SizeVariation > 0){
                    float scale = RandRange(-SizeVariation, SizeVariation);
                    Vector3 newScale = new Vector3(item.transform.localScale.x + scale,
                                                   item.transform.localScale.y + scale,
                                                   item.transform.localScale.z + scale);
                    item.transform.localScale = newScale;
                    item.GetComponent<BoxCollider>().size = newScale;
                }
            }
            if (!ShouldItemsStack){
                if (spawnPointsCommon.Contains(point))
                    spawnPointsCommon.Remove(point);
                else if (spawnPointsRare.Contains(point))
                    spawnPointsRare.Remove(point);
            }
        }
    }

    private List<int> CreateItemAmounttForEachItem(int amount, int totalCount){
        List<int> itemCount = new List<int>();
        for (int i = 0; i < totalCount; i++){
            itemCount.Add(amount);
        }
        return itemCount;
    }

    private GameObject GetItemToAdd(){
        return CommonItems[Random.Range(0, CommonItems.Count - 1)];
    }

    private GameObject GetRareItemToAdd(){
        return RareItems[Random.Range(0, RareItems.Count - 1)];
    }

    private float RandRange(float start, float end){
        float rand = Random.Range(start, end);
        return rand - (rand % 0.1f);
    }
}