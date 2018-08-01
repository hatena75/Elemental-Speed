using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class Otetsuki : asd.TextureObject2D
    {
        public Otetsuki(asd.Texture2D tex ,asd.Vector2DF pos)
        {
            Texture = tex;
            Position = pos;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (((GameScine)Layer.Scene).player1.otetsuki_count == 0 && ((GameScine)Layer.Scene).player1.otetsuki_flag == true)
            {
                Dispose();

                ((GameScine)Layer.Scene).player1.otetsuki_flag = false;
            }

            if (((GameScine)Layer.Scene).player2.otetsuki_count == 0 && ((GameScine)Layer.Scene).player2.otetsuki_flag == true)
            {
                Dispose();

                ((GameScine)Layer.Scene).player2.otetsuki_flag = false;
            }
        }
       
    }
}
