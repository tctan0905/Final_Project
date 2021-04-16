using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("SelectedCharacter", 1);
        characterList = new GameObject[transform.childCount];
        
        for(int i =0;i < transform.childCount;i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }
        foreach(GameObject t in characterList)
        {
            t.SetActive(false);
        }
        if (characterList[index])
            characterList[index].SetActive(true);
    }
    public void ToggleLeft()
    {
        characterList[index].SetActive(false);
        index--;
        if (index < 1)
            index = characterList.Length-1;
        characterList[index].SetActive(true);
    }
    public void ToggleRight()
    {
        characterList[index].SetActive(false);
        index++;
        if (index == characterList.Length)
            index = 1;
        characterList[index].SetActive(true);
    }
    public void ConfirmBtn()
    {
        PlayerPrefs.SetInt("SelectedCharacter", index);
        SceneManager.LoadScene("GameScene");
    }
}
