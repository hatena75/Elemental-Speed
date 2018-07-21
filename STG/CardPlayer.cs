using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class CardPlayer : asd.TextureObject2D
    {
        public int HP;

        public asd.TextObject2D obj = new asd.TextObject2D();

        string name;

        public bool win_state = false;

        public CardPlayer(int hp, string Name)
        {
            HP = hp;
            name = Name;
        }

        protected override void OnUpdate()
        {
            if (HP <= 0)
            {
                //相手の勝利を告げる文字を表示
                win_state = true;
              
            }

            //各PlayerのHPの表示を更新
            obj.Text = name + " : " + HP;
        }
    }
}
