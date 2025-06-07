// skripte koje se prikacinju na boss-a i podesavaju njegove abilitije
// abilitiji koje poseduje su: fire ball, ice ball, invisibility, ghost
// bice dodato pucanje, poison ball i neka vrsta zlostavljanja igraca

//KOMENTAR SAM ZA SAMOG SEBE treba popraviti invisibility i ghost
using System.Linq; // za if sa any
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Splines.ExtrusionShapes;
//using static UnityEngine.Rendering.HableCurve;

public class Shooting : BossBase
{
    private const int MAXFIREBALLS = 10;

    private Renderer selfRenderer;
    private Collider selfCollider;

    Collider[] playerHits;
    [SerializeField] private LayerMask playerMask;
    //timers
    private float shootTimer;
    private float fireBallTimer;
    private float iceBallTimer;
    private float ghostTimer;
    private float invisibilityTimer;
    //duration
    [SerializeField] private int ghostDuration = 3;
    [SerializeField] private int invisibilityDuration = 5;
    //cooldowns
    [Header("Cooldown range: ")]
    [SerializeField] private float[] shootLimits = new float[2];
    [SerializeField] private float[] fireBallLimits = new float[2];
    [SerializeField] private float[] iceBallLimits = new float[2];
    [SerializeField] private float[] invisibilityLimits = new float[2];
    [SerializeField] private float[] ghostLimits = new float[2];

    private Vector3 playerPosition;
    //spell objects
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject iceBall;

    private GameObject[] fireBalls = new GameObject[MAXFIREBALLS];
    // renderers
    private Vector3 boxSize = new Vector3(25, 7, 25);
    private Vector3 boxCentar;
    CircleRender circle;
    [SerializeField] private Material CircleMaterial;
    private LineRenderer circleRenderer;


