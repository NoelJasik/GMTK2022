using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToWin : MonoBehaviour
{
    
    [SerializeField]
    Sprite unPressed;
    [SerializeField]
    Sprite pressed;
    [SerializeField]
    SpriteRenderer sr;
    public int pressValue;
    DiceRolling dr;
    [SerializeField]
    AudioClip press;
    [SerializeField]
    AudioClip unpress;
    void Start()
    {
        dr = FindObjectOfType<DiceRolling>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Player")
    {
        pressValue = 1;
        int pressCounter = 0;
        if(sr.sprite != pressed)
        {
            GetComponent<AudioSource>().PlayOneShot(press);
        }
        sr.sprite = pressed;
        ButtonToWin[] buttons = FindObjectsOfType<ButtonToWin>();
        CancelInvoke();
        for (int i = 0; i < buttons.Length; i++)
        {
            pressCounter += buttons[i].pressValue;
            if(pressCounter >= 2)
            {
                dr.beatLevel(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
   }
    private void OnTriggerExit2D(Collider2D other) {
    if(other.tag == "Player")
    {
        CancelInvoke();
        Invoke("unPress", 1.5f);
    }
   }
   void unPress()
   {
    GetComponent<AudioSource>().PlayOneShot(unpress);
    pressValue = 0;
    sr.sprite = unPressed;
   }
}
