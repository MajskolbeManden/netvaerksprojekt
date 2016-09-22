using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace netværksprojekt
{
    public class Button
    {
        bool check;
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        string spriteBlack;
        string spriteRed;
        int width;
        int height;
        public Button(Texture2D newTexture, Vector2 newPosition, int newWidth, int newHeight)
        {
            texture = newTexture;
            position = newPosition;
            check = true;
            width = newWidth;
            height = newHeight;
        }
        
        public bool isClicked;
        public void Update(ContentManager content, MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
             
                if (mouse.LeftButton == ButtonState.Pressed && check == true)
                {
                    isClicked = true;
                    check = false;
                }
            }
            else
            {
             
                isClicked = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
