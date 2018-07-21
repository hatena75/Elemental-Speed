using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Card_field : asd.TextureObject2D
    {
        int count = 0;

        public Card card_now;

        //ショットの効果音
        private asd.SoundSource shotSound;

        //ボムを発動したときの効果音
        private asd.SoundSource bombSound;

        public Card_field(float x, float y, int num)
        {
            card_now = Card.cardlist[num];

            Texture = card_now.Texture;

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            Position = new asd.Vector2DF(x, y);

            //ショットの効果音を読み込む
            shotSound = asd.Engine.Sound.CreateSoundSource("Resources/Shot.wav", true);

            //ボム発動の効果音を読み込む
            bombSound = asd.Engine.Sound.CreateSoundSource("Resources/Bomb.wav", true);
        }

        protected override void OnUpdate()
        {
            Texture = card_now.Texture;

            //ボム発動
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.X) == asd.KeyState.Push)
            {
                
            }

            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);

            Position = position;

            count++;
        }
    }
}

