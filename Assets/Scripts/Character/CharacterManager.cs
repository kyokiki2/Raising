using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public Player Player { get { return player; } }
    private List<CharacterBase> charList = new List<CharacterBase>();

    public void Init()
    {
        if(charList.Contains(player) == false)
            AddCharacter(player);

        for (int i = 0; i < charList.Count; ++i)
            charList[i].Init();
    }

    public void AddCharacter(CharacterBase character)
    {
        charList.Add(character);
    }

    public void OnUpdate()
    {
        for(int i = 0; i < charList.Count; ++i)
            charList[i].OnUpdate();
    }

}
