using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    [TextArea(4, 10)]
    public string[] firstPart;

    [TextArea(4, 10)]
    public string[] secondPart;

    [TextArea(4, 10)]
    public string[] thirdPart;

    [TextArea(4, 10)]
    public string[] fourhPart;



}
