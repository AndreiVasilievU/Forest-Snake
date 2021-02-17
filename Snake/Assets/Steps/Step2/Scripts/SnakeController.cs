using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;
    [Range(0,3)]
    public float BonesDistance;
    public GameObject BonePrefab;
    [Range(0,4)]
    public float Speed;
    public float angel;

    public int score = 0;
    public Text scoreGT;
    public Text gameOver;
    public int directionInput;
    private Transform _transform;

    public UnityEvent OnEat;
    public UnityEvent Deathh;
    public Animator anim;
    public Animator anim2;
    public Animator anim3;

    public GameObject parct;
    public int x ;
    bool Death = false;
    public GameObject Panel;
    public GameObject GameOver;
    public GameObject Go;

    public void Awake()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3924151", false);
        }
    }
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "Your Score: 0";

        Panel = GameObject.Find("Panel");
        Panel.SetActive(false);
        anim = new Animator();
        anim = Panel.GetComponent<Animator>();

        GameOver = GameObject.Find("GameOver");
        GameOver.SetActive(false);
        gameOver = GameOver.GetComponentInChildren<Text>();
        anim2 = new Animator();
        anim2 = GameOver.GetComponent<Animator>();

        Go = GameObject.Find("Go");
        Go.SetActive(false);
        anim3 = new Animator();
        anim3 = Go.GetComponent<Animator>();
         
        GameObject parct = GameObject.Find("Particle");   
    }

    private void FixedUpdate()
    {
        MoveSnake(_transform.position + transform.forward * Speed);
        

        if(directionInput < 0) 
        {
             angel = -10f;
            _transform.Rotate(0, angel, 0);
        }
        if(directionInput > 0)
        {
             angel = 10f;
            _transform.Rotate(0, angel, 0);
        }   
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistance = BonesDistance * BonesDistance;
        Vector3 previosPosition = _transform.position;

        foreach (var bone in Tails)
        {
            if((bone.position - previosPosition).sqrMagnitude > sqrDistance)
            {
                var temp = bone.position;
                bone.position = previosPosition;
                previosPosition = temp;
            }
            else
            {
                break;
            }
        }

        _transform.position = newPosition;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);

            Speed += 0.003f;
            score += 1;
            scoreGT.text = "Your Score: "+ score.ToString();

            if(score > HighScore.score)
            {
                HighScore.score = score;
            }

            var bone = Instantiate(BonePrefab);
            Tails.Add(bone.transform);

            if(OnEat != null)
            {
                OnEat.Invoke();
            }
        }
        else if(collision.gameObject.tag == "Box")
        {
            SnakeDeath();
            if(Death == false)
            {
                Panel.SetActive(true);
            }  
            else if(Death == true)
            {
                GameOver.SetActive(true);
            }          
        }
        else if(collision.gameObject.tag == "Bone")
        {
            SnakeDeath();
            if (Death == false)
            {
                Panel.SetActive(true);
            }
            else if(Death == true)
            {
                GameOver.SetActive(true);
            }            
        }
    }

    public void SnakeDeath()
    {
        if(Deathh != null)
        {
            Deathh.Invoke();
        }
        parct.SetActive(true);
        Speed = 0;
        if (x == 0) 
        {
            x++;
            anim.gameObject.SetActive(true);
        }
        else if(Death == true && x == 1)
        {
            anim2.gameObject.SetActive(true);
            gameOver.text = "Your Score: " + score.ToString() +". Tap to Exit";  
        }
    }

    public void Move(int InputAxis)
    {
        directionInput = InputAxis;
    }

    public void RebootSnake()
    {
        anim.gameObject.SetActive(false);
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
        Go.SetActive(true);
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void RReboot()
    {
        Go.SetActive(false);
        parct.SetActive(false);
        gameObject.transform.position = new Vector3(-10, 1, -4);
        Speed = 0.11f;
        
        foreach (var bone in Tails)
        {
            bone.transform.position = new Vector3(100, 0, 0);
        }
        Tails.RemoveRange(3, Tails.Count - 3);
        Death = true;
        x = 1;
    }
}
