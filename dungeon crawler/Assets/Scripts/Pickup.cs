using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Management
    private Hotbar hotbar;
    public GameObject icon;
    public int ID;
    private ItemManager itemmanager;

    //Bobbing
    public float speed = 2f;
    public float maxRotation = 10f;

    // Start is called before the first frame update
    void Start()
    {
        hotbar = GameObject.FindGameObjectWithTag("HotBar").GetComponent<Hotbar>();
        itemmanager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        speed = Random.Range(speed, speed * 0.5f);
    }

    void Update()
    {
        // Rotate t varying speeds
        transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            for (int i = 0; i < hotbar.slots.Length; i++)
            {
                if (hotbar.isFull[i] == false)
                {
                    // Add to hotbar
                    hotbar.isFull[i] = true;
                    hotbar.CarriedItemIDs[i] = ID;
                    Instantiate(icon, hotbar.slots[i].transform, false);

                    // Hold in hand if Active
                    if (hotbar.isActive[i] == true)
                    {
                        itemmanager.Hold(hotbar.CarriedItemIDs[i]);
                    }

                    Destroy(gameObject);
                    break; 
                }
            }
        }
    }
}
