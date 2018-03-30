﻿#if !(UNITY_4_6 || UNITY_4_7 || UNITY_4_8)
using UnityEngine;
using System.Collections;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{
    
    /// <summary>
    /// Implements sequencer command: AudioWaitOnce(audioClip[, subject[, audioClips...]])
    /// This command will check for an internal lua variable of the entrytag/audioclip name, and 
    /// if it exists/is true, the audio will be skipped. after one playback, the variable
    /// will be created and marked as true.
	/// Contributed by Franklin Kester.
    /// </summary>
    [AddComponentMenu("")] // Hide from menu.
    public class SequencerCommandAudioWaitOnce : SequencerCommand
    {
        static private string _VarPrefix = "once_";
        private float _stopTime = 0;
        private AudioSource _audioSource = null;
        private int _nextClipIndex = 2;
        private AudioClip _currentClip = null;
        private AudioClip _originalClip = null;
		private bool _restoreOriginalClip = false; // Don't restore original; could stop next entry's AudioWait that runs same frame.

        public IEnumerator Start()
        {
            string audioClipName = GetParameter(0);

            Transform subject = GetSubject(1);
            _nextClipIndex = 2;

            if (audioClipName == null || audioClipName.Length < 1)
            {
                if (DialogueDebug.LogWarnings)
                {
                    Debug.LogWarningFormat("{0}: Sequencer: AudioWaitOnce(): no audio clip name given", DialogueDebug.Prefix);
                }
                if (!this.hasNextClip()) { Stop(); }
            }
            
            if (DialogueDebug.LogInfo)
            {
                Debug.LogFormat("{0}: Sequencer: AudioWaitOnce({1})", DialogueDebug.Prefix, GetParameters());
            }

            if (this.hasPlayedAlready(audioClipName))
            {
                if (DialogueDebug.LogInfo)
                {
                    Debug.LogFormat("{0}: Sequencer: AudioWaitOnce(): clip {1} already played, skipping", DialogueDebug.Prefix, audioClipName);
                    if (!this.hasNextClip()) { Stop(); }
                }
            }
            
            _audioSource = SequencerTools.GetAudioSource(subject);
            if (_audioSource == null)
            {
                if (DialogueDebug.LogWarnings)
                {
                    Debug.LogWarningFormat("{0}: Sequencer: AudioWaitOnce(): can't find or add AudioSource to {1}.", DialogueDebug.Prefix, subject.name );
                }
                //  doesn't matter if we have other clips, no audio source means no play
                Stop();
            }
            else
            {
                _originalClip = _audioSource.clip;
                _stopTime = DialogueTime.time + 1; // Give time for yield return null.
                yield return null;
                _originalClip = _audioSource.clip;
                TryAudioClip(audioClipName);
            }
        }

        private void TryAudioClip(string audioClipName)
        {
            try
            {
                AudioClip audioClip = (!string.IsNullOrEmpty(audioClipName)) ? (DialogueManager.LoadAsset(audioClipName) as AudioClip) : null;
                if (audioClip == null)
                {
                    if (DialogueDebug.LogWarnings)
                    {
                        Debug.LogWarningFormat("{0}: Sequencer: AudioWaitOnce(): Clip '{1}' wasn't found.", DialogueDebug.Prefix, audioClipName);
                    }
                }
                else
                {
                    if (IsAudioMuted())
                    {
                        if (DialogueDebug.LogInfo)
                        {
                            Debug.LogFormat("{0}: Sequencer: AudioWaitOnce(): waiting but not playing '{1}'; audio is muted.", DialogueDebug.Prefix, audioClipName);
                        }
                    }
                    else if (this.hasPlayedAlready(audioClipName))
                    {
                        Debug.LogFormat("{0}: Sequencer: AudioWaitOnce(): clip {1} already played, skipping", DialogueDebug.Prefix, audioClipName);
                        _stopTime = DialogueTime.time;
                        //  this prevents stop time from being overwritten below
                        return;
                    }
                    else
                    {
                        if (DialogueDebug.LogInfo)
                        {
                            Debug.LogFormat("{0}: Sequencer: AudioWaitOnce(): playing '{1}'.", DialogueDebug.Prefix, audioClipName);
                        }
                        _currentClip = audioClip;
                        _audioSource.clip = audioClip;
                        _audioSource.Play();
                        this.markAsPlayedAlready(audioClipName);
                    }

                    _stopTime = DialogueTime.time + audioClip.length;
                }
            }
            catch (System.Exception)
            {
                _stopTime = 0;
            }
        }

        /// <summary>
        /// returns a new string prefixed by the internal _VarPrefix, and appended by given audioClipName
        /// </summary>
        /// <param name="audioClipName">audio clip name to use for append</param>
        /// <returns></returns>
        private string buildOnceVarName(string audioClipName)
        {
            return _VarPrefix + audioClipName;
        }

        /// <summary>
        /// checks for an internal lua variable of the given audio clip name to see if it's been played already.
        /// returns true if yes, false if no or not found
        /// </summary>
        /// <param name="audioClipName">audio clip name to test</param>
        /// <returns></returns>
        private bool hasPlayedAlready(string audioClipName)
        {
            return DialogueLua.GetVariable(this.buildOnceVarName(audioClipName)).AsBool;
        }

        /// <summary>
        /// sets the internal variable that dictates the given audio clip was already played
        /// </summary>
        /// <param name="audioClipName"></param>
        private void markAsPlayedAlready(string audioClipName)
        {
            DialogueLua.SetVariable(this.buildOnceVarName(audioClipName), true);
        }

        /// <summary>
        /// checks to see if the next parameter clip index is valid (ie there's another clip waiting to be played)
        /// </summary>
        /// <returns></returns>
        private bool hasNextClip()
        {
            return _nextClipIndex < Parameters.Length;
        }

        public void Update()
        {
            if (DialogueTime.time >= _stopTime)
            {
                if (this.hasNextClip())
                {
                    TryAudioClip(GetParameter(_nextClipIndex));
                    _nextClipIndex++;
                }
                else
                {
                    Stop();
                }
            }
        }

        public void OnDialogueSystemPause()
        {
            if (_audioSource == null) { return; }
            _audioSource.Pause();
        }

        public void OnDialogueSystemUnpause()
        {
            if (_audioSource == null) { return; }
            _audioSource.Play();
        }

        public void OnDestroy()
        {
            if (_audioSource != null)
            {
                if (_audioSource.isPlaying && (_audioSource.clip == _currentClip))
                {
                    _audioSource.Stop();
                }
                if (_restoreOriginalClip) _audioSource.clip = _originalClip;
            }
        }

    }

}
#endif