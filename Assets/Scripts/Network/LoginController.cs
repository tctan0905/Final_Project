using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Logging;
using Sfs2X.Util;
using Sfs2X.Core;
using Sfs2X.Entities;

public class LoginController : MonoBehaviour {

	//----------------------------------------------------------
	// Editor public properties
	//----------------------------------------------------------

	[Tooltip("IP address or domain name of the SmartFoxServer 2X instance")]
	public string Host = "127.0.0.1";

	[Tooltip("TCP port listened by the SmartFoxServer 2X instance")]
	public int TcpPort = 9933;

	[Tooltip("UDP port listened by the SmartFoxServer 2X instance")]
	public int UdpPort = 9933;

	[Tooltip("Name of the SmartFoxServer 2X Zone to join")]
	public string Zone = "BasicExamples";

	//----------------------------------------------------------
	// UI elements
	//----------------------------------------------------------

	public InputField nameInput;
	public Button loginButton;
	public Text errorText;

    public GameObject loginPanel;
    public GameObject roomPanel;

    public GameObject roomList;
    public Transform roomListContent;
	//----------------------------------------------------------
	// Private properties
	//----------------------------------------------------------

	private SmartFox sfs;

	//----------------------------------------------------------
	// Unity calback methods
	//----------------------------------------------------------

	void Awake() {
		Application.runInBackground = true;

		// Enable interface
		enableLoginUI(true);

		// Set invert mouse Y option
		//invertMouseToggle.isOn = OptionsManager.InvertMouseY;
	}
	
	// Update is called once per frame
	void Update() {
		if (sfs != null)
			sfs.ProcessEvents();
	}

	//----------------------------------------------------------
	// Public interface methods for UI
	//----------------------------------------------------------

	public void OnLoginButtonClick() {
		enableLoginUI(false);
        if(Host == "127.0.0.1" &&  Zone =="BasicExamples")
        {
            if (sfs == null || !sfs.IsConnected)
            {
                // Set connection parameters
                ConfigData cfg = new ConfigData();
                cfg.Host = Host;
                cfg.Port = TcpPort;
                cfg.Zone = Zone;
                cfg.UdpHost = Host;
                cfg.UdpPort = UdpPort;

                // Initialize SFS2X client and add listeners
                sfs = new SmartFox();

                sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
                sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);

                sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
                sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);

                sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
                sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);
                sfs.AddEventListener(SFSEvent.UDP_INIT, OnUdpInit);
                loginPanel.gameObject.SetActive(false);
                roomPanel.gameObject.SetActive(true);

                // Connect to SFS2X
                sfs.Connect(cfg);
                Debug.Log("Connect");
            }
           
        }
        else
        {
            Debug.Log("Host or Zone Wrong");
        }
		
		
	}

	private void enableLoginUI(bool enable) {
		//nameInput.interactable = enable;
		//loginButton.interactable = enable;
		//errorText.text = "";
	}
	
	private void reset() {
		// Remove SFS2X listeners
		// This should be called when switching scenes, so events from the server do not trigger code in this scene
		sfs.RemoveAllEventListeners();
		
		// Enable interface
		enableLoginUI(true);
        cleanRoomList();
	}

	//----------------------------------------------------------
	// SmartFoxServer event listeners
	//----------------------------------------------------------

	private void OnConnection(BaseEvent evt) {
		if ((bool)evt.Params["success"])
		{
			// Save reference to SmartFox instance; it will be used in the other scenes
			SmartFoxConnection.Connection = sfs;

			Debug.Log("SFS2X API version: " + sfs.Version);
			Debug.Log("Connection mode is: " + sfs.ConnectionMode);

			// Login
			sfs.Send(new Sfs2X.Requests.LoginRequest(nameInput.text));
		}
		else
		{
			// Remove SFS2X listeners and re-enable interface
			reset();

			// Show error message
			Debug.Log("Connection failed");
			errorText.text = "Connection failed";
		}
	}
	
	private void OnConnectionLost(BaseEvent evt) {
		// Remove SFS2X listeners and re-enable interface
		reset();

		//string reason = (string) evt.Params["reason"];

		//if (reason != ClientDisconnectionReason.MANUAL) {
		//	// Show error message
		//	errorText.text = "Connection was lost; reason is: " + reason;
		//}
	}
	
	private void OnLogin(BaseEvent evt) {
		// Initialize UDP communication
		// Host and port have been configured in the ConfigData object passed to the SmartFox.Connect method
		sfs.InitUDP();
        User user = (User)evt.Params["user"];
        Debug.Log("User Name: " + user.Name);
        poupRoomList(sfs.RoomList);
        sfs.Send(new Sfs2X.Requests.JoinRoomRequest("The Lobby"));


    }

    private void OnLoginError(BaseEvent evt) {
		// Disconnect
		sfs.Disconnect();

		// Remove SFS2X listeners and re-enable interface
		reset();
		
		// Show error message
		errorText.text = "Login failed: " + (string) evt.Params["errorMessage"];
	}
	private void poupRoomList(List<Room> rooms)
    {
        cleanRoomList();
        foreach(Room room in rooms)
        {
            Debug.Log("Current room: " + room.Name);
            int roomId = room.Id;
            GameObject newList = Instantiate(roomList) as GameObject;
            RoomId roomitem = newList.GetComponent<RoomId>();
            roomitem.nameLabel.text = room.Name;
            roomitem.roomId = roomId;
            roomitem.bt.onClick.AddListener(() => OnRoomClick(roomId));

            newList.transform.SetParent(roomListContent, false);
        }
    }
    void OnRoomClick(int roomId)
    {
        sfs.Send(new Sfs2X.Requests.JoinRoomRequest(roomId));
        Debug.Log(" Room : " + roomId);
    }
    private void cleanRoomList()
    {
        foreach (Transform child in roomListContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    private void OnUdpInit(BaseEvent evt) {
		//// Remove SFS2X listeners
		//reset();

		//if ((bool)evt.Params["success"]) {
		//	// Set invert mouse Y option
		//	//OptionsManager.InvertMouseY = invertMouseToggle.isOn;

		//	// Load lobby scene
		//	SceneManager.LoadScene("Lobby");
		//} else {
		//	// Disconnect
		//	sfs.Disconnect();
			
		//	// Show error message
		//	errorText.text = "UDP initialization failed: " + (string) evt.Params["errorMessage"];
		//}
	}
    private void OnJoinRoom(BaseEvent evt)
    {

    }
    private void OnJoinRoomError(BaseEvent evt)
    {

    }
 
}
