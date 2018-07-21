using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class RushingEnemy4 : Enemy
    {
        private asd.Vector2DF moveVelocity;

        public RushingEnemy4(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Enemy2.png");
        }

        protected override void OnUpdate()
        {

            moveVelocity = (player.Position - Position).Normal;

            Position += moveVelocity * 3.0f;

            base.OnUpdate();

            //角度（度）を計算。
            double rad = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - Position.X);

            double rad2 = rad * 180 / Math.PI;

            //プレイヤーの向きに変える
            Angle = (float)rad2;
        }

        public override void OnCollide(CollidableObject obj)
        {

            base.OnCollide(obj);
            
            Singleton.singleton.score += 20;
          
            if(randomNumber >= 61 & randomNumber <= 81)
            {
                    asd.Engine.AddObject2D(new Item.SpeedThereeShotItem(Position));

            }
            else
            if (randomNumber >= 71 & randomNumber <= 91)
            {
                asd.Engine.AddObject2D(new Item.PenetrateTriShotItem(Position));

            }
        }

    }
}
