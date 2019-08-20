using UnityEngine;
using UnityEngine.Assertions;

[SelectionBase]
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
	private Flip leftFlipper;
	private Flip rightFlipper;

	private const string leftFlipperName = "LeftFlipper";
	private const string rightFlipperName = "RightFlipper";

	#region Unity Methods
	//Functions/Methods
	//access modifier, return datatype, method name, parameters
	private void Awake()
    {
        playerCamera = Camera.main; //Camera.main uses find object of tag internally, super rude.
		leftFlipper = GetFlipper(leftFlipperName);
		Assert.IsNotNull(leftFlipper, "Child: " + leftFlipperName + " wasn´t found!");

		rightFlipper = GetFlipper(rightFlipperName);
		Assert.IsNotNull(rightFlipper, "Child: " + rightFlipperName + " wasn´t found!");

		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
	}

	private void OnDestroy()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void Update()
	{
		float xPosition = playerCamera.ScreenToWorldPoint(Input.mousePosition).x;
		transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);

		leftFlipper.isPressed = Input.GetButton(leftFlipperName);
		rightFlipper.isPressed = Input.GetButton(rightFlipperName);
	}
	#endregion Unity Methods

	private Flip GetFlipper(string flipperName)
	{
		//Transform flipperTransform = transform.Find(flipperName);
		//Assert.IsNotNull(flipperTransform, "Child: " + flipperName + " wasn´t found!");
		//Flip flipper = flipperTransform.GetComponent<Flip>();
		//Assert.IsNotNull(flipper, "Child: " + flipperName + " missing Flipper script!");

		return transform.Find(flipperName)?.GetComponent<Flip>();
	}
}
