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
    public class battlefield
    {
        public Texture2D texture;
        public Vector2 bfpos1, bfpos2;
        public int speed;
        

        //Constructor
        public battlefield()
        {
            texture = null;
            bfpos1 = new Vector2(950, 0);
            bfpos2 = new Vector2(0, 0);
            speed = 3;
            
        }

        //Load Content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("battlefield");
        }
            
        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
         
            spriteBatch.Draw(texture, bfpos1, Color.White);
            spriteBatch.Draw(texture, bfpos2, Color.White);
        }


        //Update
        public void Update(GameTime gameTime)
        {
            //set speed for background scrolling
            bfpos1.X = bfpos1.X - speed;
            bfpos2.X = bfpos2.X - speed;

            //Scrolling repeating background
            if( bfpos1.X <= 0)
            {
                bfpos1.X = 950;
                bfpos2.X = 0;
            }
               
            
        }
    }
}
