using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f;

    public int MaxInventory = 4;

    public GameObject[] Items;

    public int InventoryFull { get { return _inventory.Count; } }
    public List<Color> Inventory { get { return _inventory; } }

    private Rigidbody2D _rigidbody2d;
    private float _horizontal;
    private float _vertical;

    private List<Color> _inventory;

    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _inventory = new List<Color>();
        Refresh();
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 position = _rigidbody2d.position;
        position.x += Speed * _horizontal * Time.deltaTime;
        position.y += Speed * _vertical * Time.deltaTime;

        _rigidbody2d.MovePosition(position);
    }

    public void GiveItem(Color color)
    {
        ChangeInventory(color, true);
    }
    public void GetItem(Color color)
    {
        ChangeInventory(color, false);
    }


    private void ChangeInventory(Color item, bool flag)
    {
        if (flag)
        {
            _inventory.Add(item);
            Refresh();
            return;
        }

        _inventory.Remove(item);
        Refresh();
    }

    private void Refresh()
    {
        for (int i = 0; i < MaxInventory; i++)
        {
            if (i < Inventory.Count)
            {
                Items[i].GetComponent<SpriteRenderer>().color = Inventory[i];
            }
            else
            {
                Items[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
