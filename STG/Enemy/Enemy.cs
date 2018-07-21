using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace STG
{
    public class Enemy : CollidableObject
    {
        static Random rnd = new Random();
        protected int randomNumber = rnd.Next(1, 700);

        protected void CollideWith(CollidableObject obj)
        {
            if (obj == null)
                return;

            if(obj is Bullet)
            {
                CollidableObject bullet = obj;
                
                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    bullet.OnCollide(this);
                }
            }

            if (obj is PenetrateBullet)
            {
                CollidableObject bullet = obj;

                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    //bullet.OnCollide(this);
                }
            }
        }


        protected int count;

        protected Player player;

        //破壊効果音
        protected asd.SoundSource deathSound;

        //再生中のBGMを扱うためのID
        protected int deathseID;

        public override void OnCollide(CollidableObject obj)
        {
            asd.Engine.AddObject2D(new BreakObjectEffect(Position));
            Singleton.Getsingleton();
            //Singleton.singleton.score += 10;
            deathseID = asd.Engine.Sound.Play(deathSound);
            randomNumber = rnd.Next(1, 700);
           
            Dispose();
        }
   

        //コンストラクタ
        protected Enemy(asd.Vector2DF pos,asd.Vector2DF movevector,Player player)
            : base()
        {
            Position = pos;

            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Enemy.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            count = 0;

            this.player = player;
        }

        public Enemy(Vector2DF pos, Player player)
        {
            Position = pos;
            this.player = player;

            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Enemy.png");

            Radius = Texture.Size.X / 2.0f;

            //破壊時の効果音を読み込む
            deathSound = asd.Engine.Sound.CreateSoundSource("Resources/Explode.wav", true);
        }

        protected override void OnUpdate()
        {
            foreach (var obj in Layer.Objects)
                CollideWith((obj as CollidableObject));

            //破壊音量を下げる。
            asd.Engine.Sound.SetVolume(deathseID, 0.3f);

            ++count;
        }

        protected void DisposeFromGame()
        {
            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {    
                Dispose();
            }
        }
    }
}
