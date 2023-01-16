using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private TerreinGenerator terreinGenerator;

    private Animator animator;
    public Rigidbody rb; // rigidbody de esta clase
    public Animator sceneTransition; // Animator de la escena (Se puso en esta clase para las animaciones de muerte)
    public ParticleSystem waterParticles; // Particulas de muerte cuando cae al agua.

    public bool pausaActivo = false; // UI de pausa
    private bool estaMuerto = false; // ¿Está muerto el personaje?
    private bool canControl = true; // ¿Se puede controlar al personaje?

    private VidaFlor scriptVidaFlor;
    public GameObject pausa; // GameObject de la UI de pausa
    public GameObject botonPausa; // GameObject dl boton de pausa
    public CameraShake shakecamera; // La cámara
    public GameObject scriptTransformRotationStop; // impide al jugador moverse tras morir.
    public Text scoreText; // GameObject del texto de score
    public Text scoreMaximoText; // GameObject del texto de score maximo

    public int highscore; // Variable de highscore
    public int score; // Variable de score
    public int tiempoDeLevelLoader = 3; // Tiempo que toma cargar el juego
    float maxDistance = 1.0f; // Distancia del Raycast

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
        scriptTransformRotationStop.GetComponent<TransformRotation>().enabled = true;
        scriptVidaFlor = GameObject.FindGameObjectWithTag("FlorUI").GetComponent<VidaFlor>();
        score = 0;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, new Vector3(0, 0, 1) * maxDistance, Color.red, 0.5f);

        if (canControl) // saber si esta muerto
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                float zDifference = 0;
                RaycastHit hitF;
                if (
                    Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hitF, maxDistance)
                )
                {
                    if (hitF.transform.tag == "metro")
                    {
                        animator.SetTrigger("isDeathMetro");
                        StartCoroutine(shakecamera.Shake());
                        dead();
                    }

                    if (hitF.transform.tag == "Obstaculo")
                    {
                        transform.position = transform.localPosition;
                       
                    }
                }
                else if (transform.position.z % 1 != 0)
                {
                    zDifference = Mathf.Round(transform.position.z) - transform.position.z;
                    MoveCharacter(new Vector3(1, 0, 0));
                    score++;
                    scoreText.text = "SCORE: " + score;
                    highscore = score;

                    if (PlayerPrefs.GetInt("HighScore") <= highscore)
                    {
                        PlayerPrefs.SetInt("HighScore", highscore);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                RaycastHit hitF;
                if (
                    Physics.Raycast(transform.position, new Vector3(0, 0, 1), out hitF, maxDistance)
                )
                {
                    if (hitF.transform.tag == "Obstaculo")
                    {
                        transform.position = transform.localPosition;
                        
                    }
                }
                else
                    MoveCharacter(new Vector3(0, 0, 1));
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                RaycastHit hitF;
                if (
                    Physics.Raycast(
                        transform.position,
                        new Vector3(0, 0, -1),
                        out hitF,
                        maxDistance
                    )
                )
                {
                    if (hitF.transform.tag == "Obstaculo")
                    {
                        transform.position = transform.localPosition;
                        
                    }
                }
                else
                    MoveCharacter(new Vector3(0, 0, -1));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                RaycastHit hitF;
                if (
                    Physics.Raycast(
                        transform.position,
                        new Vector3(-1, 0, 0),
                        out hitF,
                        maxDistance
                    )
                )
                {
                    if (hitF.transform.tag == "metro")
                    {
                        animator.SetTrigger("isDeathMetro2");
                        StartCoroutine(shakecamera.Shake());
                        dead();
                    }

                    if (hitF.transform.tag == "Obstaculo")
                    {
                        transform.position = transform.localPosition; // evitar que traspase arboles, rocas, etc..
                    }
                }
                else
                {
                    MoveCharacter(new Vector3(-1, 0, 0));
                    score = score - 1;
                    scoreText.text = "SCORE: " + score;
                }
            }

            // se cierra el canControl
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // Activa el menu de pausa
        {
            StopGamePausaMenu();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Florecita"))
        {
      
            scriptVidaFlor.ColisionFlor();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("water")) //muerte agua
        {
            animator.SetTrigger("isDeathWater");
            Splash();
            dead();
        }

        if (collision.gameObject.tag == "metro") // muerte metro
        {
            animator.SetTrigger("isDeathCar");
            StartCoroutine(shakecamera.Shake());
            dead();
        }

        if (collision.gameObject.tag == ("car")) // muerte carro
        {
            animator.SetTrigger("isDeathCar");
            StartCoroutine(shakecamera.Shake());
            dead();
        }

        if (collision.gameObject.tag == ("PlayerKiller")) // por si se sale del mapa
        {
            dead();
            StartCoroutine(shakecamera.Shake());
        }

        if (collision.collider.GetComponent<Car>() != null)
        {
            if (collision.collider.GetComponent<Car>().isLog)
            {
                transform.parent = collision.transform;
            }
        }
        else
        {
            transform.parent = null;
        }
    }

    private void MoveCharacter(Vector3 diferrence)
    {
        animator.SetTrigger("Hop");
        transform.position = (transform.position + diferrence);
        terreinGenerator.SpawnTerrain(false, transform.position);
    }

    void Splash()
    {
        Instantiate(waterParticles, gameObject.transform.position, Quaternion.identity);
        waterParticles.Play();
    }

    public void dead()
    {
        canControl = false;
        estaMuerto = true;
        rb.isKinematic = true;
        rb.detectCollisions = false;
        scriptTransformRotationStop.GetComponent<TransformRotation>().enabled = false;
        if (estaMuerto == true)
        {
            LoadTheLevel();
        }
    }

    public void StopGamePausaMenu()
    {
        scoreMaximoText.text = "BEST SCORE = " + (PlayerPrefs.GetInt("HighScore"));
        pausa.gameObject.SetActive(!pausa.gameObject.activeSelf);
        botonPausa.gameObject.SetActive(!botonPausa.gameObject.activeSelf);
        if (pausa.gameObject.activeInHierarchy == true)
        {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;
    }

    public void LoadTheLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        sceneTransition.SetTrigger("start");
        yield return new WaitForSecondsRealtime(tiempoDeLevelLoader);
        SceneManager.LoadScene(levelIndex);
    }
}
