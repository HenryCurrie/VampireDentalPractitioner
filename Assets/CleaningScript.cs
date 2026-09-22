using UnityEngine;

public class CleaningScript : MonoBehaviour

{
    public SpriteRenderer dirtySprite;
    public ParticleSystem sparkleEffect; 
    public float cleanSpeed = 0.1f;
    
    private float transparency = 1.0f;
    private bool isClean = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!isClean && other.name.Contains("Toothbrush"))
        {
            Clean();
        }
    }

    void Clean()
    {
        transparency -= Time.deltaTime * cleanSpeed;
        
        
        Color c = dirtySprite.color;
        c.a = transparency;
        dirtySprite.color = c;

        
        if (transparency <= 0 && !isClean)
        {
            FinishCleaning();
        }
    }

    void FinishCleaning()
    {
        isClean = true;
        dirtySprite.enabled = false; 
        
        if (sparkleEffect != null)
        {
            sparkleEffect.Play(); 
        }
        
        Debug.Log("Tooth is shiny!");
    }
    

public float GetCleanProgress()
{
    return 1f - Mathf.Clamp01(transparency);
}



    
}


