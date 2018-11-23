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
   public class player
    {

       //declare variables for player class
        public Texture2D texture,bulletTexture, healthBarTexture,maxHealthBarTexture, heartTexture;
        public Vector2 position,healthBarPosition,maxHealthBarPosition, heartPosition;
        public int speed,playerBulletDamage,playerscore;
        public int health, maxHealth, pSpeed;
        public List<bullet> bulletList;
        public float bulletDelay;
        public bool godMode;
        SoundManager sm = new SoundManager();
        
        

       
        
        


        public Rectangle boundingBox, healthBarRectangle,maxHealthBarRectangle, heartRectangle;

        public player()
        {
            texture = null;
            
            //Set position for player class
            position = new Vector2(0, 400);
            maxHealthBarPosition = new Vector2(100, 30);
            healthBarPosition = new Vector2(100, 30);
            heartPosition = new Vector2(20, 10);

            //Sets player attributes
            speed = 10;
            bulletDelay = 20;
            health = 100;
            maxHealth = 100;
            pSpeed = 20;
            
            
            bulletList = new List<bullet>();
            playerBulletDamage = 6;
            playerscore = 0;
            godMode = false;
            
            

        }

        public void Loadcontent(ContentManager Content)
        {

            //Load textures for player class
            texture = Content.Load<Texture2D>("stickmanv2");
            heartTexture = Content.Load<Texture2D>("heartshape");
            bulletTexture = Content.Load<Texture2D>("playerbullet");
            maxHealthBarTexture = Content.Load<Texture2D>("maxplayerhealthbar");
            healthBarTexture = Content.Load<Texture2D>("healthbar");

            sm.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw texture for player class
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(heartTexture, heartPosition, Color.White);
            spriteBatch.Draw(maxHealthBarTexture, maxHealthBarRectangle, Color.White);
            spriteBatch.Draw(healthBarTexture, healthBarRectangle, Color.White);
            
            //draw a bullet for every bullet in bullet list
            foreach(bullet b in bulletList)
                b.Draw(spriteBatch);
        }

        //Player Controls
        public void Update(GameTime gameTime)
        {

            

            //Set bounding box for player object
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //getting keyboard state
            KeyboardState keyState = Keyboard.GetState();

            //Set Rectangle for health bar
            healthBarRectangle = new Rectangle((int)healthBarPosition.X,(int)healthBarPosition.Y, health*2, 40);
            maxHealthBarRectangle = new Rectangle((int)maxHealthBarPosition.X, (int)maxHealthBarPosition.Y, maxHealth*2, 40);

            //Fire bullet
            if (keyState.IsKeyDown(Keys.Space))
            {
                Shoot();
            }
            UpdateBullet();
            
            //Player controls
            if (keyState.IsKeyDown(Keys.W))
                position.Y = position.Y - speed;

            if (keyState.IsKeyDown(Keys.D))
                position.X = position.X + speed;

            if (keyState.IsKeyDown(Keys.S))
                position.Y = position.Y + speed;

            if (keyState.IsKeyDown(Keys.A))
                position.X = position.X - speed;

            
            //Hacks for health and speed
            if (keyState.IsKeyDown(Keys.D1))
                speed = 50;

            if (keyState.IsKeyDown(Keys.D2))
                godMode = true ;

            if (keyState.IsKeyDown(Keys.D4))
            {
                bulletDelay = 0;
                pSpeed = 80;
            }    


         
               

           

            

            

            


            

            //Does not allow player to leave screen
               
            if (position.X <= 0)
                position.X = 0;
            if (position.X >= 950 - texture.Width)
                position.X = 950 - texture.Width;

            if (position.Y <= 320 - texture.Height)
                position.Y = 320 - texture.Height;
            if (position.Y >= 800 - texture.Height)
                position.Y = 800 - texture.Height;

            
            
            //Leveling up method, Levelling up increases health, maximum health and damage
            switch(playerscore)
       {      
            
                case 20:
            {  
                health = 110;
                maxHealth = 110;
                playerBulletDamage = 8;
                break;
            }

                case 40:
            {
                health = 120;
                maxHealth = 120;
                playerBulletDamage = 10;
                break;
            }

                case 60:
            {
                health = 130;
                maxHealth = 130;
                playerBulletDamage = 12;
                break;
            }

                case 80:
            {
                health = 140;
                maxHealth = 140;
                playerBulletDamage = 14;
                break;
            }
                case 100:
            {
                health = 150;
                maxHealth = 150;
                playerBulletDamage = 16;
                break;
            }
        }

           }

        // Shooting bullet method
        public void Shoot()
        
         //shoot only if delay= 0
    {
        if (bulletDelay >= 0)
        bulletDelay--;

        if(bulletDelay <= 0)
        {
            
            //create new bullet
            bullet newBullet = new bullet(bulletTexture);
            newBullet.position = new Vector2(position.X +110 - newBullet.texture.Width / 2, position.Y +15);


            //make bullet visible
            newBullet.isVisible = true;

            if (bulletList.Count() < 20)
                bulletList.Add(newBullet);

        }

        if (bulletDelay == 0)
            bulletDelay = 15;
        }
            //Update bullet functiojn
        public void UpdateBullet()
            {
                
                //Each bullet will be removed fro screen if it hit right of screen
                foreach (bullet b in bulletList)
                {

                    //Bounding box for bullets in bulletlist
                    b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                    
                    //set speed of bullet
                    b.position.X = b.position.X + pSpeed;

                    //if bullet hit utmost right of scrren, make it invisble
                    if (b.position.X >= 950)
                        b.isVisible = false;
                   
                    }

                //Remove any invisible bullets
                for (int i = 0; i < bulletList.Count; i++)
                {
                    if (!bulletList[i].isVisible)
                    {
                        bulletList.RemoveAt(i);
                        i--;
                    }
                }

                
            
            
            }


       


       


        
        }

    

}






