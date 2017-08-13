using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConexaoFacebook : MonoBehaviour {
	
	void Awake(){
		FB.Init (SetInit, OnHideUnity);
	}

	void SetInit(){
		if (FB.IsLoggedIn) {
			Debug.Log ("FB esta conectado");
			SceneManager.LoadScene ("Perfil");
		} else {
			Debug.Log ("FB n esta conectado");
		}
	}

	void OnHideUnity(bool isGameShown){
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void FBLogin(){
		List<string> permissions = new List<string> ();
		permissions.Add ("public_profile");
		FB.LogInWithReadPermissions (permissions, AuthCallBack);
	}

	void AuthCallBack (IResult result)
	{
		if (result.Error!= null){
			Debug.Log(result.Error);
		} else{
			if (FB.IsLoggedIn) {
				
				Debug.Log ("FB esta conectado");
				SceneManager.LoadScene ("Perfil");
			} else {
				Debug.Log ("FB n esta conectado");
			}
		}
	}
}