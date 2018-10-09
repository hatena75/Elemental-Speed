using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Card : asd.TextureObject2D
    {
        //ここのパラメータはjsonで別途記述している
        public int number; //カードの数字
        public typename type; //回復、攻撃、防御など
        public elementname element; //炎、水、草など
        public int effect; //攻撃力、回復数など
        public string texture; //画像名

        public static List<Card> cardlist = new List<Card>();

        Card()
        {

        }

        public enum typename
        {
            attack = 1,
            heal
        }

        public enum elementname
        {
            fire = 1,
            water,
            ground,
            wind
        }

        public static void CardCreate()
        {
            // コレクションのデシリアライズ
            var text = System.IO.File.ReadAllText("Card.json");
            cardlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Card>>(text);
            foreach (var item in cardlist)
            {
                item.Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/trump/{item.texture}.png");
                Console.WriteLine("number: {0}, type: {1}", item.number, item.Texture);
            }
        }

    }
}
