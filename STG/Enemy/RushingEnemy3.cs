using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class RushingEnemy3 : Enemy
    {
        private const int maxHP = 20;
        private int HP;

        private asd.Vector2DF moveVelocity;

        public RushingEnemy3(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Boss2.png");
            HP = maxHP;
            //破壊時の効果音を読み込む
            deathSound = asd.Engine.Sound.CreateSoundSource("Resources/Explode2.wav", true);
        }

        protected override void OnUpdate()
        {

            moveVelocity = (player.Position - Position).Normal;

            Position += moveVelocity * 0.6f;

            base.OnUpdate();

            //角度（度）を計算。
            double rad = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - Position.X);

            double rad2 = rad * 180 / Math.PI;

            //プレイヤーの向きに変える
            Angle = (float)rad2;
        }

        public override void OnCollide(CollidableObject obj)
        {
            if (HP > 0)
            {
                HP -= 1;
            }
            else
            {
                base.OnCollide(obj);
                //asd.Engine.AddObject2D(new BreakObjectEffect2(Position));
                Singleton.singleton.score += 50;

                if (randomNumber >= 41 & randomNumber <= 45)
                {
                    asd.Engine.AddObject2D(new Item.BombItem(Position));
                }
                else
                if (randomNumber >= 51 & randomNumber <= 91)
                {
                    asd.Engine.AddObject2D(new Item.BarrierItem(Position));

                }
            }


        }
    }
}