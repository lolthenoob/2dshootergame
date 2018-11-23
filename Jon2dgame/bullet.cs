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
    public class bullet
    {
        //declare bullet variable
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 origin;
        public Vector2 position;
        public int speed, espeed;
        public bool isVisible;
        public float bulletDelay;
        public bool hit;

        public bullet(Texture2D newtexture)
        {
            //set value to bullet attributes
            speed = 20;
            espeed = 12;
            texture = newtexture;
            bulletDelay = 20;

        }

        public void Update(GameTime gametime)
        {
            KeyboardState keyState = Keyboard.GetState();

           
        }
        
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }

   
}
