using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{

    public int[] DiceVals;
    public int numDice = 4;
    public int DiceTotal;

    public Sprite[] DiceImageOne;
    public Sprite[] DiceImageZero;

    public bool isDoneRolling = false;

    // Start is called before the first frame update
    void Start()
    {
        DiceVals = new int[numDice];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewTurn()
    {
        isDoneRolling = false;
    }

    // function connected to the button in unity UI
    public void RollTheDice()
    {
        // in Ur 4 dice are rolled, where each face has a value of "0" or "1"
        DiceTotal = 0;
        for(int i=0; i<DiceVals.Length; i++)
        {
            DiceVals[i] = Random.Range(0, 2);
            DiceTotal += DiceVals[i];

            //
            if (DiceVals[i] == 0)
            {
                int r = Random.Range(0, DiceImageZero.Length);
                transform.GetChild(i).GetComponent<Image>().sprite = DiceImageZero[r];
            }
            else
            {
                int r = Random.Range(0, DiceImageOne.Length);
                transform.GetChild(i).GetComponent<Image>().sprite = DiceImageOne[r];
            }

        }
        isDoneRolling = true;
    }
}
