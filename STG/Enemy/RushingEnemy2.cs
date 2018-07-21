using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class RushingEnemy2 : Enemy
    {
        private const int maxHP = 3;
        private int HP;

        private asd.Vector2DF moveVelocity;

        public RushingEnemy2(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Boss.png");
            HP = maxHP;
            //破壊時の効果音を読み込む
            deathSound = asd.Engine.Sound.CreateSoundSource("Resources/Explode2.wav", true);
        }

        protected override void OnUpdate()
        {

            moveVelocity = (player.Position - Position).Normal;

            Position += moveVelocity;

            base.OnUpdate();

            //角度（度）を計算。
            double rad = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - Position.X);

            double rad2 = rad * 180 / Math.PI;

            //プレイヤーの向きに変える
            Angle = (float)rad2;
        }

        public override void OnCollide(CollidableObject obj)
        {
            if(HP > 0)
            {
                HP -= 1;
            }
            else
            {
                base.OnCollide(obj);
                //asd.Engine.AddObject2D(new BreakObjectEffect2(Position));
                Singleton.singleton.score += 30;

                if (randomNumber >= 1 & randomNumber <= 7)
                {
                    asd.Engine.AddObject2D(new Item.PenetrateShotItem(Position));
                }
                else
                if (randomNumber >= 11 & randomNumber <= 16)
                {
                    asd.Engine.AddObject2D(new Item.ThereeShotItem(Position));
                }
                else
                if (randomNumber >= 31 & randomNumber <= 36)
                {
                    asd.Engine.AddObject2D(new Item.TriShotItem(Position));

                }
            }
            

        }
    }
}
