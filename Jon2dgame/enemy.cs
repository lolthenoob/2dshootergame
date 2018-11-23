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
    public class enemy
    {
        public Texture2D enemyTexture, bulletTexture,enemyHealthTexture, maxHealthTexture;
        public Rectangle boundingBox, enemyHealthBarRectangle, maxHealthBarRectangle;

        public Vector2 position;
        public int speed,enemyBulletDamage;
        public int health,maxHealth;

        public bool isVisible;
        Random rnd = new Random();
        public float ranY, ranX;

        public float bulletDelay;
        public List<bullet> bulletList;







        //contructor
        public enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture, Texture2D newenemyHealthTexture, Texture2D newMaxHealthTexture)
        {

            position = newPosition;
            enemyTexture = newTexture;
            bulletTexture = newBulletTexture;
            enemyHealthTexture = newenemyHealthTexture;
            maxHealthTexture = newMaxHealthTexture;

            health = 30;
            maxHealth = 30;
            
            speed = 4;
            isVisible = true;

            bulletList = new List<bullet>();
            bulletDelay = 100;
            enemyBulletDamage = 10;

            ranX = 950;
            ranY = rnd.Next(0, 800);

        }


        //load content
        public void LoadContent(ContentManager Content)
        {
            enemyTexture = Content.Load<Texture2D>("enemy");
            bulletTexture = Content.Load<Texture2D>("enemybullet");
            enemyHealthTexture = Content.Load<Texture2D>("healthbar");
            maxHealthTexture = Content.Load<Texture2D>("maxplayerhealthbar");



        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(enemyTexture, position, Color.White);
            spriteBatch.Draw(maxHealthTexture, maxHealthBarRectangle, Color.White);
            spriteBatch.Draw(enemyHealthTexture, enemyHealthBarRectangle, Color.White);
            


            foreach (bullet b in bulletList)
                b.Draw(spriteBatch);
        }


        public void Update(GameTime gameTime)
        {

            // Set bounding box for enemy object
            boundingBox = new Rectangle((int)position.X, (int)position.Y, enemyTexture.Width, enemyTexture.Height);

            //Set bounding box for enemy health bar
            enemyHealthBarRectangle = new Rectangle((int)position.X + 15, (int) position.Y -20, health * 2, 10);
            maxHealthBarRectangle = new Rectangle((int)position.X + 15, (int)position.Y - 20, maxHealth * 2, 10);


            // Update movement of enemy
            position.X = position.X - speed;

            // Update shooting method for enemy 
            EnemyShoot();
            UpdateBullet();
            

            // Does not allow enemy to go out of screen 
            if (position.X <= 0 + enemyTexture.Width)
            {
                position.X = 950 - enemyTexture.Width;
                health = 30;
            }

            if (position.Y <= 320 - enemyTexture.Height)
                position.Y = 320 - enemyTexture.Height;
            if (position.Y >= 800 - enemyTexture.Height)
                position.Y = 800 - enemyTexture.Height;

            

        }


        
         public void EnemyShoot()
        {
            if (bulletDelay >= 0)
                bulletDelay--;

            if (bulletDelay <= 0)
            {
                bullet newBullet = new bullet(bulletTexture);
                newBullet.position = new Vector2(position.X  - newBullet.texture.Width / 2, position.Y + 30);


                //make bullet visible
                newBullet.isVisible = true;

                if (bulletList.Count() < 20)
                    bulletList.Add(newBullet);

            }

            if (bulletDelay == 0)
                bulletDelay = 200;
        }
       
        
         
        
        
        //Update bullet function               
         public void UpdateBullet()
         {

             //Each bullet will be removed fro screen if it hit right of screen
             foreach (bullet b in bulletList)
             {
                 //Bounding box for bullets in bulletlist
                 b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                 
                 //set speed of bullet
                 b.position.X = b.position.X - b.espeed;

                 //if bullet hit utmost right of scrren, make it invisble
                 if (b.position.X <= 0)
                     b.isVisible = false;
             }

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



    


