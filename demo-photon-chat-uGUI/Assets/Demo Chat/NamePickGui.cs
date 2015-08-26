using UnityEngine;


[RequireComponent(typeof(ChatNewGui))]
public class NamePickGui : MonoBehaviour
{
    public Vector2 GuiSize = new Vector2(200, 300);
    public string InputLine = string.Empty;

    private Rect guiCenteredRect;
    private ChatNewGui chatNewComponent;
    private string helpText = "Welcome to the Photon Chat demo.\nEnter a nickname to start. This demo does not require users to authenticate.";
    private const string UserNamePlayerPref = "NamePickUserName";

    public void Start()
    {
        this.guiCenteredRect = new Rect(Screen.width/2-GuiSize.x/2, Screen.height/2-GuiSize.y/2, GuiSize.x, GuiSize.y);
        this.chatNewComponent = this.GetComponent<ChatNewGui>();

        if (this.chatNewComponent != null && this.chatNewComponent.enabled)
        {
            Debug.LogWarning("When using the NamePickGui, ChatNewGui should be disabled initially.");
            
            if (this.chatNewComponent.chatClient != null)
            {
                this.chatNewComponent.chatClient.Disconnect();
            }
            this.chatNewComponent.enabled = false;
        }

        string prefsName = PlayerPrefs.GetString(NamePickGui.UserNamePlayerPref);
        if (!string.IsNullOrEmpty(prefsName))
        {
            this.InputLine = prefsName;
        }
    }
    
    public void OnGUI()
    {
        // Enter-Key handling:
        if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(this.InputLine))
            {
                this.StartChat();
                return;
            }
        }


        GUI.skin.label.wordWrap = true;
        GUILayout.BeginArea(guiCenteredRect);


        if (this.chatNewComponent != null && string.IsNullOrEmpty(this.chatNewComponent.ChatAppId))
        {
            GUILayout.Label("To continue, configure your Chat AppId.\nEnter it in the Chat GUI component (in inspector!) before you continue.\nGet one with the Photon Chat Dashboard.");
            if (GUILayout.Button("Open Chat Dashboard"))
            {
                Application.OpenURL("https://www.exitgames.com/en/Chat/Dashboard");
            }
            GUILayout.EndArea();
            return;
        }

        GUILayout.Label(this.helpText);

        GUILayout.BeginHorizontal();
        GUI.SetNextControlName("NameInput");
        this.InputLine = GUILayout.TextField(this.InputLine);
        if (GUILayout.Button("Connect", GUILayout.ExpandWidth(false)))
        {
            this.StartChat();
        }
        GUILayout.EndHorizontal();

        GUILayout.EndArea();


        GUI.FocusControl("NameInput");
    }

    private void StartChat()
    {
        this.chatNewComponent.UserName = this.InputLine;
        this.chatNewComponent.enabled = true;
        this.enabled = false;

        PlayerPrefs.SetString(NamePickGui.UserNamePlayerPref, this.InputLine);
    }
}
