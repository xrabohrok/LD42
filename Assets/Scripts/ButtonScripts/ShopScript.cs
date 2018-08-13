using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour {

    //Guns
    public GameObject defaultGun;
    public GameObject shotgunGun;
    public GameObject sniperGun;
    public GameObject globeGun;
    public GameObject currentGun;

    public int bonusHealth;

    public GameObject Roomba;

    //Buttons in inspector
    public Button defaultGunButton;
    public Button shotgunButton;
    public Button sniperGunButton;
    public Button globeGunButton;

    // Use this for initialization
    void Start () {

        //Capture objects
        Button btn1 = defaultGunButton.GetComponent<Button>();
        Button btn2 = shotgunButton.GetComponent<Button>();
        Button btn3 = sniperGunButton.GetComponent<Button>();
        Button btn4 = globeGunButton.GetComponent<Button>();

        //Add Listeners
        btn1.onClick.AddListener(SetGunToDefault);
        btn2.onClick.AddListener(SetGunToShotgun);
        btn3.onClick.AddListener(SetGunToSniper);
        btn4.onClick.AddListener(SetGunToGlobe);

        Roomba = GameObject.Find("Roomba");
        Roomba.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGunToDefault()
    {
        currentGun = defaultGun;
    }
    public void SetGunToShotgun()
    {
        currentGun = shotgunGun;
    }
    public void SetGunToSniper()
    {
        currentGun = sniperGun;
    }
    public void SetGunToGlobe()
    {
        currentGun = globeGun;
    }

    public void openShop()
    {

    }


    public void closeShop()
    {

        SceneManager.LoadScene("RichScene");
        Roomba.GetComponent<PlayerStatus>().ResetPlayer();
        Roomba.SetActive(true);
        Roomba.GetComponent<PlayerStatus>().IsDead = false;
        Roomba.GetComponent<PlayerStatus>().IsEquiped = false;
        Roomba.GetComponent<PlayerStatus>().InGoo = false;
        Roomba.GetComponent<PlayerTopDownMovement>().SetCurrentGun(currentGun);
        Roomba.GetComponent<PlayerTopDownMovement>().Respawn();
        
    }

}
