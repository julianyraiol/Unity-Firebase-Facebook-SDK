using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PerfilController : MonoBehaviour {

	public Text txtNome;
	public Text txtEmail;
	public GameObject imgUsuario;

	void Awake(){
		
	if (FB.IsLoggedIn) {
			Debug.Log ("FB esta conectado");
			getInformations(FB.IsLoggedIn);
		} else {
			Debug.LogError ("FB n esta conectado");
		}
	}
		
	void AuthCallBack (IResult result)
	{
		if (result.Error!= null){
			Debug.Log(result.Error);
		} else{
			if (FB.IsLoggedIn) {

				Debug.Log ("FB esta conectado");
			} else {
				Debug.LogError  ("FB n esta conectado");
			}
		}
		getInformations(FB.IsLoggedIn);
	}

	void getInformations(bool isLoggedIn){
		if (isLoggedIn) {
			FB.API ("me?fields=name", HttpMethod.GET, DisplayUsername);
			FB.API ("me?fields=email", HttpMethod.GET, DisplayEmail);
			FB.API ("me/picture?square&height=150&width=150", HttpMethod.GET, DisplayPicture);
		} else {
			Debug.LogError ("Erro ao pegar nome do usuário");
		}
	}

	void DisplayUsername(IResult result){
		Debug.Log (AccessToken.CurrentAccessToken);
		Debug.Log (result.ResultDictionary["name"]);
		txtNome.text = ""+ result.ResultDictionary ["name"];
	}

	void DisplayEmail(IResult result){
		txtEmail.text = ""+ result.ResultDictionary ["email"];
	}

	void DisplayPicture(IGraphResult result){
		
		if (result.Error == null && result.Texture != null) {
			Image picUser = imgUsuario.GetComponent<Image> ();
			picUser.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
		} else {
			Debug.LogError (result.Error);
			Debug.LogError ("Não peguei a imagem");
		}
	}
}
