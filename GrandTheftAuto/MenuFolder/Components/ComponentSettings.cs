using System.Linq;
using GrandTheftAuto.MenuFolder.Classes;
using Menu.MenuFolder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.MenuFolder.Components
{
    public class ComponentSettings : DrawableGameComponent
    {
        private GameClass game;
        private SettingValues values;
        private IMenu settings;
        private int index;      //pomocn� prom�nn�
        private bool IsResolutionChanged;
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="componentGameMenu"></param>
        public ComponentSettings(GameClass game)
            : base(game)
        {
            this.game = game;
            IsResolutionChanged = false;
        }
        /// <summary>
        /// Initialiaze method implemets from IInitializable
        /// </summary>
        public override void Initialize()
        {
            settings = new MenuItems(game,new Vector2(100,game.graphics.PreferredBackBufferHeight/3),game.normalFont);
            values = new SettingValues(game);
            for (int i = 0; i<values.GetResolutionList().Count;i++)
            {
                if (values.GetResolutionList()[i].Width == game.graphics.PreferredBackBufferWidth &&values.GetResolutionList()[i].Height == game.graphics.PreferredBackBufferHeight)
                {
                    index = i;
                    break;
                }
            }
            settings.AddItem("Display mode:",values.IsFullScreen());
            settings.AddItem("Resolution:",values.GetResolution(index));
            settings.AddItem("Back");
            settings.Selected = settings.Items.First();
            base.Initialize();
        }
        /// <summary>
        /// Updatable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            settings.Moving(Keys.W, Keys.S);
            if (game.SingleClick(Keys.Enter) || (game.SingleClickMouse() && settings.CursorColision()))
            {
                //D�lej n�co p�i zm��knut� enter na ur�it�m m�st�
                switch (settings.Selected.Text)
                {
                    case "Display mode:":
                        game.graphics.IsFullScreen = !game.graphics.IsFullScreen;
                        game.graphics.ApplyChanges();
                        settings.UpdateItem("Display mode:",0,values.IsFullScreen());
                        break;
                    case "Resolution:":
                        //index = index < values.GetResolutionList().Count-1 ? index++ : index = 0;
                        if (index < values.GetResolutionList().Count - 1)
                            index++;
                        else index = 0;
                        IsResolutionChanged = true;
                        game.graphics.PreferredBackBufferWidth = values.GetResolutionList()[index].Width;
                        game.graphics.PreferredBackBufferHeight = values.GetResolutionList()[index].Height;
                        settings.UpdateItem("Display mode:",0,values.IsFullScreen());
                        settings.UpdateItem("Back", 2);
                        settings.UpdateItem("Resolution:",1,values.GetResolution(index));
                        game.graphics.ApplyChanges();
                        game.componentGameMenu = new ComponentGameMenu(game);
                        break;
                    case "Back":
                        game.ComponentEnable(this,false);
                        if(!IsResolutionChanged)
                        game.ComponentEnable(game.componentGameMenu);
                        else 
                            game.Components.Add(game.componentGameMenu);
                        break;
                }
            } 
            settings.CursorPosition();
            base.Update(gameTime);
        }
        /// <summary>
        /// Drawable method
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.spritMenuBackground, new Rectangle(0, 0, game.graphics.PreferredBackBufferWidth, game.graphics.PreferredBackBufferHeight), Color.White);  //vykreslen� backgroundu pro settings
            settings.Draw();
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
