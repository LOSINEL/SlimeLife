using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    const int playerSoundSize = 3;
    const int weaponSoundSize = 4;
    public static SoundManager instance;
    Slider masterVolume, bgmVolume, sfxVolume;
    AudioSource bgmPlayer, sfxPlayer;
    [SerializeField] AudioClip[] playerSound = new AudioClip[playerSoundSize];
    [SerializeField] AudioClip[] weaponSound = new AudioClip[weaponSoundSize];
    Text masterVolumeText, bgmVolumeText, sfxVolumeText;
    public enum PlayerSoundName
    {
        Move, Jump, WeaponSwing
    }
    public enum WeaponSoundName
    {
        AxeChop, PickAxeChop, ScytheChop, ShovelChop
    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        masterVolume = GameObject.Find("MasterVolume").GetComponent<Slider>();
        bgmVolume = GameObject.Find("BgmVolume").GetComponent<Slider>();
        sfxVolume = GameObject.Find("SfxVolume").GetComponent<Slider>();

        masterVolume.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        bgmVolume.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        sfxVolume.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        bgmPlayer = GameObject.Find("BgmPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SfxPlayer").GetComponent<AudioSource>();

        playerSound[(int)PlayerSoundName.Move] = Resources.Load<AudioClip>("SoundEffect/Player/Slime_Move");
        playerSound[(int)PlayerSoundName.Jump] = Resources.Load<AudioClip>("SoundEffect/Player/Slime_Jump");
        playerSound[(int)PlayerSoundName.WeaponSwing] = Resources.Load<AudioClip>("SoundEffect/Weapon/ETC/WeaponSwing");
        weaponSound[(int)WeaponSoundName.AxeChop] = Resources.Load<AudioClip>("SoundEffect/Weapon/Axe/Axe_Chop");
        weaponSound[(int)WeaponSoundName.PickAxeChop] = Resources.Load<AudioClip>("SoundEffect/Weapon/PickAxe/PickAxe_Chop");
        weaponSound[(int)WeaponSoundName.ScytheChop] = Resources.Load<AudioClip>("SoundEffect/Weapon/Scythe/Scythe_Chop");
        weaponSound[(int)WeaponSoundName.ShovelChop] = Resources.Load<AudioClip>("SoundEffect/Weapon/Shovel/Shovel_Chop");

        ValueChangeCheck();
    }
    public void ValueChangeCheck()
    {
        bgmPlayer.volume = masterVolume.value * bgmVolume.value;
        sfxPlayer.volume = masterVolume.value * sfxVolume.value;
        masterVolume.GetComponentsInChildren<Text>()[1].text = ((int)(masterVolume.value * 100)).ToString() + "%";
        bgmVolume.GetComponentsInChildren<Text>()[1].text = ((int)(bgmVolume.value * 100)).ToString() + "%";
        sfxVolume.GetComponentsInChildren<Text>()[1].text = ((int)(sfxVolume.value * 100)).ToString() + "%";
    }
    public void PlayerSoundPlay(int num, bool isPlayer)
    {
        if(isPlayer)
        {
            sfxPlayer.PlayOneShot(playerSound[num]);
        }else
        {
            sfxPlayer.PlayOneShot(weaponSound[num]);
        }
    }
}