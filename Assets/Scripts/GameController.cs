using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ResourceController Storage1;
    public ResourceController Storage2;
    public ResourceController Storage3;

    public Text Text;

    private string _message = "";

    void FixedUpdate()
    {
        _message += CheckStorage(Storage1);
        _message += CheckStorage(Storage2);
        _message += CheckStorage(Storage3);
        
        Text.text = _message;
        _message = "";
    }

    private string CheckStorage(ResourceController Storage)
    {
        if (Storage != null)
        {
            string s = "";
            if (Storage.NumberOfResourcesToGenerate > 0)
                if (Storage.StorageIn.FirstResourceCount == 0)
                    s += $"There is no first resource in {Storage.gameObject.name}\n";

            if (Storage.NumberOfResourcesToGenerate > 1)
                if (Storage.StorageIn.SecondResourceCount == 0)
                    s += $"There is no second resource in {Storage.gameObject.name}\n";

            if (Storage.StorageOut.OutputResourceCount == Storage.StorageOut.SizeOfStorage)
                s += $"{Storage.gameObject.name} is full\n";

            return s;
        }
        else { return ""; }
    }
}
