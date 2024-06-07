using UnityEngine;

public class MusicDB : Singleton<MusicDB>
{
    public AudioClip Music_BG, Music_BG2, Music_Shop;
    public AudioClip[] Music_Home;

    public AudioClip Music_Home_Random => Music_Home[Random.Range(0, Music_Home.Length)];

    //public AudioClip Music_InGame, Music_Loading, SFX_button, Music_Lose, Music_Win;
    public AudioClip SFX_popup, SFX_click, SFX_Delete, SFX_win, SFX_Lose, SFX_hit;
}
