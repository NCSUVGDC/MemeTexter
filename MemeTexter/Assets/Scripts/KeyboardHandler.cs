using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardHandler : MonoBehaviour
{
    Vector2 position;
    float galleryHeight;
    float sendContainerHeight;

    public GameObject galleryContainer;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        galleryHeight = galleryContainer.GetComponent<RectTransform>().rect.height;
        sendContainerHeight = gameObject.GetComponent<RectTransform>().rect.height;
        Debug.Log(galleryHeight);
        Debug.Log(sendContainerHeight);
    }

    private void Update()
    {
        /*if (TouchScreenKeyboard.visible)
        {
            transform.position = position + new Vector2(0, GetKeyboardHeight(true) - galleryHeight - sendContainerHeight);
        } else
        {
            transform.position = position;
        }*/
    }

    public static int GetKeyboardHeight(bool includeInput)
    {
#if UNITY_ANDROID
        using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var unityPlayer = unityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
            var view = unityPlayer.Call<AndroidJavaObject>("getView");
            var dialog = unityPlayer.Get<AndroidJavaObject>("b");

            if (view == null || dialog == null)
                return 0;

            var decorHeight = 0;

            if (includeInput)
            {
                var decorView = dialog.Call<AndroidJavaObject>("getWindow").Call<AndroidJavaObject>("getDecorView");

                if (decorView != null)
                    decorHeight = decorView.Call<int>("getHeight");
            }

            using (var rect = new AndroidJavaObject("android.graphics.Rect"))
            {
                view.Call("getWindowVisibleDisplayFrame", rect);
                return Display.main.systemHeight - rect.Call<int>("height") + decorHeight;
            }
        }
#else
        var height = Mathf.RoundToInt(TouchScreenKeyboard.area.height);
        return height >= Display.main.systemHeight ? 0 : height;
#endif
    }
}
