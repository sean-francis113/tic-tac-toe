using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace TicTacToe.Managers
{

    /// <summary>
    /// Handles All Audio Processes Such as Crossfading and Fading In/Out
    /// </summary>
    public class AudioManager : MonoBehaviour
    {

        /// <summary>
        /// The Single Static Instance of AudioManager.
        /// </summary>
        public static AudioManager instance;

        [Header("Fade Properties")]
        /// <summary>
        /// The Default Time to Fade From/To Music Channels
        /// </summary>
        [Tooltip("The Default Time to Fade From/To Music Channels")]
        public float DefaultFadeTime;

        [Header("Mixers")]
        /// <summary>
        /// The Main Audio Mixer
        /// </summary>
        [Tooltip("The Main Audio Mixer")]
        public AudioMixer MainMixer;

        [Header("Groups")]
        /// <summary>
        /// The Master Audio Group
        /// </summary>
        [Tooltip("The Master Audio Group")]
        public AudioMixerGroup MainGroup;

        /// <summary>
        /// The Sound Effect Audio Group
        /// </summary>
        [Tooltip("The Sound Effect Audio Group")]
        public AudioMixerGroup SFXGroup;

        /// <summary>
        /// The Music Parent Audio Group.
        /// </summary>
        [Tooltip("The Music Parent Audio Group")]
        public AudioMixerGroup MusicGroup;

        /// <summary>
        /// The Crossfade Channel One Group
        /// </summary>
        [Tooltip("The Crossfade Channel One Group")]
        public AudioMixerGroup CrossfadeOneGroup;

        /// <summary>
        /// The Crossfade Channel Two Group
        /// </summary>
        [Tooltip("The Crossfade Channel Two Group")]
        public AudioMixerGroup CrossfadeTwoGroup;

        [Header("Sources")]
        /// <summary>
        /// The Audio Source That All Sound Effects Are Piped Through
        /// </summary>
        [Tooltip("The Audio Source That All Sound Effects Are Piped Through")]
        public AudioSource SFXSource;

        /// <summary>
        /// The Audio Source That Pipes The Crossfade Channel One
        /// </summary>
        [Tooltip("The Audio Source That Pipes The Crossfade Channel One")]
        public AudioSource CrossfadeOneSource;

        /// <summary>
        /// The Audio Source That Pipes The Crossfade Channel Two
        /// </summary>
        [Tooltip("The Audio Source That Pipes The Crossfade Channel Two")]
        public AudioSource CrossfadeTwoSource;

        [Header("Audio Clips")]
        /// <summary>
        /// Music That Plays On the Main Menu
        /// </summary>
        [Tooltip("Music That Plays On the Main Menu")]
        public AudioClip MainMenuMusic;

        /// <summary>
        /// Music That Plays During Gameplay
        /// </summary>
        [Tooltip("Music That Plays During Gameplay")]
        public AudioClip GameplayMusic;

        /// <summary>
        /// Fanfare That Plays When A Player Wins
        /// </summary>
        [Tooltip("Fanfare That Plays When A Player Wins")]
        public AudioClip EndGameFanfare;

        /// <summary>
        /// Sound Effect That Plays When a Symbol Is Placed
        /// </summary>
        [Tooltip("Sound Effect That Plays When a Symbol Is Placed")]
        public AudioClip SymbolPlaced;

        /// <summary>
        /// Sound Effect That Plays When a Button Is Pressed
        /// </summary>
        [Tooltip("Sound Effect That Plays When a Button Is Pressed")]
        public AudioClip ButtonPressed;

        //The Default Fade Time Set in the Inspector.
        private float OriginalDefaultFadeTime;

        private void Awake()
        {

            //Ensure Single Instance
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

        }

        // Use this for initialization
        void Start()
        {

            //Set the Reset Value
            OriginalDefaultFadeTime = DefaultFadeTime;

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Set the Volume of the Master Mixer Group.
        /// </summary>
        public void SetMasterVolume()
        {

            MainMixer.SetFloat("MasterVolume", UIManager.instance.MasterVolumeSlider.value);

        }

        /// <summary>
        /// Set the Volume of the Music Mixer Group.
        /// </summary>
        public void SetMusicVolume()
        {

            MainMixer.SetFloat("MusicVolume", UIManager.instance.MusicVolumeSlider.value);

        }

        /// <summary>
        /// Set the Volume of the SFX Mixer Group.
        /// </summary>
        public void SetSFXVolume()
        {

            MainMixer.SetFloat("SFXVolume", UIManager.instance.SFXVolumeSlider.value);

        }

        /// <summary>
        /// Immediately Play a Single Sound Effect
        /// </summary>
        /// <param name="SFX">The Audio Clip of the Effect to Play.</param>
        public void PlaySound(AudioClip SFX)
        {

            SFXSource.PlayOneShot(SFX);

        }

        /// <summary>
        /// Set the Default Fade Time to a New Value. 
        /// This Can Be Very Useful To Use With UI Events.
        /// </summary>
        /// <param name="Value">The Value to Set the Default Time To.</param>
        public void SetDefaultFadeTime(float Value)
        {

            DefaultFadeTime = Value;

        }

        /// <summary>
        /// Reset the Default Fade Time to the Original Value Set in the Inspector.
        /// </summary>
        public void ResetDefaultFadeTime()
        {

            DefaultFadeTime = OriginalDefaultFadeTime;

        }

        /// <summary>
        /// Change the Music Playing Immediately.
        /// </summary>
        /// <param name="Music"></param>
        /// <param name="Crossfade"></param>
        public void ChangeMusicImmediately(AudioClip Music)
        {

            //Stop Any Audio Processes
            StopAllCoroutines();

            float ChannelOneVol = 0.0f;
            float ChannelTwoVol = 0.0f;

            MainMixer.GetFloat("CrossfadeOneVolume", out ChannelOneVol);
            MainMixer.GetFloat("CrossfadeTwoVolume", out ChannelTwoVol);

            //Find the Channel That is Currently At Full Volume, If Any
            if(ChannelOneVol >= ChannelTwoVol)
            {

                CrossfadeOneSource.clip = Music;
                MainMixer.SetFloat("CrossfadeOneVolume", 0.0f);
                MainMixer.SetFloat("CrossfadeTwoVolume", -80.0f);
                CrossfadeOneSource.Play();

            }
            else
            {

                CrossfadeTwoSource.clip = Music;
                MainMixer.SetFloat("CrossfadeTwoVolume", 0.0f);
                MainMixer.SetFloat("CrossfadeOneVolume", -80.0f);
                CrossfadeTwoSource.Play();

            }

        }

        /// <summary>
        /// Change the Music Playing Over the Default Time.
        /// </summary>
        /// <param name="Music"></param>
        public void ChangeMusicOverTime(AudioClip Music)
        {

            //Stop Any Audio Processes
            StopAllCoroutines();

            //Start Crossfading Music
            StartCoroutine(CrossfadeMusic(Music, DefaultFadeTime));

        }

        /// <summary>
        /// Change the Music Playing Over a Set Time.
        /// </summary>
        /// <param name="Music"></param>
        /// <param name="FadeTime"></param>
        public void ChangeMusicOverTime(AudioClip Music, float FadeTime)
        {

            //Stop Any Audio Processes
            StopAllCoroutines();

            //Start Crossfading Music
            StartCoroutine(CrossfadeMusic(Music, FadeTime));

        }

        /// <summary>
        /// Fade Out All Music Channels Over the Default Fade Time.
        /// </summary>
        public void FadeOutAll()
        {

            //Stop Any Audio Processes
            StopAllCoroutines();

            //Start Fading Out
            StartCoroutine(FadeOutAllMusic(DefaultFadeTime));

        }

        /// <summary>
        /// Fade Out All Music Channels Over the Passed In Fade Time.
        /// </summary>
        /// <param name="FadeTime">The Time to Fade Out the Channels.</param>
        public void FadeOutAll(float FadeTime)
        {

            //Stop All Audio Processes
            StopAllCoroutines();

            //Start Fading Out
            StartCoroutine(FadeOutAllMusic(FadeTime));

        }

        /// <summary>
        /// Immediately Silence All Music Channels.
        /// </summary>
        public void HaltAllMusic()
        {

            MainMixer.SetFloat("CrossfadeOneVolume", -80.0f);
            MainMixer.SetFloat("CrossfadeTwoVolume", -80.0f);

        }

        /// <summary>
        /// Crossfades Music From One Track to the Next
        /// </summary>
        /// <param name="CrossfadeTo">The Audio Clip of the Music to Crossfade Into.</param>
        /// <returns></returns>
        IEnumerator CrossfadeMusic(AudioClip CrossfadeTo, float FadeTime)
        {

            float ChannelOneVol = 0.0f;
            float ChannelTwoVol = 0.0f;
            float ToLowerVol = 0.0f;
            float ToRaiseVol = 0.0f;

            string ToLowerStr = "";
            string ToRaiseStr = "";

            MainMixer.GetFloat("CrossfadeOneVolume", out ChannelOneVol);
            MainMixer.GetFloat("CrossfadeTwoVolume", out ChannelTwoVol);

            //Choose Which Channel To Lower and Raise
            if(ChannelOneVol >= ChannelTwoVol)
            {

                ToLowerVol = ChannelOneVol;
                ToLowerStr = "CrossfadeOneVolume";
                ToRaiseVol = ChannelTwoVol;
                ToRaiseStr = "CrossfadeTwoVolume";
                CrossfadeTwoSource.clip = CrossfadeTo;
                CrossfadeTwoSource.Play();

            }
            else
            {

                ToLowerVol = ChannelTwoVol;
                ToLowerStr = "CrossfadeTwoVolume";
                ToRaiseVol = ChannelOneVol;
                ToRaiseStr = "CrossfadeOneVolume";
                CrossfadeOneSource.clip = CrossfadeTo;
                CrossfadeOneSource.Play();

            }

            //Need the Distance Needed to Move
            float LowerDifference = 80.0f - ToLowerVol;

            //Since the Upper Attenuation is 0, Just Need to Grab the Absolute Value.
            float RaiseDifference = Mathf.Abs(ToRaiseVol);

            float LowerChangePerTick = LowerDifference * (FadeTime / 10);
            float RaiseChangePerTick = RaiseDifference * (FadeTime / 10);

            //Time to Crossfade Over FadeTime
            while (ToLowerVol > -80.0f && ToRaiseVol < 0.0f)
            {

                //Divide FadeTime By Ten as We Itterate This Coroutine Every Tenth of a Second
                MainMixer.SetFloat(ToLowerStr, ToLowerVol - LowerChangePerTick);
                MainMixer.SetFloat(ToRaiseStr, ToRaiseVol + RaiseChangePerTick);

                Debug.Log(string.Format("Crossfading [ToLower:{0}:{1}, ToRaise:{2}:{3}, ToLowerDifference:{4}, ToRaiseDifference:{5}]",
                    ToLowerStr, ToLowerVol, ToRaiseStr, ToRaiseVol, LowerDifference, RaiseDifference));

                MainMixer.GetFloat(ToLowerStr, out ToLowerVol);
                MainMixer.GetFloat(ToRaiseStr, out ToRaiseVol);

                yield return new WaitForSecondsRealtime(0.1f);

            }

        }

        /// <summary>
        /// Fade Out All Music Channels Over a Set Period of Time.
        /// </summary>
        /// <param name="FadeTime">The Time to Fade Out All Channels</param>
        /// <returns></returns>
        IEnumerator FadeOutAllMusic(float FadeTime)
        {

            float ChannelOneVolume = 0.0f;
            float ChannelTwoVolume = 0.0f;

            MainMixer.GetFloat("CrossfadeOneVolume", out ChannelOneVolume);
            MainMixer.GetFloat("CrossfadeTwoVolume", out ChannelTwoVolume);

            float ChannelOneDifference = 80.0f - ChannelOneVolume;
            float ChannelTwoDifference = 80.0f - ChannelTwoVolume;

            float ChannelOneChangePerTick = ChannelOneDifference - (FadeTime / 10);
            float ChannelTwoChangePerTick = ChannelTwoDifference - (FadeTime / 10);

            //Using || (Or) So The Loop Can Keep Running Until Both Channels Are Silent
            while(ChannelOneVolume > -80.0f || ChannelTwoVolume > -80.0f)
            {

                ChannelOneVolume -= ChannelOneChangePerTick;
                ChannelTwoVolume -= ChannelTwoChangePerTick;

                MainMixer.SetFloat("CrossfadeOneVolume", ChannelOneVolume);
                MainMixer.SetFloat("CrossfadeTwoVolume", ChannelTwoVolume);

                yield return new WaitForSecondsRealtime(0.1f);

            }

        }

    }

}