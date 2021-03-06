// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using KiiCorp.Cloud.Storage;

using UnityEngine;
using System;

public class TitlePage : IPage
{
	private MainCamera camera;
	private Rect messageRect = new Rect(0, 0, 640, 64);
	private string message = "(no message)";


	// username
	private Rect usernameRect = new Rect(0, 64, 320, 64);
	private string username = "";

	// password
	private Rect passwordTextRect = new Rect(0, 128, 320, 64);
	private string password = "123456";

	// login
	private Rect loginButtonRect = new Rect(0, 192, 320, 64);

	// signup
	private Rect signupButtonRect = new Rect(0, 256, 320, 64);

	private bool buttonEnable = true;

	private bool menuButtonEnable = false;

	// menu buttons
	private Rect userPageButtonRect = new Rect(320, 128, 320, 64);
	private Rect changePageButtonRect = new Rect(320, 192, 320, 64);
	private Rect findUserPageButtonRect = new Rect(320, 256, 320, 64);
	private Rect groupPageButtonRect = new Rect(320, 320, 320, 64);

	public TitlePage (MainCamera camera)
	{
		this.camera = camera;
	}

	#region IPage implementation
	public void OnGUI ()
	{
		GUI.Label(messageRect, message);
		username = GUI.TextField(usernameRect, username);
		password = GUI.TextField(passwordTextRect, password);

		GUI.enabled = buttonEnable;
		bool loginClicked = GUI.Button(loginButtonRect, "Login");
		bool signupClicked = GUI.Button(signupButtonRect, "Signup");
		GUI.enabled = true;

		if (loginClicked)
		{
			performLogin(username, password);
			return;
		}
		if (signupClicked)
		{
			PerformSignup(username, password);
			return;
		}

		// menu button
		if (!menuButtonEnable) { return; }

		bool userPageClicked = GUI.Button(userPageButtonRect, "User API");
		bool changePageClicked = GUI.Button(changePageButtonRect, "Change API");
		bool findUserPageClicked = GUI.Button(findUserPageButtonRect, "Find User API");
		bool groupPageClicked = GUI.Button(groupPageButtonRect, "Group API");

		if (userPageClicked)
		{
			ShowUserPage();
			return;
		}
		if (changePageClicked)
		{
			ShowChangePage();
			return;
		}
		if (findUserPageClicked)
		{
			ShowFindUserPage();
			return;
		}
		if (groupPageClicked)
		{
			ShowGroupPage();
			return;
		}
	}
	#endregion

	void performLogin (string username, string password)
	{
		message = "Login...";

		SetButtonEnabled(false);

		KiiUser.LogIn(username, password, (KiiUser user, Exception e) => {
			SetButtonEnabled(true);
			if (e != null)
			{
				message = "Login failed " + e.ToString();
				return;
			}
			message = "Login succeeded";
			menuButtonEnable = true;
		});
	}

	void PerformSignup (string username, string password)
	{	
		message = "Signup...";

		SetButtonEnabled(false);

		KiiUser user = null;

		try {
			user = KiiUser.BuilderWithName (username).Build ();
		} catch (Exception e) {
			SetButtonEnabled(true);
			message = "Failed to signup " + e.ToString ();
			return;
		}

		user.Register(password, (KiiUser user2, Exception e) =>
		{
			SetButtonEnabled(true);
			if (e != null)
			{
				message = "Failed to signup " + e.ToString();
				return;
			}
			message = "Signup succeeded";
			menuButtonEnable = true;
		});
	}

	private void SetButtonEnabled(bool value)
	{
		buttonEnable = value;
	}

	#region menu buttons
	void ShowUserPage ()
	{
		camera.PushPage(new UserPage(camera));
	}

	void ShowChangePage ()
	{
		camera.PushPage(new ChangePage(camera));
	}

	void ShowFindUserPage ()
	{
		camera.PushPage(new FindUserPage(camera));
	}

	void ShowGroupPage ()
	{
		camera.PushPage(new GroupPage(camera));
	}
	#endregion
}


