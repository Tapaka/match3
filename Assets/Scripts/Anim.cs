
using UnityEngine;
using System.Collections;
 
public class Anim : MonoBehaviour
{

    //refrence for the pause menu panel in the hierarchy
    public GameObject canvas;
    //animator reference
    private Animator animator;
    //variable for checking if the game is paused 
    private bool isPaused = false;
    // Use this for initialization
    public Anim()
    {

    }
    void Start()
    {
        //unpause the game on start
        Time.timeScale = 1;
        //get the animator component
        animator = canvas.GetComponent<Animator>();
        //disable it on start to stop it from playing the default animation
        animator.enabled = false;
    }

    // Update is called once per frame
    public void Update()
    {
        //pause the game on escape key press and when the game is not already paused
        if (Input.GetKeyUp(KeyCode.Escape) && !isPaused)
        {
            loginToCreationCompte();
        }
        //unpause the game if its paused and the escape key is pressed
        else if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
        {
            UnpauseGame();
        }
    }

    //function to pause the game
    public void loginToCreationCompte()
    {
        //enable the animator component
        animator.enabled = true;
        //play the Slidein animation
        animator.Play("NewAnimation");
        //set the isPaused flag to true to indicate that the game is paused
        isPaused = true;
        //freeze the timescale
        Time.timeScale = 1;
    }
    //function to unpause the game
    public void UnpauseGame()
    {
        //set the isPaused flag to false to indicate that the game is not paused
        isPaused = false;
        //play the SlideOut animation
        animator.Play("NewAnimationback");
        //animator.Play("AnimationLoginPanelback");
        //set back the time scale to normal time scale
        Time.timeScale = 1;
    }

}
