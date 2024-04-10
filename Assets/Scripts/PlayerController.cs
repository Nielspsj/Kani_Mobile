using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player playerControls;
    public int playerNumber;
    public List<GameObject> cardHand = new List<GameObject>();
    //private List<GameObject> currentCardHand = new List<GameObject>();
    public int trickCount = 0;
    private GameObject lastPlayedCrad;
    private Vector3 cardRotation;
    private int rotZ;
    private int rotY;

    private GameObject cardHandParent;
    private GameObject playedCard;
    public Card_SO playedCard_SO;
    private List<GameObject> playedCards = new List<GameObject>();
    private GameManager gameManager;
    private float cardHeightOnTableIncrement = 0f;
    private bool isDragging = false;
    private Vector3 curScreenPos;
    private bool isPC = false;

    [SerializeField] private List<GameObject> tempCardList = new List<GameObject>();
    private Vector3 worldPos
    {
        get
        {
            float z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(curScreenPos + new Vector3(0, 0, z));
        }
    }

    //Function for automatically sorting the cards in the hand

    //Function to reveal your hand when you start your turn
    //Function to hide your hand when you end your turn

    // Start is called before the first frame update
    void Awake()
    {        
        cardHandParent = new GameObject("cardHandParent");
    }

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        playerControls = new Player();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCardsWithInput();
    }        

    
    //Sort the hand in suits and increased number value
    private void SortHand()
    {
        foreach(GameObject card in cardHand)
        {
            //if card suit club
            //if card suit spade
            //if card suit hearts
            //if card suit hearts

        }
    }

    //Listens to OnCardsDealt event and runs.
    public void PositionRotationCardsInHand()
    {
        //Debug.Log("positionrotation");
        foreach(GameObject card in cardHand)
        {
            //Debug.Log("running");
            //Position
            Vector3 newPosition = transform.position + transform.forward * 0.8f;
            newPosition.y = newPosition.y - 0.35f;
            card.transform.position = newPosition;
            //Create a GameObject to parent the cards under and move in localspace
            //cardHandParent = Instantiate(cardHandParent, newPosition, transform.rotation) as GameObject;
            cardHandParent.transform.position = newPosition;
            cardHandParent.transform.rotation = transform.rotation;
            //Parent all cards under the cardHandParent
            card.transform.parent = cardHandParent.transform;

            //Rotation
            cardRotation.x = -90;
            card.transform.localRotation = Quaternion.Euler(cardRotation);
        }
        SortCardsInHand();
        SpaceCardsInHand();
    }

    //Grab cards in hand and sort them by suit lowest to highest
    private void SortCardsInHand()
    {
        List<int> sortingList = new List<int>();
        

        foreach (GameObject card in cardHand)
        {
            sortingList.Add(card.GetComponent<CardInfo>().cardNr);
        }
        sortingList.Sort();
        for(int i = 0; i < sortingList.Count; i++)
        {
            for(int j = 0; j < cardHand.Count; j++)
            {
                if (sortingList[i] == cardHand[j].GetComponent<CardInfo>().cardNr)
                {
                    tempCardList.Add(cardHand[j]);
                }
            }
        }

        cardHand.Clear();
        foreach (GameObject card in tempCardList)
        {
            cardHand.Add(card);
        }
    }

    //Function to space the cards out in your hand
    public void SpaceCardsInHand()
    {
        //float screenWidth = Screen.width;
        //float handSpace = Mathf.Ceil(screenWidth * 0.8f);
        //Debug.Log("handSpace " + handSpace);
        float handSpace = 2f;
        float distanceBetweenCards = handSpace / cardHand.Count;
        float distanceFromStart = 0f;
        float startingPositionX = -handSpace / 2;
        foreach(GameObject card in cardHand)
        {
            //Debug.Log("spacing");
            //Vector3 newPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 1));
            //newPosition = new Vector3(newPosition.x, card.transform.position.y, card.transform.position.z);
            //card.transform.position = newPosition;
            Vector3 newPosition = card.transform.localPosition;
            newPosition.x = newPosition.x + startingPositionX + distanceFromStart;
            card.transform.localPosition = newPosition;
            distanceFromStart = distanceFromStart + distanceBetweenCards;

            //Tilt cards slightly so they are easier to see in the hand.
            Quaternion newRotation = card.transform.localRotation;
            newRotation.z = newRotation.z - 0.05f;
            card.transform.localRotation = newRotation;
        }
    }

    private void MoveCardsWithInput()
    {        
        RaycastHit raycastHit;
        if (Input.GetMouseButtonDown(0) && GameManager.currentPlayer == playerNumber && isPC == true)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Hit the ui");
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit, 2f))
                {
                    if (raycastHit.transform != null && raycastHit.transform.tag == "Card")
                    {
                        Debug.Log("raycast");
                        //Unparent, reset position and rotation to place on table at 0,0,0.
                        playedCard = raycastHit.transform.gameObject;
                        playedCard.transform.parent = null;
                        Vector3 newPosition = Vector3.zero;
                        playedCard.transform.position = newPosition;
                        Quaternion newRotation = Quaternion.identity;
                        playedCard.transform.rotation = newRotation;                        

                        //Randomly place the cards on the table.
                        newPosition.x = newPosition.x + Random.Range(-0.5f, 0.5f);
                        newPosition.z = newPosition.z + Random.Range(-0.5f, 0.5f);
                        //This will make the card height same regardless of who starts.
                        if (playerNumber == 1)
                        {
                            cardHeightOnTableIncrement = 0.01f;
                        }
                        else if (playerNumber == 2)
                        {
                            cardHeightOnTableIncrement = 0.02f;
                        }
                        else if (playerNumber == 3)
                        {
                            cardHeightOnTableIncrement = 0.03f;
                        }
                        else if (playerNumber == 4)
                        {
                            cardHeightOnTableIncrement = 0.04f;
                        }
                        newPosition.y = newPosition.y + cardHeightOnTableIncrement;
                        playedCard.transform.position = newPosition;

                        //Keep the reference to the playedCard until Trick is done in case the player makes an error.                                        
                        CollectPlayedCards();
                    }
                }
            }            
        }
    }

    private void OnScreenPos(InputValue inputValue)
    {
        curScreenPos = inputValue.Get<Vector2>();
    }
    private void OnPlayCard()
    {
        Debug.Log("OnPlayCard");
        Touch touch = Input.GetTouch(0); // Get the first touch

        RaycastHit raycastHit;
        if (GameManager.currentPlayer == playerNumber)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Hit the ui");
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out raycastHit, 2f))
                {
                    if (raycastHit.transform != null && raycastHit.transform.tag == "Card")
                    {
                        Debug.Log("raycast");
                        playedCard = raycastHit.transform.gameObject;

                        //Start the Drag Coroutine
                        //StartCoroutine(Drag());
                    }
                }
            }
        }
    }

    private IEnumerator Drag()
    {
        
        isDragging = true;
        Vector3 offset = playedCard.transform.position - worldPos;
        while (isDragging)
        {
            //Dragging
            Debug.Log("Dragging");
            playedCard.transform.position = worldPos;
            yield return null;
        }
        //Drop
    }

    private void OnReleaseCard()
    {
        Debug.Log("released the card");
        //Unparent, reset position and rotation to place on table at 0,0,0.
        isDragging = false;
        playedCard.transform.parent = null;
        Vector3 newPosition = Vector3.zero;
        playedCard.transform.position = newPosition;
        Quaternion newRotation = Quaternion.identity;
        playedCard.transform.rotation = newRotation;

        //Randomly place the cards on the table.
        newPosition.x = newPosition.x + Random.Range(-0.5f, 0.5f);
        newPosition.z = newPosition.z + Random.Range(-0.5f, 0.5f);
        //This will make the card height same regardless of who starts.
        if (playerNumber == 1)
        {
            cardHeightOnTableIncrement = 0.01f;
        }
        else if (playerNumber == 2)
        {
            cardHeightOnTableIncrement = 0.02f;
        }
        else if (playerNumber == 3)
        {
            cardHeightOnTableIncrement = 0.03f;
        }
        else if (playerNumber == 4)
        {
            cardHeightOnTableIncrement = 0.04f;
        }
        newPosition.y = newPosition.y + cardHeightOnTableIncrement;
        playedCard.transform.position = newPosition;

        //Keep the reference to the playedCard until Trick is done in case the player makes an error.                                        
        CollectPlayedCards();
    }

    private void CollectPlayedCards()
    {

        //Debug.Log("collecting");
        gameManager.playedCards.Add(playedCard);
        cardHand.Remove(playedCard);        
        GameManager.playedCardsOnTable++;    
    }

    //Clear the played card variable
    public void ClearTrickCards()
    {
        //Debug.Log("clear");
        //gameManager.playedCards.Add(playedCard);
        //playedCards.Clear();
        playedCard = null;        
    }

    //Listen for Trick event to clear the list of playedCards


    //Find out suit and number of playedCard.
    //Store in playedCard_SO.
    private void SavePlayedCard()
    {
        string suit;
        int number;


        //playedCard_SO
    }
}
