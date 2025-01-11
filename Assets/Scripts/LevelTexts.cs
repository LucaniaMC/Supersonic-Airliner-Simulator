using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTexts : MonoBehaviour
{
    public Text ingametext;

    bool text1played = false;
    bool text3played = false;
    bool text6played = false;
    bool text7played = false;
    bool text8played = false;

    //Literally hard coded texts. Lmao. I wanted to make a text manager but realized I don't have that many texts for it.
    void Start() 
    {
        ingametext.text = "";
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.name == "Trigger1" & text1played == false)
        {
            StartCoroutine(Text1()); 
            text1played = true;
        }

        if (other.name == "Trigger2" & text6played == false)
        {
            StartCoroutine(Text6()); 
            text6played = true;
        }

        if (other.name == "Trigger3" & text7played == false)
        {
            StartCoroutine(Text7()); 
            text7played = true;
        }

        if (other.name == "Trigger4" & text8played == false)
        {
            StartCoroutine(Text8()); 
            text8played = true;
        }
    }

        void OnTriggerExit2D(Collider2D other) 
    {
        if (other.name == "Trigger1" & text3played == false)
        {
            StopCoroutine(Text1());
            StartCoroutine(Text3()); 
            text3played = true;
        }
    }

    private IEnumerator Text1()
    {
        yield return new WaitForSeconds(1f);
        ingametext.text = "Left click to start the game.";
    }

    private IEnumerator Text3()
    {
        ingametext.text = "";
        yield return new WaitForSeconds(1f);
        ingametext.text = "Hold down left mouse button to fly at supersonic speed.";
        yield return new WaitForSeconds(3f);
        ingametext.text = "";
        StartCoroutine(Text4());
    }

    private IEnumerator Text4()
    {
        yield return new WaitForSeconds(1f);
        ingametext.text = "Supersonic speed consumes less fuel, but generates sonic booms.";
        yield return new WaitForSeconds(3f);
        ingametext.text = "";
        StartCoroutine(Text5());
    }

    private IEnumerator Text5()
    {
        yield return new WaitForSeconds(1f);
        ingametext.text = "Avoid the red no-fly zones.";
        yield return new WaitForSeconds(3f);
        ingametext.text = "";
    }

    private IEnumerator Text6()
    {
        yield return new WaitForSeconds(1f);
        ingametext.text = "Do not hit houses with sonic booms.";
        yield return new WaitForSeconds(3f);
        ingametext.text = "";
    }

    private IEnumerator Text7()
    {
        yield return new WaitForSeconds(1f);
        ingametext.text = "Reach the destination before fuel runs out.";
        yield return new WaitForSeconds(3f);
        ingametext.text = "";
    }

    private IEnumerator Text8()
    {
        yield return new WaitForSeconds(1f);
        ingametext.text = "Avoid hitting the birds.";
        yield return new WaitForSeconds(3f);
        ingametext.text = "";
    }
    
}
