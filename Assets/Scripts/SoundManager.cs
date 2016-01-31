using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip[] clipList;

	public AudioSource soundSource;
	public AudioSource musicSource;

	public void playSound (string clip) {
		switch (clip) {
		case("power"):
			soundSource.clip = clipList [4];
			break;
		case("clever"):
			soundSource.clip = clipList [5];
			break;
		case("deception"):
			soundSource.clip = clipList [6];
			break;
		case("seduction"):
			soundSource.clip = clipList [7];
			break;
		case("occult"):
			soundSource.clip = clipList [8];
			break;
		}
		soundSource.Play ();
	}
	public void playMusic (string track) {
		switch (track) {
		case("default"):
			musicSource.clip = clipList [0];
			break;
		case("win"):
			musicSource.clip = clipList [1];
			break;
		case("lose"):
			musicSource.clip = clipList [2];
			break;
		case("summon"):
			soundSource.clip = clipList [3];
			break;
		}
		musicSource.Play ();
	}
}
