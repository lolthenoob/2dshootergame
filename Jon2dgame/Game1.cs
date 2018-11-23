using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Jon2dgame
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //state enum
        public enum state
        {
            menu,
            playing,
            tutorial,
            credits,
            paused,
            gameOver
        }
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //declaring texture and position of buttons
        public Texture2D menuTexture, playButton, exitButton, tutorialbutton,pauseButton,resumeButton, creditsButton;
        public Vector2 playButtonPos, tutorialButtonPos, exitButtonPos,menuTexturePos, pauseButtonPos, resumeButtonPos, creditsButtonPos,mousePos;

        //declaring texture and position of screens
        public Texture2D tutorialTexture, returnMainTexture, gameOverTexture,creditsTexture;
        public Vector2 ReturnMainPos;

        //Random num genertator
        Random randomNum = new Random();

        


        //list for enemy
        List<enemy> enemyList = new List<enemy>();

        //Initiating background and player objects
        player p = new player();
        battlefield bf = new battlefield();
        HUD hud = new HUD();
        SoundManager sm = new SoundManager();

        //Set MouseState
        public MouseState previousState;

        //Set main menu state
        state gameState = state.menu;





        //Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 950;
            graphics.PreferredBackBufferHeight = 800;
            this.Window.Title = "The Last Soldier";
            Content.RootDirectory = "Content";
            menuTexture = null;

           

            //Setting position of buttons
            playButtonPos = new Vector2(325, 200);
            tutorialButtonPos = new Vector2(325, 325);
            creditsButtonPos = new Vector2(325, 450);
            exitButtonPos = new Vector2(325, 575);

            resumeButtonPos = new Vector2(325, 300);
            pauseButtonPos = new Vector2(450, 10);
            ReturnMainPos = new Vector2(600, 700);
            
            
            //setting position for screens
            menuTexturePos = new Vector2(0, 0);
            
           
            
        }


        protected override void Initialize()
        {
            IsMouseVisible = true;
            
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //load content for HUD
            hud.LoadContent(Content);

            //Load content for game objects
            bf.LoadContent(Content);
            p.Loadcontent(Content);
            sm.LoadContent(Content);
            
            //Loading all buttons
            playButton = Content.Load<Texture2D>("playbutton");
            tutorialbutton = Content.Load<Texture2D>("tutorialbutton");
            exitButton = Content.Load<Texture2D>("exitbutton");
            pauseButton = Content.Load<Texture2D>("pausebutton");
            resumeButton = Content.Load<Texture2D>("resumebutton");
            returnMainTexture = Content.Load<Texture2D>("returnmainmenu");
            creditsButton = Content.Load < Texture2D>("creditsbutton");


            //Loading screen texture
            menuTexture = Content.Load<Texture2D>("titleScreen");
            tutorialTexture = Content.Load<Texture2D>("tutorial");
            gameOverTexture = Content.Load<Texture2D>("gameover");
            creditsTexture = Content.Load<Texture2D>("credits");






            

        }

        //Upload content
        protected override void UnloadContent()
        {


        }


        // Update
        protected override void Update(GameTime gameTime)
        {
            //Allows Game to close
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState mouseState = Mouse.GetState();
            
            //Mouse method
            if (previousState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                mouseClick(mouseState.X, mouseState.Y);
            }

            previousState = mouseState;

            //updating playing state
            switch(gameState)
            {

                case state.playing:
                    {

                        

                        foreach (enemy e in enemyList)
                        {
                            // Check if player bullet collide with enemy, if it collides make player bullet invisible
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (e.boundingBox.Intersects(p.bulletList[i].boundingBox))
                                {
                                    e.health = e.health - p.playerBulletDamage;
                                    p.bulletList.ElementAt(i).isVisible = false;
                                }
                            }

                            //check if enemy bullet hit player, and deduct health. If it collide make enemy bullet inivisble
                            for (int i = 0; i < e.bulletList.Count; i++)
                            {

                                if (p.boundingBox.Intersects(e.bulletList[i].boundingBox) && p.godMode ==false)
                                {
                                    p.health = p.health - e.enemyBulletDamage;
                                    e.bulletList.ElementAt(i).isVisible = false;
                                }
                            }


                            //Check if enemy objects are overlaping each other
                            for (int i = 0; i < enemyList.Count; i++)
                            {
                                if (e.boundingBox.Intersects(e.boundingBox))
                                {

                                }
                            }


                            //Causes enemy to become invisble if bullet have collided with enemy 3 times and increase kill count
                            if (e.health <= 0)
                            {

                                e.isVisible = false;
                                p.playerscore = p.playerscore + 1;
                                hud.playerscore = hud.playerscore + 10;
                                
                            }



                            //For each enemy in enemy list, update it 
                            e.Update(gameTime);
                        }

                        if (p.health <= 0)
                        {
                            MediaPlayer.Play(sm.gameOverMusic);
                            gameState = state.gameOver;
                        }
                        LoadEnemy();

                        hud.Update(gameTime);
                        p.Update(gameTime);
                        bf.Update(gameTime);
                        break;
                    }

            

            //Updating menu state
            case state.menu:
            {
                MediaPlayer.Play(sm.menuMusic);
                Reset();
               
                break;
            }

           

            base.Update(gameTime);
        }

    }

        //draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch(gameState)
            {
                    //Drawing playing state
                case state.playing:
                    {
                        bf.Draw(spriteBatch);
                        p.Draw(spriteBatch);

                        foreach (enemy e in enemyList)
                        {
                            e.Draw(spriteBatch);
                        }
                        
                        hud.Draw(spriteBatch);

                        spriteBatch.Draw(pauseButton, pauseButtonPos, Color.White);

                        break;
                    }


                //Drawing menu state
                case state.menu:
                    {
                        spriteBatch.Draw(menuTexture, menuTexturePos, Color.White);
                        spriteBatch.Draw(playButton, playButtonPos, Color.White);
                        spriteBatch.Draw(tutorialbutton, tutorialButtonPos, Color.White);
                        spriteBatch.Draw(creditsButton, creditsButtonPos, Color.White);
                        spriteBatch.Draw(exitButton, exitButtonPos, Color.White);
                        break;
                    }

                //drawing pause state
                case state.paused:
                    {
                        spriteBatch.Draw(bf.texture, menuTexturePos, Color.White);
                        spriteBatch.Draw(resumeButton, resumeButtonPos, Color.White);
                        spriteBatch.Draw(returnMainTexture, ReturnMainPos, Color.White);
                        break;
                    }
                
                
                    //Drawing game over state
                case state.gameOver:
                    {
                        spriteBatch.Draw(gameOverTexture, menuTexturePos, Color.White);
                        spriteBatch.DrawString(hud.playerScoreFont, " Your SCORE: " + hud.playerscore, hud.yourScorePos, Color.Red);
                        spriteBatch.Draw(returnMainTexture, ReturnMainPos, Color.White);
                        break;
                    }

                    //Drawing tutorial state
                case state.tutorial:
                    {
                        spriteBatch.Draw(tutorialTexture, menuTexturePos, Color.White);
                        spriteBatch.Draw(returnMainTexture, ReturnMainPos, Color.White);
                        break;
                    }

                    //drawing credits state
                case state.credits:
                    {
                        spriteBatch.Draw(creditsTexture, menuTexturePos, Color.White);
                        spriteBatch.Draw(returnMainTexture, ReturnMainPos, Color.White);
                        break;
                    }
            }

            spriteBatch.End();



            base.Draw(gameTime);
        }


        //Load Enemy
        public void LoadEnemy()
        {

            //Create Random number for y axis of enemy and setting its x axis to 950
            int randY = randomNum.Next(0, 800);
            int randX = 950;

           

            //if there are less than  2 enemy is screen, then create more util it is 2 again
            if (enemyList.Count() < 3 )
            {
                enemyList.Add(new enemy(Content.Load<Texture2D>("enemy"), new Vector2(randX, randY), (Content.Load<Texture2D>("enemybullet")), (Content.Load<Texture2D>("healthbar")), (Content.Load<Texture2D>("maxplayerhealthbar"))));
   
            }

            //If any enemy in the screen are not visible(destroyed), then remove them from the list
            for (int i = 0; i < enemyList.Count; i++)
            {
                if(!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }


        }

        //Button operating functions
        public void mouseClick(int x, int y)
        {
            Rectangle mouseClickRectangle = new Rectangle(x, y, 10, 10);

            mousePos = new Vector2(x, y);
              

            //Check Start menu
            switch(gameState)
            {
                case state.menu:
                    {
                        
                        //Set rectangles for buttons in start menu
                        Rectangle playButtonRectangle = new Rectangle((int)playButtonPos.X, (int)playButtonPos.Y, 300, 100);
                        Rectangle tutorialButtonRectangle = new Rectangle((int)tutorialButtonPos.X, (int)tutorialButtonPos.Y, 300, 100);
                        Rectangle creditsButtonRectangle = new Rectangle((int)creditsButtonPos.X,(int) creditsButtonPos.Y, 300, 100);
                        Rectangle exitButtonRectangle = new Rectangle((int)exitButtonPos.X, (int)exitButtonPos.Y, 300, 100);

                        //If player click start button
                        if(mouseClickRectangle.Intersects(playButtonRectangle))
                        {
                            MediaPlayer.Play(sm.backgroundMusic);
                            gameState = state.playing;
                        }

                        //If player click tutorial button
                        if (mouseClickRectangle.Intersects(tutorialButtonRectangle))
                        {
                            //play tutorial music and change state to tutorial
                            MediaPlayer.Play(sm.tutorialMusic);
                            gameState = state.tutorial;
                        }

                        //If player click credits button
                        if (mouseClickRectangle.Intersects(creditsButtonRectangle))
                        {
                            //play credits music and change state to credits
                            MediaPlayer.Play(sm.creditsMusic);
                            gameState = state.credits;
                        }

                        //close the game if player click exit
                        if (mouseClickRectangle.Intersects(exitButtonRectangle))
                        {
                            Exit();
                        }
                        break;
                    }

                //Create rectangles for pause button in playing state
                case state.playing:
                      {
                          Rectangle pauseButtonRectangle = new Rectangle((int)pauseButtonPos.X, (int)pauseButtonPos.Y, 70, 70);
                          if (mouseClickRectangle.Intersects(pauseButtonRectangle))
                          {
                              gameState = state.paused;
                          }

                         
                          
                          
                           break; 
                       }
                      

                    //Create rectnagle for buttons in paused state
                case state.paused:
                      {
                          Rectangle resumeButtonRectangle = new Rectangle((int)resumeButtonPos.X, (int)resumeButtonPos.Y, 300, 100);
                          Rectangle returnMainrectangle = new Rectangle((int)ReturnMainPos.X, (int)ReturnMainPos.Y, 300, 100);
                          
                          
                          if (mouseClickRectangle.Intersects(resumeButtonRectangle))
                          {
                              gameState = state.playing;
                          }

                          if (mouseClickRectangle.Intersects(returnMainrectangle))
                          {
                              MediaPlayer.Play(sm.menuMusic);
                              gameState = state.menu;
                          }
                          break;
                      }

                case state.tutorial:
                      {
                          Rectangle returnmaintutorialRec = new Rectangle((int)ReturnMainPos.X, (int)ReturnMainPos.Y, 300, 100);
                          if (mouseClickRectangle.Intersects(returnmaintutorialRec))
                          {
                              MediaPlayer.Play(sm.menuMusic);
                              gameState = state.menu;
                          }
                          break;
                      }

                case state.gameOver:
                      {
                          
                          Rectangle returnmaintutorialRec = new Rectangle((int)ReturnMainPos.X, (int)ReturnMainPos.Y, 300, 100);
                          if (mouseClickRectangle.Intersects(returnmaintutorialRec))
                          {
                              MediaPlayer.Play(sm.gameOverMusic);
                              gameState = state.menu;
                          }
                          break;
                      }
               
                case state.credits:
                      {
                          Rectangle returnmaintutorialRec = new Rectangle((int)ReturnMainPos.X, (int)ReturnMainPos.Y, 300, 100);
                          if (mouseClickRectangle.Intersects(returnmaintutorialRec))
                          {
                              
                              gameState = state.menu;
                          }
                          break;
                      }
                      
                   
            }
        }

        //Method for returning game to original state after player goes to main menu
        public void Reset()
        {
            foreach(enemy e in enemyList)
            {
                e.health = 30;
                e.position.X = 0;

            }
            p.health = 100;
            hud.playerscore = 0;
            p.playerscore = 0;
            p.playerBulletDamage = 6;


        }

       

       
    }
}
