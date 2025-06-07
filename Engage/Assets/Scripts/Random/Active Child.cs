//sve child-ove od game object-a postavlja na neaktivne sem onog sa indeksom smestenom u var activeChildIndex

using UnityEngine;

public class ActivateChild : MonoBehaviour
{
    public GameObject[] children; 
    public int activeChildIndex = 0; 
    
    void Start()
    {
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }

        SetActiveChild(activeChildIndex);
    }

    public void SetActiveChild(int index)
    {
        
        if (index >= 0 && index < children.Length)
        {            
            foreach (var child in children)
            {
                child.SetActive(false);
            }
            children[index].SetActive(true);
            Debug.Log(children[index].gameObject.name);
        }
        else
        {
            Debug.LogWarning("Invalid index for child activation!");
        }
    }

}
