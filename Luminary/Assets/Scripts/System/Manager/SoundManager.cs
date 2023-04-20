using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    static SoundManager s_Instance;
    public static SoundManager Instance
    {
        get
        {
            // if instance is NULL create instance
            if (!s_Instance)
            {
                s_Instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (s_Instance == null)
                    Debug.Log("instance is NULL_SoundManager");
            }
            return s_Instance;
        }
    }



    public static AudioSource inPlayBGM;
    public AudioSource attackSound;
    public AudioSource fireSkillSoundEffect;
    public AudioSource iceSkillSoundEffect;
    public AudioSource windSkillSoundEffect;
    public AudioSource earthSkillSoundEffect;
    public AudioSource playerHitSound;
    public AudioSource[] mobHitSound;



    // Audio load
    public bool setInPlayBGM(string soundName)
    {
        try
        {
            inPlayBGM.clip = Resources.Load<AudioClip>("Audio/BGM/" + soundName);
        }
        catch(UnityException e)
        {
            Debug.LogError("Failed to load inPlayBGM: " + "Audio/BGM/" + soundName);
            return false;
        }
        return true;
    }

    public bool loadSkillSound(int[] skillClass)
    {
        try
        {
            fireSkillSoundEffect.clip = Resources.Load<AudioClip>("Audio/Skill/" + skillClass[0]);
            iceSkillSoundEffect.clip = Resources.Load<AudioClip>("Audio/Skill/" + skillClass[1]);
            windSkillSoundEffect.clip = Resources.Load<AudioClip>("Audio/Skill/" + skillClass[2]);
            earthSkillSoundEffect.clip = Resources.Load<AudioClip>("Audio/Skill/" + skillClass[3]);
        }
        catch (UnityException e)
        {
            Debug.LogError("Failed to load inPlayBGM: " + "Audio/Skill/" + skillClass);
            return false;
        }
        return true;
        
    }


    //Audio play_BGM
    public void playBGM()
    {
        inPlayBGM.Play();
    }

    //Audio play_Skill
    public void playSkillSound(int skillNum)
    {
        switch (skillNum)
        {
            case 0:
                fireSkillSoundEffect.Play();
                break;
            case 1:
                iceSkillSoundEffect.Play();
                break;
            case 2:
                windSkillSoundEffect.Play();
                break;
            case 3:
                earthSkillSoundEffect.Play();
                break;
            default:
                Debug.Log("An unexpected skill value was entered.");
                break;
        }
    }

}
