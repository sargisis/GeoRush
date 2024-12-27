using TMPro;
using UnityEngine;

public class CenteredText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Transform textTransform;
        
    private void Start()
    {
        textTransform.position = new Vector3(textTransform.position.x, textTransform.position.y, textTransform.position.z);
    }
    private void Update()
    {
     
        textComponent.text = textTransform.position.z.ToString("0");
    }
}