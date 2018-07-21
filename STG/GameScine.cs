using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class GameScine : asd.Scene
    {
        //Player player;

        public Card_field card_right;

        public Card_field card_left;

        //player1
        Card_play card_Z;
        Card_play card_X;
        Card_play card_C;
        Card_play card_V;

        //player2
        Card_play card_Q;
        Card_play card_W;
        Card_play card_E;
        Card_play card_R;

        public CardPlayer player1;
        public CardPlayer player2;


        bool isSceneChanging = false;
        bool iswindetermind = false;

        asd.Layer2D gameLayer;

        int count = 0;

        public asd.TextObject2D obj = new asd.TextObject2D();

        public asd.TextObject2D obj2 = new asd.TextObject2D();


        //BGM
        asd.SoundSource bgm;

        //再生中のBGMを扱うためのID
        int? playingBgmId;
        
        //乱数を用意する
        static Random rnd = new Random();
        public int randomnumber()
        {
            return rnd.Next(Card.cardlist.Count);
        }



        protected override void OnRegistered()
        {
            Card.CardCreate();

            gameLayer = new asd.Layer2D();

            asd.Layer2D backgroundLayer = new asd.Layer2D();

            backgroundLayer.DrawingPriority = -10;
            
            AddLayer(gameLayer);
            AddLayer(backgroundLayer);

            Background bg = new Background(new asd.Vector2DF(0.0f, 0.0f), "Resources/Bg.png");

            backgroundLayer.AddObject(bg);

            //player = new Player();

            card_left = new Card_field(220,470, randomnumber());

            card_right = new Card_field(420,470, randomnumber());

            card_Z = new Card_play(120, 700, 1,randomnumber());
            card_X = new Card_play(270, 700, 2, randomnumber());
            card_C = new Card_play(420, 700, 3, randomnumber());
            card_V = new Card_play(570, 700, 4, randomnumber());

            card_Q = new Card_play(320, 0, 5, randomnumber());
            card_W = new Card_play(190, 200, 6, randomnumber());
            card_E = new Card_play(450, 200, 7, randomnumber());
            card_R = new Card_play(320, 280, 8, randomnumber());

            player1 = new CardPlayer(200,"Player1");
            player2 = new CardPlayer(200,"Player2");


            //gameLayer.AddObject(player);
            gameLayer.AddObject(card_left);
            gameLayer.AddObject(card_right);

            gameLayer.AddObject(card_Z);
            gameLayer.AddObject(card_X);
            gameLayer.AddObject(card_C);
            gameLayer.AddObject(card_V);

            gameLayer.AddObject(card_Q);
            gameLayer.AddObject(card_W);
            gameLayer.AddObject(card_E);
            gameLayer.AddObject(card_R);

            gameLayer.AddObject(player1);
            gameLayer.AddObject(player2);

             
            //文字レイヤーここから

            asd.Layer2D layertext = new asd.Layer2D();
            AddLayer(layertext);
            
            // フォントを生成する。
            var font = asd.Engine.Graphics.CreateDynamicFont("", 35, new asd.Color(255, 0, 0, 255), 1, new asd.Color(255, 255, 255, 255));

            // 文字描画オブジェクトを生成する。

            // 描画に使うフォントを設定する。
            player1.obj.Font = font;

            // 描画位置を指定する。
            player1.obj.Position = new asd.Vector2DF(350, 810);

            // 描画する文字列を指定する。
            player1.obj.Text = "Player1 : " + player1.HP;

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            layertext.AddObject(player1.obj);

            // 描画に使うフォントを設定する。
            player2.obj.Font = font;

            // 描画位置を指定する。
            player2.obj.Position = new asd.Vector2DF(0, 0);

            // 描画する文字列を指定する。
            player2.obj.Text = "Player2 : " + player2.HP;

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            layertext.AddObject(player2.obj);


            //文字レイヤーここまで


            //BGMを読み込む
            bgm = asd.Engine.Sound.CreateSoundSource("Resources/LUX.ogg", false);

            //BGMループ
            bgm.IsLoopingMode = true;

            //IDはnull(BGMは流れてない）
            playingBgmId = null;
            
        }

        

        protected override void OnUpdated()
        {
            //勝利が確定したときの処理
            if ((player1.win_state != false || player2.win_state != false) && iswindetermind == false) {
                // フォントを生成する。
                var font = asd.Engine.Graphics.CreateDynamicFont("", 35, new asd.Color(255, 0, 0, 255), 1, new asd.Color(255, 255, 255, 255));

                // 文字描画オブジェクトを生成する。
                var obj = new asd.TextObject2D();

                // 描画に使うフォントを設定する。
                obj.Font = font;

                // 描画位置を指定する。
                obj.Position = new asd.Vector2DF(200, 450);

                if(player1.win_state == true)
                {
                    // 描画する文字列を指定する。
                    obj.Text = "Player2 WIN!";
                }
                else
                {
                    obj.Text = "Player1 WIN!";
                }

                // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
                asd.Engine.AddObject2D(obj);

                iswindetermind = true;
            }

            //勝利後の処理
            if(iswindetermind == true && asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push && isSceneChanging == false)
            {
                asd.Engine.ChangeSceneWithTransition(new TitleScene(), new asd.TransitionFade(1.0f, 1.0f));

                //iswindetermind = false;

                isSceneChanging = true;
            }

            if (count == 10)
            {
                playingBgmId = asd.Engine.Sound.Play(bgm);
            }

            //各カードにおいて場に出せるかどうか判定
            if(gameLayer.Objects.OfType<Card_field>().All(u => 
                    gameLayer.Objects.OfType<Card_play>().All(x => u.card_now.number != x.card_now.number + 1 && u.card_now.number != x.card_now.number - 1)) && count % 200 == 0)
                    {
                        card_left.card_now = Card.cardlist[randomnumber()];

                        card_right.card_now = Card.cardlist[randomnumber()];
                    }        

            count++;
        }
    }
}
