using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private bool KeepKilling = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            KeepKilling = true;
            StartCoroutine(MakeDamage());
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            KeepKilling = false;
        }
    }

    private IEnumerator MakeDamage()
    {
        while (KeepKilling)
        {
            slider.value -= 1;
            if (slider.value <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            yield return new WaitForSeconds(0.1f);
        }    
    }
}
