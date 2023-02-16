using UnityEngine;
using UnityEditor;

public class PlayerPrefsEditor : EditorWindow
{
    private string keyToCheck = "myKey";

    [MenuItem("Window/PlayerPrefs Editor")]
    static void Init()
    {
        PlayerPrefsEditor window = (PlayerPrefsEditor)EditorWindow.GetWindow(typeof(PlayerPrefsEditor));
        window.Show();
    }
    void OnGUI()
    {
        // Get the value associated with the specified key
        string valueToDisplay = PlayerPrefs.GetString(keyToCheck, "");

        // Display the key and its value in labels
        GUILayout.Label("Key: " + keyToCheck);
        GUILayout.Label("Value: " + valueToDisplay);

        // Display a text field for entering a new value for the key
        GUILayout.Label("Enter new value:");

        // Display a text field for entering the key to check
        GUILayout.Label("Enter key to check:");
        keyToCheck = GUILayout.TextField(keyToCheck);
    }
}
