// stvara se meteor i zakucava se ka zemlji pri koliziji sa bilo cim nestaje u slucaju da je kontakt sa igracem prenosi mu stetu

using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody rg;
    [SerializeField] private float fireBallForce = 10f;
    [SerializeField] private float demage = 10f; 
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        rg.AddForce(Vector3.down * fireBallForce, ForceMode.Impulse);
    }
    // u metodu ispod dodati predaju demedza igracu ako je doslo do kolizije
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            TransferDemage();
            Destroy(gameObject, .05f);
        }
        if (collision != null) { Destroy(gameObject); } 
    }
    private void TransferDemage() { }


}