    void Awake()
    {
        selfRenderer = GetComponent<Renderer>();
        selfCollider = GetComponent<Collider>();
        boxCentar = transform.position + new Vector3(0f, ((boxSize.y/2)+50.05f), 0f); // boss area
        circle = new CircleRender(10f, 0.1f ,100, CircleMaterial);
        SetupCircleRendered(ref circleRenderer, circle);
    }
    private void Start()
    {
        SetLimits(ref shootLimits, 3, 5);
        SetLimits(ref fireBallLimits, 3, 5);
        SetLimits(ref iceBallLimits, 3, 5);
        SetLimits(ref invisibilityLimits, 5, 10);
        SetLimits(ref ghostLimits, 50, 70);
        shootTimer = Random.Range(shootLimits[0], shootLimits[1]);
        fireBallTimer = Random.Range(iceBallLimits[0], iceBallLimits[1]);
        iceBallTimer = Random.Range(iceBallLimits[0], iceBallLimits[1]);
        invisibilityTimer = Random.Range(invisibilityLimits[0], invisibilityLimits[1]);
        ghostTimer = Random.Range(ghostLimits[0], ghostLimits[1]);

        // istestirati i ulepsati ovo
        DrawCircle(ref circleRenderer, circle, playerPosition);
        circleRenderer.enabled = false;
    }
    void Update()
    {
        Detector(ref playerHits, playerMask);
        ShootingHandler();
        FireBallHandler();
        IceBallHandler();
        InvisibilityHandler();
        GhostHandler();
    }
    void Detector(ref Collider[] hits, LayerMask Mask)
    {
        boxCentar = transform.position + new Vector3(0f, (boxSize.y + 0.05f), 0f);
        hits = Physics.OverlapBox(boxCentar, boxSize, transform.rotation, Mask);
    }
    private void ShootingHandler()
    {
        if(Timer(ref shootTimer, shootLimits[0], shootLimits[1]))
        {
            Shoot();
        }
    }
    private void FireBallHandler()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (fireBalls[i] != null && !fireBalls[i].activeInHierarchy)
            {
                fireBalls[i] = null;  
            }
        }
        //if (fireBalls.Any(fb => fb != null))
        if(fireBalls[0] == null){
            circleRenderer.enabled = false;
            Debug.Log(fireBalls[0]);
        }
        if(Timer(ref fireBallTimer, fireBallLimits[0], fireBallLimits[1]))
        {
            DropFireBall();
            circleRenderer.enabled = true;
        }
    }
    private void IceBallHandler()
    {
        if (!iceBall.activeInHierarchy)
        {
            circleRenderer.enabled = false;
        }
        if (Timer(ref iceBallTimer, iceBallLimits[0], iceBallLimits[1])) {
            DropIceBall();
            circleRenderer.enabled= true;
        }
    }
    private async Task GhostHandler()
    {
        if (Timer(ref ghostTimer, ghostLimits[0], ghostLimits[1])) {
            ToggleGhost();
            await Task.Delay(ghostDuration);
            ToggleGhost();

        } 
    }
    private async Task InvisibilityHandler()
    {
        Debug.Log("K");
        if (Timer(ref invisibilityTimer, invisibilityLimits[0], invisibilityLimits[1]))
        {
            Debug.Log("P");
            ToggleInvisibility();
            await Task.Delay(invisibilityDuration); 
            ToggleInvisibility();
        }
    }
    private void Shoot()
    {
        foreach (Collider col in playerHits)
        { 
            // implementirati Anastasijinu skriptu 
        }
    }
    private void DropFireBall()
    {
        for (int i = 0; i < playerHits.Length; i++)
        {
            fireBalls[i] = Instantiate(fireBall, (playerPosition + Vector3.up * 20f), Quaternion.identity);
        }
    }
    private void DropIceBall() { iceBall =  Instantiate(iceBall, (playerPosition + Vector3.up * 20f), Quaternion.identity); }
    private void ToggleInvisibility() { selfRenderer.enabled = !selfRenderer.enabled;Debug.Log(3); }
    private void ToggleGhost() { selfCollider.enabled = !selfCollider.enabled; }
    private void SetupCircleRendered(ref LineRenderer circleRend, CircleRender circle)
    {
        circleRend = gameObject.AddComponent<LineRenderer>();
        circleRend.positionCount = circle.segments + 1;
        circleRend.widthMultiplier = circle.widthMultiplier;
        circleRend.material = circle.lineMaterial;
        circleRend.useWorldSpace = true;
    }
    void DrawCircle(ref LineRenderer circleRenderer,CircleRender circle, Vector3 circlePosition)
    {
        float angleStep = 360f / circle.segments;
        float angle = 0f;

        for (int i = 0; i <= circle.segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * circle.radius; 
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * circle.radius; 

            circleRenderer.SetPosition(i, circlePosition + new Vector3(x, 0.1f, z));

            angle += angleStep;
        }
    }
    void SetLimits(ref float[] limits, float min, float max) { limits[0] = min; limits[1] = max; }
    struct CircleRender
    {
        public float radius { get; private set; }
        public float widthMultiplier { get; private set; }
        public int segments { get; private set; }
        public Material lineMaterial { get; private set; }
        public CircleRender(float radius, float widthMultiplier, int segments, Material lineMaterial)
        {
            this.radius = radius;
            this.widthMultiplier = widthMultiplier;
            this.segments = segments;
            this.lineMaterial = lineMaterial;

        }
    }
/* debug method
void OnDrawGizmosSelected()
{
    Gizmos.color = Color.green;
    Gizmos.matrix = Matrix4x4.TRS(boxCentar, transform.rotation, Vector3.one);
    Gizmos.DrawWireCube(Vector3.zero, boxSize * 2); 
}
*/
}

/*
    FrozenPlayer: 
    ---    dodati setter za MonoBehaviour script controllPlayer
    ShootingFunctionality:
    ---  prebaciti metodu ShootBullet u static

    ---  dodati da se vektor spawnPos unosi kroz
         argument metode ShootBullet kao i direction 
         od bulleta
 
 
 
 
 */