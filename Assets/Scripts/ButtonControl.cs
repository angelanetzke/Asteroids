﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {
	public void StartGame() {
		SceneManager.LoadScene("game");
	}

	public void ExitGame() {
		Application.Quit();
	}
}