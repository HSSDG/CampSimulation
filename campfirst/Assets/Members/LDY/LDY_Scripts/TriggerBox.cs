using UnityEngine;
using UnityEngine.UI;

public class TriggerBox : MonoBehaviour
{
    public Collider torch;
    public Collider sandBag;
    public GameObject fire;
    public GameObject sand;
    public GameObject arrow;
    public DialogNode_HW dialogNode_torch;
    public DialogNode_HW dialogNode_snadBag;
    private AudioSource soundPlayer;
    public AudioClip fireSound;
    public AudioClip sandSound;

    public GameObject stepOne;
    public GameObject stepTwo;

    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other == null)
        {
            Debug.Log("Collider is null");
            return;
        }

        if (other == torch)
        {
            fire.SetActive(true);
            stepOne.SetActive(true);

            soundPlayer.clip = fireSound;
            soundPlayer.Play();

            arrow.SetActive(false);
            Destroy(torch.gameObject);

            if (dialogNode_torch)
            {
                dialogNode_torch.requiresMissionSuccess = false;
            }
            return;
        }

        if (other == sandBag)
        {
            sand.SetActive(true);
            stepTwo.SetActive(true);

            soundPlayer.clip = sandSound;
            soundPlayer.Play();

            arrow.SetActive(false);
            Destroy(fire);
            Destroy(sandBag.gameObject);
            
            if (dialogNode_snadBag)
            {
                dialogNode_snadBag.requiresMissionSuccess = false;
            }
            return;
        }
    }
}