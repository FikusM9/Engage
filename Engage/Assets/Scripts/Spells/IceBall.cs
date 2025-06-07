//pri sudaru sa igracem zamrzava ga i prenosi mu stetu u koliziji sa bilo cim drugim samo nestaje
// Ice ball pada u smeru ka zemlji

using UnityEditor.UIElements;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    Rigidbody rg;
    private int effectAffectedLayer;
    [SerializeField] MonoBehaviour movementScript;
    [SerializeField] private float IceBallForce = 10f;
    [SerializeField] private float demage = 0f;
    [SerializeField] private float freezingTime = 3f;
    private bool hasHit = false;

    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
        rg.AddForce(Vector3.down*IceBallForce, ForceMode.Impulse);
        effectAffectedLayer = LayerMask.NameToLayer("Player");
    }
    private void Update()
    {
        freezingTime -= Time.deltaTime;
        if (freezingTime <= 0) {
            FrozenPlayer.UnfreezePlayer(movementScript);
            Destroy(gameObject); 
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FrozenPlayer.FreezePlayer(movementScript);
            hasHit = true;
            TransferDemage();
            gameObject.SetActive(false);
        }
        if(collision != null) { gameObject.SetActive(false); }
    }
    public void SetEffectAffectedLayer(string layer) { effectAffectedLayer = LayerMask.NameToLayer(layer); }
    public string GetEffectAffectedLayer() { return LayerMask.LayerToName(effectAffectedLayer); }
    public float GetFreezingTime() { return freezingTime; }
    public void SetFreezingTime(float time) { freezingTime = time; }
    
    public bool HasHit() {  return hasHit; }
    private void TransferDemage() { }
}