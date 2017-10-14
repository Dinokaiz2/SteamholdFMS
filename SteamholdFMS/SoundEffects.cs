using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;


namespace SteamholdFMS
{
    public class SoundEffects
    {
        List<SoundEffectInstance> queue = new List<SoundEffectInstance>();
        static SoundEffect autoStart;
        static SoundEffect teleopStart;
        static SoundEffect endgameStart;
        static SoundEffect matchEnd;
        static SoundEffect faultSound;
        public static void Load(ContentManager content)
        {
            //ContentManager soundEffectManager = new ContentManager(serviceProvider, @"Content");
            
            //endgameStart = soundEffectManager.Load<SoundEffect>("Start of End Game_normalized.wav");
            //matchEnd = soundEffectManager.Load<SoundEffect>("Match End_normalized.wav");
            //faultSound = soundEffectManager.Load<SoundEffect>("Match Pause_normalized.wav");
            //autoStart = soundEffectManager.Load<SoundEffect>("Start Auto_normalized.wav");
            //teleopStart = soundEffectManager.Load<SoundEffect>("Start Teleop_normalized.wav");

            endgameStart = content.Load<SoundEffect>("Start of End Game_normalized");
            matchEnd = content.Load<SoundEffect>("Match End_normalized");
            faultSound = content.Load<SoundEffect>("Match Pause_normalized");
            autoStart = content.Load<SoundEffect>("Start Auto_normalized");
            teleopStart = content.Load<SoundEffect>("Start Teleop_normalized");
        }
        public void addToQueue(String sound)
        {
            if (sound.Equals("autoStart"))
            {
                queue.Add(autoStart.CreateInstance());
            } else if(sound.Equals("teleopStart"))
            {
                queue.Add(teleopStart.CreateInstance());
            } else if(sound.Equals("endgameStart"))
            {
                queue.Add(endgameStart.CreateInstance());
            } else if(sound.Equals("matchEnd"))
            {
                queue.Add(matchEnd.CreateInstance());
            } else if(sound.Equals("faultSound"))
            {
                queue.Add(faultSound.CreateInstance());
            }
        }
        public void Play()
        {
            for (int i = 0; i < queue.Count; i++)
            {
                queue[i].Play();
                queue.RemoveAt(i);
            }
        }
    }
}

