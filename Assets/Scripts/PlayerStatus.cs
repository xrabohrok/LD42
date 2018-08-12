using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{

	public int startingHP = 100;
	public int hp = 100;
	private bool isDead = false;

	public bool IsDead
	{
		get { return isDead; }
	}

    public Vector2 playerStartPos;

    //Goo Varaibles
    private float startTime;
	private float curTime;
	public float dmgTick = 2.0f;
	private bool inGoo = false;
	public int gooDmg = 0;

	//end Goo
	
	//GUI
	public Slider batterySilder;
	private Color fullBattery =  new Color(0f,1f,0f,1f);
	private Color highMidBattery =  new Color(1f,1f,0f,1f);
	private Color midBattery =  new Color(1f,.5f,0f,1f);
	private Color lowBattery =  new Color(1f,0f,0f,1f);

	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor =  new Color(1f,0f,0f,0.1f);
	public AudioClip deathSound;
	//end GUI

	// Use this for initialization
	void Start ()
	{
        DontDestroyOnLoad(gameObject);
        playerStartPos = gameObject.transform.position;
        hp = startingHP;
		batterySilder.GetComponentInChildren<Image>().color = fullBattery;
	}
	
	// Update is called once per frame
	void Update()
	{

		if (!isDead)
		{
			//Player Standing in Goo
			if (inGoo)
			{
				curTime = Time.time;
				//Debug.Log("Player Standing in Goo for " + (curTime - startTime) + " seconds");

				if (curTime - startTime > dmgTick)
				{
					TakeDamage(gooDmg);
					ResetTimer();
				}
			}
		}
		else
		{
			
		}

	}

	public void TakeDamage(int dmg)
	{
		hp -= dmg;
		Debug.Log("OW! Health is now at: " + hp);
		batterySilder.value = hp;
		
		if (hp >= 75 )
		{
			batterySilder.GetComponentInChildren<Image>().color = fullBattery;
		}
		else if (hp < 75 && hp >= 50)
		{
			batterySilder.GetComponentInChildren<Image>().color = highMidBattery;
		}
		else if (hp < 50 && hp >= 25)
		{
			batterySilder.GetComponentInChildren<Image>().color = midBattery;
		}
		else if (hp < 25)
		{
			batterySilder.GetComponentInChildren<Image>().color = lowBattery;
		}
		
		if(hp <= 0)
		{
			Death();
		}
	}
	
	public void Death()
	{
		isDead = true;
		EventManager.TriggerEvent("PLAYER_DIED");
		this.GetComponent<PlayerTopDownMovement>().animator.SetBool("dead", true);
		this.GetComponent<PlayerTopDownMovement>().animator.SetFloat("speed", 0.0f);
	}

	public void Cleanup()
	{
		//Destroy(gameObject);
	}
	
	private void ResetTimer()
	{
		startTime = Time.time;
		curTime = 0.0f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			Debug.Log("Player Entered Goo");
			gooDmg = other.GetComponent<GooScript>().dmg;
			inGoo = true;
			ResetTimer();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			Debug.Log("Player Left Goo");
			inGoo = false;
			gooDmg = 0;
			ResetTimer();
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			Debug.Log("Player Standing in Goo");
			gooDmg = other.GetComponent<GooScript>().dmg;
			inGoo = true;

		}
	}
    
    public void GrantBonusHealth(int bonusHealth)
    {
        hp += bonusHealth;
    }
	
    //Run when leaving the shop and returning to the game
    public void ResetPlayer()
    {
        hp = startingHP;
        isDead = false;
        gameObject.transform.position = playerStartPos;


    }
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.tag == "Enemy" && !IsDead)
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyTopDownMovement>().bumpDamage);
        }
    }
    
}
