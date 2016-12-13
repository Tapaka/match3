using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    public Jewel jewel;
    public SpriteRenderer spriteJewel;
    public Position position; 
	// Use this for initialization
	void Start () {
	
	}

    public void AddJewel(Jewel jewel)
    {
        this.jewel = jewel;
        spriteJewel.sprite = jewel.sprite;
    }

    //void OnMouseEnter()
    //{
    //    GetComponent<SpriteRenderer>().color = Color.yellow;    
    //}

    //void OnMouseExit()
    //{
    //    GetComponent<SpriteRenderer>().color = Color.white;  
    //}

   // void OnMouseDown()
   // {
   //     jewel.GetComponent<Animator>().SetTrigger("Bump");
   //     GridBuilder.SelectCell(this);
   // }


    public void Idle()
    {
        jewel.GetComponent<Animator>().SetTrigger("Idle");
    }

    public struct Position
    {
        public int x, y;
    }

    public void Switch(Cell cell)
    {
        //Jewel.JewelType type = this.jewel.type;
        //Sprite sprite = this.jewel.sprite;

        //this.jewel.type = cell.jewel.type;
        //this.spriteJewel.sprite = cell.spriteJewel.sprite;
        //cell.jewel.type = type;
        //cell.spriteJewel.sprite = sprite;
    }
}
