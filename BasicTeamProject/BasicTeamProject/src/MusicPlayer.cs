using System.Media;
using System.Windows;
using System;

namespace BasicTeamProject;

public class MusicPlayer
{
    private SoundPlayer sp;
    //private MediaPlayer.MediaPlayer sp2;
    private string musicPath = Environment.CurrentDirectory + "/Data/BGM/test.wav";
    public MusicPlayer()
    {
        //sp2 = new MediaPlayer.MediaPlayer();
        sp = new SoundPlayer(musicPath);
    }

    public void Play()
    {
        //sp2.Open(musicPath);
        sp.PlayLooping();
    }

    public void Stop()
    {
        
    }
}