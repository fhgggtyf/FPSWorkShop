using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameProcedure : MonoBehaviour
{
    public List<GameObject> Enemies;
    public FirstPersonController Player;
    private Vector3 playerPos;
    private Quaternion playerRot;

    public GameObject GGPanel;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Enemies.Capacity; i++)
        {
            Enemies[i].SetActive(true);
        }
        playerPos = Player.transform.position;
        playerRot = Player.transform.rotation;
    }

    public void Restart()
    {
        Player.enabled = true;
        Player.gameObject.GetComponentInChildren<Shoot>().enabled = true;
        Player.MouseLook.lockCursor = true;
        Player.life = 100;
        Player.shield = 100;
        for (int i = 0; i < Enemies.Capacity; i++)
        {
            Enemies[i].SetActive(true);
            Enemies[i].GetComponent<EnemyAI>().life = 100;
        }
        Player.transform.position = playerPos;
        Player.transform.rotation = playerRot;
        GGPanel.SetActive(false);
    }

    public void OnDeath()
    {
        for (int i = 0; i < Enemies.Capacity; i++)
        {
            Enemies[i].SetActive(false);
        }
        GGPanel.SetActive(true);
        Player.MouseLook.SetCursorLock(false);
        Player.enabled = false;
        Player.gameObject.GetComponentInChildren<Shoot>().enabled = false;
    }
}
