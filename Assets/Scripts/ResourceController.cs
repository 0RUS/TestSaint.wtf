using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public int NumberOfResourcesToGenerate = 0;

    public Text Text;

    public InputStorageController StorageIn;
    public OutputStorageController StorageOut;

    public float WaitTime = 3.0f;
    
    private float _timer = 0.0f;

    private void Start()
    {
        Text.text = gameObject.name;
    }

    void FixedUpdate()
    {
        if (CheckResources(NumberOfResourcesToGenerate))
        {
            _timer += Time.deltaTime;

            if (_timer > WaitTime)
            {
                _timer -= WaitTime;
                DeleteUserResources(NumberOfResourcesToGenerate);
                StorageOut.ChangeOutputResourcesCount(1);
            }
        }
    }

    private bool CheckResources(int count)
    {
        switch (count)
        {
            case 0:
                {
                    return true;
                }
            case 1:
                {
                    if (StorageIn.FirstResourceCount > 0) { return true; }
                    break;
                }
            case 2:
                {
                    if (StorageIn.FirstResourceCount > 0 && StorageIn.SecondResourceCount > 0) { return true; }
                    break;
                }
        }
        return false;
    }

    private void DeleteUserResources(int count)
    {
        switch (count)
        {
            case 1:
                {
                    StorageIn.ChangeInputFirstResourcesCount(-1);
                    break;
                }
            case 2:
                {
                    StorageIn.ChangeInputFirstResourcesCount(-1);
                    StorageIn.ChangeInputSecondResourcesCount(-1);
                    break;
                }
        }
    }
}