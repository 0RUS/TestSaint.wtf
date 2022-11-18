using UnityEngine;

public class OutputStorageController : MonoBehaviour
{
    public int ResourceOut = 1;

    public GameObject[] Items;

    public int SizeOfStorage = 4;
    
    public float WaitTime = 1.0f;

    public int OutputResourceCount { get { return _outputResourceCount; } }
    int _outputResourceCount;

    private float _timer = 0.0f;
    private Collider2D _graber = null;

    private ColorManager manager;

    void Start()
    {
        manager = gameObject.AddComponent<ColorManager>();
        Color tmp = manager.Colors[ResourceOut];
        tmp.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        _outputResourceCount = 0;
        Refresh();
    }

    private void FixedUpdate()
    {
        if (_graber != null)
            _timer += Time.deltaTime;

        if (_timer > WaitTime)
        {
            _timer -= WaitTime;
            GiveItem();
        }
    }

    public void ChangeOutputResourcesCount(int changer)
    {
        int tmp = _outputResourceCount + changer;
        if (tmp >= 0 && tmp <= SizeOfStorage)
            _outputResourceCount = tmp;
        Refresh();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _graber = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _graber = null;
        _timer = 0.0f;
    }

    private bool GiveItem()
    {
        if (OutputResourceCount > 0)
        {
            PlayerController controller = _graber.GetComponent<PlayerController>();

            if (controller != null)
            {
                if (controller.Inventory.Count < controller.MaxInventory)
                {
                    controller.GiveItem(manager.Colors[ResourceOut]);
                    ChangeOutputResourcesCount(-1);
                    Refresh();
                    return true;
                }
            }
        }
        return false;
    }

    private void Refresh()
    {
        for (int i = 0; i < SizeOfStorage; i++)
        {
            if (i < OutputResourceCount)
                Items[i].GetComponent<SpriteRenderer>().color = manager.Colors[ResourceOut];
            else
                Items[i].GetComponent<SpriteRenderer>().color = manager.Colors[0];
        }
    }
}
