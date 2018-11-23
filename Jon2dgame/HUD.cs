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
    public class HUD
    {
        //Declare variables in hud class
        public int playerscore;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePosition, levelUpPosition, yourScorePos, maxHealthPos, DamagePos;
        public bool showHUD;
        public float displayTime;


        public HUD()
        {
            playerscore = 0;
            showHUD = true;
            
            playerScoreFont = null;
            
            //Set sprites position
            playerScorePosition = new Vector2(700, 30); 
            yourScorePos = new Vector2(325, 400);

            levelUpPosition = new Vector2(225, 200);
            maxHealthPos = new Vector2(225, 300);
            DamagePos = new Vector2(225, 400);
            
            

        }

        //load Content
        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("georgia");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw score sprite
            spriteBatch.DrawString(playerScoreFont, " SCORE: " + playerscore, playerScorePosition, Color.Red);


            //Create notification for levelling up
            switch (playerscore)
            {
                case 200:
                    {

                        spriteBatch.DrawString(playerScoreFont, " Levelled up to level 2" , levelUpPosition, Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Max Health Increased" ,maxHealthPos , Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Damage Increased", DamagePos, Color.Red);
                        
                        break;
                    }

                case 400:
                    {
                        spriteBatch.DrawString(playerScoreFont, " Levelled up to level 3", levelUpPosition, Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Max Health Increased", maxHealthPos, Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Damage Increased", DamagePos, Color.Red);

                        break;
                    }

                case 600:
                    {
                        spriteBatch.DrawString(playerScoreFont, " Levelled up to level 4", levelUpPosition, Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Max Health Increased", maxHealthPos, Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Damage Increased", DamagePos, Color.Red);

                        break;
                    }

                case 800:
                    {
                        spriteBatch.DrawString(playerScoreFont, " Levelled up to level 5", levelUpPosition, Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Max Health Increased", maxHealthPos, Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Damage Increased", DamagePos, Color.Red);

                        break;
                    }

                case 1000:
                    {
                         spriteBatch.DrawString(playerScoreFont, " Levelled up to level 6" , levelUpPosition, Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Max Health Increased" ,maxHealthPos , Color.Red);
                        spriteBatch.DrawString(playerScoreFont, "Damage Increased", DamagePos, Color.Red);
                        
                        break;

                    }
            }
            }
    }
}
