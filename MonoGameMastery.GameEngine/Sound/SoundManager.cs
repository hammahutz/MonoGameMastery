using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Audio;

using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.GameEngine.Sound;

public class SoundManager
{
    private int _soundtrackIndex = -1;
    private List<SoundEffectInstance> _soundtracks = new List<SoundEffectInstance>();
    private readonly Dictionary<Type, SoundEffect> _soundBank = new Dictionary<Type, SoundEffect>();

    public void SetSoundTrack(List<SoundEffectInstance> soundtracks)
    {
        _soundtracks = soundtracks;
        _soundtrackIndex = _soundtracks.Count - 1;
    }

    public void RegisterSound(BaseGameStateEvent gameStateEvent, SoundEffect soundEffect) => _soundBank.Add(gameStateEvent.GetType(), soundEffect);

    public void OnNotify(BaseGameStateEvent gameStateEvent)
    {
        if (_soundBank.ContainsKey(gameStateEvent.GetType()))
        {
            _soundBank[gameStateEvent.GetType()].Play();
        }
    }

    public void PlaySoundtrack()
    {
        int nbTracks = _soundtracks.Count;
        if (nbTracks <= 0)
            return;

        var currentTrack = _soundtracks[_soundtrackIndex];
        var nextTrack = _soundtracks[(_soundtrackIndex + 1) % nbTracks];

        if (currentTrack.State == SoundState.Stopped)
        {
            nextTrack.Play();
            _soundtrackIndex++;
            if (_soundtrackIndex >= _soundtracks.Count)
                _soundtrackIndex = 0;
        }
    }
}