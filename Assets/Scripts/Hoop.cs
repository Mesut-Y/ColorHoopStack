using UnityEngine;

public class Hoop : MonoBehaviour
{
    public GameObject _aitOlduguStand;
    public GameObject _aitOlduguCemberSoketi;
    public bool hareketEdebilirmi;
    public string renk;
    public GameManager _gameManager;

    GameObject hareketPozisyonu;
    GameObject aitOlduguStand;

    bool secildi, posDegistir, soketOtur, soketGeriGit;

    public void HareketEt(string islem, GameObject stand = null, GameObject soket = null, GameObject gidilecekObje = null)
    {
        switch(islem)
        {
            case "secim":
                hareketPozisyonu = gidilecekObje;
                secildi = true;
                break;
            case "pozisyondegistir":

                break;
            case "soketeotur":

                break;
            case "soketegerigit":

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( secildi )
        {
            transform.position = Vector3.Lerp(transform.position, hareketPozisyonu.transform.position, .2f); //sliding A -> B
            if (Vector3.Distance(transform.position, hareketPozisyonu.transform.position) < .10) //is object in target?
            {
                secildi = false;
            }
        }
    }
}
