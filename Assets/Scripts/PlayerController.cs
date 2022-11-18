using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public GameObject[] Items;

    public int MaxInventory = 4;
    public int InventoryFull { get { return _inventory.Count; } }
    public List<Color> Inventory { get { return _inventory; } }

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private float Speed;

    private List<Color> _inventory;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inventory = new List<Color>();
        Refresh();
    }

    void FixedUpdate()
    {
        Vector2 position = _rigidbody.position;
        position.x += Speed * _joystick.Horizontal * Time.deltaTime;
        position.y += Speed * _joystick.Vertical * Time.deltaTime;

        _rigidbody.MovePosition(position);
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
