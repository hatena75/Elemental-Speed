using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Card : asd.TextureObject2D
    {

        public int number; //カードの数字
        public typename type; //回復、攻撃、防御など
        public elementname element; //炎、水、草など
        public int effect; //攻撃力、回復数など
        public string texture; //画像名

        /*
        //プロパティ
        public int Number { get; set; }
        public int Type { get; set; }
        public int Element { get; set; }
        public int Effect { get; set; }
        */

        public static List<Card> cardlist = new List<Card>();

        private static int number_of_card = 0;

        public static int Number_of_card
        {
            get { return number_of_card; }
            private set { number_of_card = value; }
        }


        Card()
        {

        }



        Card(int number_fanction,typename type_fanction, elementname element_fanction, int effect_fanction, string texture_fanction)
        {    
            number = number_fanction;
            type = type_fanction;
            element = element_fanction;
            effect = effect_fanction;
            Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/trump/{texture_fanction}.png");
            cardlist.Add(this);
            Number_of_card += 1;
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
            rock,
            wind
        }

        public static void CardCreate()
        {
            // コレクションのデシリアライズ
            var text = System.IO.File.ReadAllText("Card.json");
            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Card>>(text);
            foreach (var item in list)
            {
                
                item.Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/trump/{item.texture}.png");
                Console.WriteLine("number: {0}, type: {1}", item.number, item.Texture);

            }
            

            Card card1 = new Card(0, typename.attack, elementname.fire, 10, "f10");
            Card card2 = new Card(1, typename.attack, elementname.fire, 10, "f11");
            Card card3 = new Card(2, typename.attack, elementname.fire, 10, "f12");
            Card card4 = new Card(3, typename.attack, elementname.fire, 10, "f13");
            Card card5 = new Card(4, typename.attack, elementname.fire, 10, "f14");
            Card card6 = new Card(5, typename.attack, elementname.fire, 10, "f15");

            Card card7 = new Card(0, typename.attack, elementname.water, 10, "w10");
            Card card8 = new Card(1, typename.attack, elementname.water, 10, "w11");
            Card card9 = new Card(2, typename.attack, elementname.water, 10, "w12");
            Card card10 = new Card(3, typename.attack, elementname.water, 10, "w13");
            Card card11= new Card(4, typename.attack, elementname.water, 10, "w14");
            Card card12 = new Card(5, typename.attack, elementname.water, 10, "w15");
        }

    }
}
