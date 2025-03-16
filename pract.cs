using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpacemanController : MonoBehaviour
{
    public float moveSpeed=10f;
    public float rotationSpeed=5f;
    float mouseX=0;


    CharacterController cc;
    Animator animator;
    AudioSource audioSource;

    int score=0;

    public float GameTime=10f;

    public string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      cc=GetComponent<CharacterController>();
      animator=GetComponent<Animator>();
      audioSource=GetComponent<AudioSource>();
      StartCoroutine("GameTimeCalculation");
    }

    // Update is called once per frame
    void Update()
    {
        float h=Input.GetAxis("Horizontal");
        float v=Input.GetAxis("Vertical");

        if( v<0)
        {
            return;
        }

       Vector3 movement=transform.TransformDirection(0,0,v*moveSpeed); //Calculate movement Velocity
       cc.SimpleMove(movement); //Apply movement velocity to character

       mouseX +=Input.GetAxis("Mouse X")*rotationSpeed; //reading mouseX input
       transform.eulerAngles=new Vector3(0,mouseX,0); //apply mouseX to Y axis

       //Trigger animation
       if(movement.magnitude>0)
       {
        animator.SetBool("IsRunning", true);
       }
       else
       {
         animator.SetBool("IsRunning", false);
       }
       if(GameTime==0)
       {
        SceneManager.LoadScene(sceneName);
       }

    }
    
        private void OnTriggerEnter(Collider other)
     {
        if(other.tag == "Collectibles")
        {
            audioSource.Play();
            other.gameObject.SetActive(false);
            score++;
        }

    }

    IEnumerator GameTimeCalculation()
    {
        while (GameTime >0)
        {
            yield return new WaitForSeconds(1f);
            GameTime = GameTime - 1f;
            if(GameTime==0)
            {
                break;
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label($"Score: {score}");
        GUILayout.Label($"Time Left: {GameTime}");
        
    }
}


//////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prog1 : MonoBehaviour
{
    public GameObject Cube, Sphere, Plane;


    void Update()
    {
        if(Input.GetKey(KeyCode.Q)) Cube.transform.position += new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.W)) Cube.transform.position -= new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.E)) Plane.transform.position += new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.R)) Plane.transform.position -= new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.T)) Sphere.transform.position += new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.Y)) Sphere.transform.position -= new Vector3(0,0,0.05f);

        if(Input.GetKey(KeyCode.A)) Cube.transform.Rotate(1,0,0);
        if(Input.GetKey(KeyCode.S)) Cube.transform.Rotate(-1,0,0);
        if(Input.GetKey(KeyCode.D)) Plane.transform.Rotate(0,1,0);
        if(Input.GetKey(KeyCode.F)) Plane.transform.Rotate(0,1,0);

        if(Input.GetKey(KeyCode.Z)) Cube.transform.localScale += new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.X)) Cube.transform.localScale -= new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.C)) Plane.transform.localScale += new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.V)) Plane.transform.localScale -= new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.B)) Sphere.transform.localScale += new Vector3(0,0,0.05f);
        if(Input.GetKey(KeyCode.N)) Sphere.transform.localScale -= new Vector3(0,0,0.05f);

    }
}
////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Prog2 : MonoBehaviour
{
    public Renderer ObjectRenderer;  // Reference to the Cube's Renderer
    
    public Texture[] textures;  // Array of textures
    public Material[] materials;  // Array of materials

    private int colorIndex = 0;
    private int textureIndex = 0;
    private int materialIndex = 0;

    void Start()
    {
        // Ensure the Renderer is assigned
        if (ObjectRenderer == null)
        {
            ObjectRenderer = GetComponent<Renderer>();
        }
    }

    public void ChangeColor()
    {
       
        ObjectRenderer.material.color = Random.ColorHSV();
    }

    public void ChangeTexture()
    {
        if (textures.Length == 0) return;
        //textureIndex = (textureIndex + 1) % textures.Length;
        ObjectRenderer.material.mainTexture = textures[Random.Range(0,textures.Length)];
    }

    public void ChangeMaterial()
    {
        if (materials.Length == 0) return;
        //materialIndex = (materialIndex + 1) % materials.Length;
        ObjectRenderer.material = materials[Random.Range(0,materials.Length)];
    }
}
//////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prog3 : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Make_JUMP()
    {
        anim.SetTrigger("Jump");
    }
}

////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prog4 : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float rotationSpeed = 100f; // Rotation speed

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the Character Controller
    }

    void Update()
    {
        // Get movement input (Forward/Backward & Left/Right)
        float moveZ = Input.GetAxis("Vertical");  // W & S for forward/backward
        float moveX = Input.GetAxis("Horizontal"); // A & D for left/right

        // Move the player based on input
        Vector3 move = transform.forward * moveZ + transform.right * moveX;
        controller.Move(move * speed * Time.deltaTime);

        // Rotate Left/Right using A & D
        float rotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
}
/////////////////

using UnityEngine;
using UnityEngine.InputSystem;

public class Prog5 : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float distance = 10f;

    RaycastHit2D hit;

    void Start()
    {
        Debug.Log("Press 'Space' to shoot a raycast");
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        hit = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, hit.point, Color.red);
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.right * distance, Color.green);
            Debug.Log("No hit");
        }
    }

}
//////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SpacemanController : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
   public VideoPlayer videoplayer;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
       
    }
    public void MakeJump()
    {
        audioSource.Play();
        animator.SetTrigger("Jump");
        videoplayer.Play();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

