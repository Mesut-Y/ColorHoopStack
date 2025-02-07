using UnityEngine;

public class Hoop : MonoBehaviour
{
    public GameObject _aitOlduguStand;
    public GameObject _aitOlduguCemberSoketi;
    public bool hareketEdebilirmi;
    public string renk;
    public GameManager _gameManager;

    GameObject hareketPozisyonu;
    GameObject gidilecekStand;

    bool secildi, posDegistir, soketOtur, soketGeriGit;

    public void HareketEt(string islem, GameObject stand = null, GameObject socket = null, GameObject gidilecekObje = null)
    {
        switch(islem)
        {
            case "secim":
                hareketPozisyonu = gidilecekObje;
                secildi = true;
                break;
            case "pozisyondegistir":
                gidilecekStand = stand;
                _aitOlduguCemberSoketi = socket;
                hareketPozisyonu = gidilecekObje;
                posDegistir = true;
                break;
            case "soketegerigit":
                soketGeriGit = true;
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
        if (posDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, hareketPozisyonu.transform.position, .2f); //sliding A -> B
            if (Vector3.Distance(transform.position, hareketPozisyonu.transform.position) < .10) //is object in target?
            {
                posDegistir = false;
                soketOtur = true;
            }
        }
        if (soketOtur)
        {
            transform.position = Vector3.Lerp(transform.position, _aitOlduguCemberSoketi.transform.position, .2f); //sliding A -> B
            if (Vector3.Distance(transform.position, _aitOlduguCemberSoketi.transform.position) < .10) //is object in target?
            {
                transform.position = _aitOlduguCemberSoketi.transform.position;
                soketOtur = false;

                _aitOlduguStand = gidilecekStand;

                if (_aitOlduguStand.GetComponent<Stand>()._hoops.Count>1)
                {
                    _aitOlduguStand.GetComponent<Stand>()._hoops[^2].GetComponent<Hoop>().hareketEdebilirmi = false;
                }
                _gameManager.isMove = false;
            }
        }

        if (soketGeriGit)
        {
            transform.position = Vector3.Lerp(transform.position, _aitOlduguCemberSoketi.transform.position, .2f); //sliding A -> B
            if (Vector3.Distance(transform.position, _aitOlduguCemberSoketi.transform.position) < .10) //is object in target?
            {
                transform.position = _aitOlduguCemberSoketi.transform.position;
                soketGeriGit = false;
                _gameManager.isMove = false;
            }
        }
    }
}
