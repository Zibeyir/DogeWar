using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortAddress : MonoBehaviour
{
    public Web3 Web3;
    public Text FullText;
    public Text ShortText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var fullText  = this.FullText.text;
        this.ShortText.text = !string.IsNullOrEmpty(fullText) && fullText.StartsWith("0x")
            ? string.Format("{0}...{1}", fullText.Substring(0, 6), fullText.Substring(fullText.Length - 4))
            : fullText;
    }
}
