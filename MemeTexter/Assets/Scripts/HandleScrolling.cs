using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class HandleScrolling : MonoBehaviour
{
    public RectTransform child;
    public RectTransform parent;
    public RectTransform mostRecentMessage;
    public ScrollRect scroll;

    // Update is called once per frame
    void Update()
    {
        //since the ability of the scrollRect to scroll depends on the size of the content,
        //we need to adjust the size of the contentContainer to reflect that of one of the message queues
        if (child.rect.height > parent.rect.height)
        {
            parent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parent.rect.height + mostRecentMessage.rect.height);
            scroll.verticalNormalizedPosition = 0;
        }
    }
}
