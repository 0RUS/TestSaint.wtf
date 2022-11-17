using UnityEngine;

public class InputStorageController : MonoBehaviour
{
    public int ResourceIn1 = 0;
    public int ResourceIn2 = 0;

    public GameObject[] Items;

    public int SizeOfStorage = 4;

    public float WaitTime = 1.0f;
    public int FirstResourceCount { get { return _countOfResource1; } }
    public int SecondResourceCount { get { return _countOfResource2; } }

    private int _countOfResource1;
    private int _countOfResource2;

    private ColorManager manager;

    private float _timer = 0.0f;
    private Collider2D _graber = null;

    void Start()
    {
        manager = new ColorManager();
        Color tmp = Color.Lerp(manager.Colors[ResourceIn1],manager.Colors[ResourceIn2],0.5f);
        tmp.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        _countOfResource1 = 0;
        _countOfResource2 = 0;
        Refresh();
    }

    private void FixedUpdate()
    {
        if (_graber != null)
            _timer += Time.deltaTime;

        if (_timer > WaitTime)
        {
            _timer -= WaitTime;
            GetItem();
        }
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

    private bool GetItem()
    {
        if (FirstResourceCount + SecondResourceCount < SizeOfStorage)
        {
            PlayerController controller = _graber.GetComponent<PlayerController>();

            if (controller != null)
            {
                if (controller.Inventory.Count > 0)
                {
                    foreach (Color col in controller.Inventory)
                    {
                        if (col == manager.Colors[ResourceIn1])
                        {
                            controller.GetItem(col);
                            ChangeInputFirstResourcesCount(1);
                            return true;
                        }
                        if (col == manager.Colors[ResourceIn2])
                        {
                            controller.GetItem(col);
                            ChangeInputSecondResourcesCount(1);
                            return true;
                        }
                        Refresh();
                    }
                }
            }
        }
        return false;
    }

    public void ChangeInputFirstResourcesCount(int changer)
    {
        _countOfResource1 += changer;
        Refresh();
    }

    public void ChangeInputSecondResourcesCount(int changer)
    {
        _countOfResource2 += changer;
        Refresh();
    }
    private void Refresh()
    {
        for (int i = 0; i < SizeOfStorage; i++)
        {
            if (i < FirstResourceCount)
                Items[i].GetComponent<SpriteRenderer>().color = manager.Colors[ResourceIn1];
            else if (i < FirstResourceCount + SecondResourceCount)
                Items[i].GetComponent<SpriteRenderer>().color = manager.Colors[ResourceIn2];
            else
                Items[i].GetComponent<SpriteRenderer>().color = manager.Colors[0];
        }
    }
}