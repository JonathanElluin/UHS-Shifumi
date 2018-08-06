using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class positionPlayer : MonoBehaviour
{
    public Sprite coucherA1;
    public Sprite deboutA1;
    public Sprite reloadA1;

    public Sprite coucherB1;
    public Sprite deboutB1;
    public Sprite reloadB1;

    private Sprite choixA1Sprite;
    private Sprite choixB1Sprite;

    private string choixA1 = "debout";
    private string choixB1 = "debout";

    private GameObject spriteRendererA1;
    private GameObject spriteRendererB1;

    private GameObject chargeurA;
    private GameObject chargeurB;

    private int ballesA1 = 6;
    private int ballesB1 = 6;

    private float time = 10;

    private int viesA1 = 2;
    private int viesB1 = 2;

    private int chargeurA1 = 6;
    private int chargeurB1 = 6;

    private float timer = 10;

    public Sprite[] cartouches;

    public GameObject[] lifeA1;
    public GameObject[] lifeB1;

    private GUIStyle guiStyle = new GUIStyle();
    void Start()
    {
        InvokeRepeating("compteur", 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeTheDamnSprite(coucherA1, "A1", "coucher");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeTheDamnSprite(deboutA1, "A1", "debout");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeTheDamnSprite(reloadA1, "A1", "reload");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeTheDamnSprite(coucherB1, "B1", "coucher");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeTheDamnSprite(deboutB1, "B1", "debout");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeTheDamnSprite(reloadB1, "B1", "reload");
        }
    }

    private void ChangeTheDamnSprite(Sprite sprite, string joueur, string choix)
    {
        if(joueur == "A1")
        {
            choixA1Sprite = sprite;
            choixA1 = choix;

        }
        if (joueur == "B1")
        {
            choixB1Sprite = sprite;
            choixB1 = choix;
        }
    }

    private void compteur()
    {
        if(timer <= 0)
        {
            spriteRendererA1 = GameObject.Find("A_01");
            spriteRendererB1 = GameObject.Find("B_01");
            if(choixA1Sprite != null)
            {
                spriteRendererA1.GetComponent<SpriteRenderer>().sprite = choixA1Sprite;
            }
            if (choixB1Sprite != null)
            {
                spriteRendererB1.GetComponent<SpriteRenderer>().sprite = choixB1Sprite;
            }
            if (!endOfGame(viesA1, viesB1))
            {
                actions(choixA1, chargeurA1, choixB1, "A1");
                actions(choixB1, chargeurB1, choixA1, "B1");
                timer = 10;
            }
        }
        else
        {
            timer -= 1;
        }

    }

    void OnGUI()
    {
        guiStyle.fontSize = 40;
        guiStyle.normal.textColor = Color.red;
        GUI.Label(new Rect(575, 75, 500, 500), timer.ToString(), guiStyle);
    }

    private void shootB1()
    {
        chargeurB1--;
        changeSpriteShootB1(chargeurB1);
    }

    private void shootA1()
    {
        chargeurA1--;
        changeSpriteShootA1(chargeurA1);
    }

    private void rechargeA1()
    {
        chargeurA1 = 6;
        changeSpriteShootA1(chargeurA1);
    }

    private void rechargeB1()
    {
        chargeurB1 = 6;
        changeSpriteShootB1(chargeurB1);
    }

    private void changeSpriteShootA1(int number)
    {
        chargeurA = GameObject.Find("cylinder_A1");
        chargeurA.GetComponent<SpriteRenderer>().sprite = cartouches[number];
    }

    private void changeSpriteShootB1(int number)
    {
        chargeurB = GameObject.Find("cylinder_B1");
        chargeurB.GetComponent<SpriteRenderer>().sprite = cartouches[number];
    }

    private void destroyLifeA1()
    {
        Destroy(lifeA1[viesA1]);
        viesA1--;
    }

    private void destroyLifeB1()
    {
        Destroy(lifeB1[viesB1]);
        viesB1--;
    }

    private bool canShoot(int balles)
    {
        return balles > 0;
    }

    private void actions(string choice, int chargeur, string joueurB, string joueur)
    {
        switch (choice)
        {
            case "debout":
                if (canShoot(chargeur) && joueurB != "coucher")
                {
                    if(joueur == "A1")
                    {
                        shootA1();
                        destroyLifeB1();
                    }
                    else
                    {
                        shootB1();
                        destroyLifeA1();
                    }
                }
                break;
            case "reload":
                if (joueur == "A1")
                {
                    rechargeA1();
                }
                else
                {
                    rechargeB1();
                }
                break;
            default:
                break;
        }
    }

    private bool endOfGame(int vieA, int vieB)
    {
        return vieA == 0 || vieB == 0;
    }
}
