using UnityEngine;

using TMPro;

using System.Collections;
using UnityEditor;

public class VampireManager : MonoBehaviour
{
    public GameObject[] vampirePrefabs; 
    public TextMeshProUGUI winText;
    public TextMeshProUGUI scoreText;

public TextMeshProUGUI upgradeButtonText; 
    public int vampiresServed = 0;
    public int dentalGold = 0;
public float scrubPower = 1.0f; 
public int upgradeCost = 20;

public int goldPerPatient = 10;
public int incomeUpgradeCost = 20;

    public Transform spawnPos;  
    public Transform centerPos; 
    public Transform exitPos;   

public TextMeshProUGUI incomeText;

    private GameObject currentVampire;
    private CleaningScript[] teethInCurrentVampire;
    private bool isMovingIn = false;
    private bool isCleaning = false; 

    void Start() 
    {
        SpawnVampire();
    }

    void SpawnVampire()
    {
    
{
    
    int randomIndex = Random.Range(0, vampirePrefabs.Length);
    
    
    currentVampire = Instantiate(vampirePrefabs[randomIndex], spawnPos.position, Quaternion.identity);
    
    teethInCurrentVampire = currentVampire.GetComponentsInChildren<CleaningScript>();
    winText.gameObject.SetActive(false);
    isMovingIn = true;
    isCleaning = false; 
}
    }

   
    void Update()
    {
        if (currentVampire == null) return;

        
        if (isMovingIn)
        {
            currentVampire.transform.position = Vector3.MoveTowards(currentVampire.transform.position, centerPos.position, Time.deltaTime * 5f);
            
            if (Vector3.Distance(currentVampire.transform.position, centerPos.position) < 0.01f) 
            {
                isMovingIn = false;
                isCleaning = true; 
            }
        }

        
        if (isCleaning)
        {
            if (CheckIfAllClean())
            {
                isCleaning = false; 
                StartCoroutine(NextPatient());
            }
        }
    }

    bool CheckIfAllClean()
    {
        if (teethInCurrentVampire == null) return false;
        
        foreach (var tooth in teethInCurrentVampire)
        {
            if (tooth.GetCleanProgress() < 0.95f) return false;
        }
        return true;
    }

    IEnumerator NextPatient()
    {
        
vampiresServed++;
dentalGold += goldPerPatient; 
scoreText.text = "Gold: " + dentalGold;

        winText.gameObject.SetActive(true);
        winText.text = "SPARKLING!";
        
        yield return new WaitForSeconds(1.5f);

        
        while (Vector3.Distance(currentVampire.transform.position, exitPos.position) > 0.1f)
        {
            currentVampire.transform.position = Vector3.MoveTowards(currentVampire.transform.position, exitPos.position, Time.deltaTime * 8f);
            yield return null;
        }

        Destroy(currentVampire);
        SpawnVampire();
        
    }
    public void BuySpeedUpgrade()
{
    if (dentalGold >= upgradeCost)
    {
        dentalGold -= upgradeCost; 
        scrubPower += 0.5f; 
        
        upgradeCost = (int)(upgradeCost * 1.5f);

        upgradeButtonText.text = "Better Brush ($" + upgradeCost + ")";

        
        scoreText.text = "Gold: " + dentalGold;
        
        Debug.Log("Upgrade Bought! New Power: " + scrubPower);
    }
    else
    {
        Debug.Log("Not enough gold!");

    }
}
    

    public void BuyIncomeUpgrade()
{
    if (dentalGold >= incomeUpgradeCost)
    {
        
        dentalGold -= incomeUpgradeCost;

        
        goldPerPatient += 5;

        
        incomeUpgradeCost = (int)(incomeUpgradeCost * 1.8f);

       
        scoreText.text = "Gold: " + dentalGold;
        incomeText.text = "Increase Gold ($" + incomeUpgradeCost + ")";

        Debug.Log("Income Upgraded! New Payout: " + goldPerPatient);
    }
    else
    {
        Debug.Log("Too poor for Premium Treatment!");
    }
}
}


