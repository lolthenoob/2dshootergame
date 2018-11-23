using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Jon2dgame
{
    class SoundManager
    {
        public SoundEffect playerShootSound;
        public SoundEffect enemyDeath;
       
        public Song backgroundMusic;
        public Song menuMusic;
        public Song tutorialMusic;
        public Song creditsMusic;
        public Song gameOverMusic;



        public SoundManager()
        {
            playerShootSound = null;
            enemyDeath = null;
            backgroundMusic = null;
            MediaPlayer.IsRepeating = true;
        }

        public void LoadContent(ContentManager Content)
        {
            
            backgroundMusic = Content.Load<Song>("SpiderDanceRemix");
            menuMusic = Content.Load<Song>("Alan Walker - Fade");
            creditsMusic = Content.Load<Song>("Megalovania");
            tutorialMusic = Content.Load<Song>("Alan Walker - Fade");
            gameOverMusic = Content.Load<Song>("Finale");


        }

    }
}
