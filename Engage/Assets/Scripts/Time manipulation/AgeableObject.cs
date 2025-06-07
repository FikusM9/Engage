using UnityEditor.SceneManagement;
using UnityEngine;

// Makes the object able to go through stages of aging where its visuals are changed.

public class AgeableObject : MonoBehaviour
{
    [SerializeField] private float age = 0f;
    [SerializeField] private float maxAge = 100f;

    // NOTE: Materials can be swapped for meshes or even sprites in case of 2D games

    [System.Serializable]
    private struct AgeMaterialStage
    {
        [SerializeField] private float minAge;
        [SerializeField] private Material material;

        public float GetMinAge()
        {
            return minAge;
        }

        public Material GetMaterial()
        {
            return material;
        }
    }

    [SerializeField] private AgeMaterialStage[] stages;
    private float nextStageTreshold = 0f;
    private Renderer objectRenderer;

    private void Start()
    {
        // Automatically find the Renderer component attached to the same GameObject
        if (objectRenderer == null)
        {
            objectRenderer = GetComponent<Renderer>();
            if (objectRenderer == null)
            {
                Debug.LogWarning("Renderer not found on " + gameObject.name);
            }
        }
    }

    private void Update()
    {
        age += AgeManager.Instance.GetAgingDeltaTime();

        if (age >= maxAge)
        {
            // If the maximum age is exceeded, an OnDeath() method can be called.
            OnDeath();
        }

        if (age > nextStageTreshold) UpdateVisual();
    }

    private void UpdateVisual()
    {
        int stagesLen = stages.Length;
        for (int i = stagesLen - 1; i >= 0; i--) {
            if (age >= stages[i].GetMinAge())
            {
                objectRenderer.material = stages[i].GetMaterial();
                break;
            }
            else nextStageTreshold = stages[i].GetMinAge();
        }
    }

    private void OnDeath()
    {
        // Add death behavior here
    }
}
