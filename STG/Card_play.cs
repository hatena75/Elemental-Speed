using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Card_play : asd.TextureObject2D
    {
        int count = 0;

        public int key;

        public Card card_now;

        bool change_flag = false;
        bool cpu_change_flag = false;
        public static bool isCpuDo = false;

        static Random rnd = new Random();

        int effect_temp;

        private asd.Joystick joystick = asd.Engine.JoystickContainer.GetJoystickAt(0);
        private bool joystickCheck(int t) => asd.Engine.JoystickContainer.GetIsPresentAt(0) && joystick.GetButtonState(t) == asd.JoystickButtonState.Push;

        //ショットの効果音
        private asd.SoundSource shotSound;

        //ボムを発動したときの効果音
        private asd.SoundSource bombSound;

        private void playingcard_left()
        {
            if (((GameScine)Layer.Scene).card_left.card_now.element == Card.elementname.fire && card_now.element == Card.elementname.water)
            {
                effect_temp = card_now.effect;
                effect_temp =effect_temp * 2;
            }
            else
            {
                effect_temp = card_now.effect;
            }

            if ((((GameScine)Layer.Scene).card_left.card_now.number == card_now.number + 1 || ((GameScine)Layer.Scene).card_left.card_now.number == card_now.number - 1) && change_flag == false)
                {
                    if(key <= 4)
                    {
                        ((GameScine)Layer.Scene).player2.HP = ((GameScine)Layer.Scene).player2.HP - effect_temp;
                    }
                    else
                    {
                        ((GameScine)Layer.Scene).player1.HP = ((GameScine)Layer.Scene).player1.HP - effect_temp;
                    }

                    ((GameScine)Layer.Scene).card_left.card_now = card_now;
                    change_flag = true;
                }

                if (change_flag == true)
                {
                    //乱数生成
                    //次の手札
                    card_now = Card.cardlist[rnd.Next(Card.cardlist.Count)];

                    change_flag = false;
                }
            
        }

        private void playingcard_right()
        {

            if ((((GameScine)Layer.Scene).card_right.card_now.number == card_now.number + 1 || ((GameScine)Layer.Scene).card_right.card_now.number == card_now.number - 1) && change_flag == false)
            {
                //属性判定
                if (((GameScine)Layer.Scene).card_right.card_now.element == Card.elementname.fire && card_now.element == Card.elementname.water)
                {
                    effect_temp = card_now.effect;
                    effect_temp = effect_temp * 2;
                }
                else
                {
                    effect_temp = card_now.effect;
                }

                //1Pか2Pか
                if (key <= 4)
                {
                    ((GameScine)Layer.Scene).player2.HP = ((GameScine)Layer.Scene).player2.HP - effect_temp;
                }
                else
                {
                    ((GameScine)Layer.Scene).player1.HP = ((GameScine)Layer.Scene).player1.HP - effect_temp;
                }

                ((GameScine)Layer.Scene).card_right.card_now = card_now;
                change_flag = true;
            }

            if (change_flag == true)
            {
                //乱数生成
                //次の手札
                card_now = Card.cardlist[rnd.Next(Card.cardlist.Count)];

                change_flag = false;
            }

        }

        public void playingcard_cpu(Card_field field)
        {

            if ((field.card_now.number == card_now.number + 1 || field.card_now.number == card_now.number - 1) && cpu_change_flag == false)
            {
                //属性判定
                if (((GameScine)Layer.Scene).card_right.card_now.element == Card.elementname.fire && card_now.element == Card.elementname.water)
                {
                    effect_temp = card_now.effect;
                    effect_temp = effect_temp * 2;
                }
                else
                {
                    effect_temp = card_now.effect;
                }

                //1Pか2Pか
                ((GameScine)Layer.Scene).player1.HP = ((GameScine)Layer.Scene).player1.HP - effect_temp;
                

                field.card_now = card_now;
                cpu_change_flag = true;
            }

            if (cpu_change_flag == true)
            {
                //乱数生成
                //次の手札
                card_now = Card.cardlist[rnd.Next(Card.cardlist.Count)];

                cpu_change_flag = false;
                isCpuDo = false;
            }
        }

        public Card_play(float x, float y, int Key, int RandomNumber)
        {

            Position = new asd.Vector2DF(x, y);

            key = Key;

            card_now = Card.cardlist[RandomNumber];

            Texture = card_now.Texture;

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);


            //ショットの効果音を読み込む
            shotSound = asd.Engine.Sound.CreateSoundSource("Resources/Shot.wav", true);

            //ボム発動の効果音を読み込む
            bombSound = asd.Engine.Sound.CreateSoundSource("Resources/Bomb.wav", true);
        }

        protected override void OnUpdate()
        {
            switch (key)
            {
                case 1:
                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.A) == asd.KeyState.Push)
                    {
                        playingcard_left();
                    }

                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.J) == asd.KeyState.Push)
                    {
                        playingcard_right();
                    }
                    break;

                case 2:
                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.S) == asd.KeyState.Push)
                    {
                        playingcard_left();

                    }

                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.K) == asd.KeyState.Push)
                    {
                        playingcard_right();

                    }
                    break;

                case 3:
                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.D) == asd.KeyState.Push)
                    {
                        playingcard_left();

                    }

                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.L) == asd.KeyState.Push)
                    {
                        playingcard_right();

                    }
                    break;

                case 4:
                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.F) == asd.KeyState.Push)
                    {
                        playingcard_left();

                    }

                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Semicolon) == asd.KeyState.Push)
                    {
                        playingcard_right();

                    }
                    break;

                case 5:
                    if (joystickCheck(14))
                    {
                        playingcard_left();
                    }

                    if (joystickCheck(3))
                    {
                        playingcard_right();
                    }
                    break;

                case 6:
                    if (joystickCheck(17))
                    {
                        playingcard_left();

                    }

                    if (joystickCheck(0))
                    {
                        playingcard_right();

                    }
                    break;

                case 7:
                    if (joystickCheck(15))
                    {
                        playingcard_left();

                    }

                    if (joystickCheck(2))
                    {
                        playingcard_right();

                    }
                    break;

                case 8:
                    if (joystickCheck(16))
                    {
                        playingcard_left();

                    }

                    if (joystickCheck(1))
                    {
                        playingcard_right();

                    }
                    break;

                default:
                    break;
            }

            Texture = card_now.Texture;

            

            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);

            Position = position;

            count++;
        }
    }
}

