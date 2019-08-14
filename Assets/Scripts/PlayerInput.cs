using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Data types and variables
    //creating a variable
    //access modifier - data type - variable name

    //access modifiers: public, private, protected
    //if no access modifier it is private

    //public int myValue; //declaring a variable.

    //public int myValue = 10; //declaring and initializing the variable. This is a member variable, beacuse it belongs to a class.
    //public bool isDead = false;

    //private float speed = 10.5f; //10.0f or 10f
    //private double precisionSpeed = 10.5;
    //private string myName = "Jacob";
    //private char myChar = 'J';

    /* Data types
     * int = whole numbers
     * bool = true/false
     * float = decimals  ~7 decimals
     * double = decimals ~15 decimals
     * string = text or a string of character
     * char = one character
     */

    #endregion Data types and variables


    private Camera playerCamera; //Default value null

    //Functions/Methods
    //access modifier, return datatype, method name, parameters
    private void Awake()
    {
        playerCamera = Camera.main; //Camera.main uses find object of tag internally, super rude.
    }


}
