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
    class spawn
    {

        public Texture2D texture;
        public Vector2 position;
        public bool isVisible;
        public int health;
        public int speed;

         public spawn(Texture2D newtexture)
        {
           
            texture = newtexture;
            isVisible = false;
            health = 3;
            speed = 10;

         
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


    }
}
