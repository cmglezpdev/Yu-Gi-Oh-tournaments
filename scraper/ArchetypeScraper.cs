using Newtonsoft.Json.Linq;
using HtmlAgilityPack;

namespace Scraper
{
    class ArchetypeScraper
    {
        public static List<JObject> cards(string url)
        {
            try 
            {
                var listCardJson = CardScraper.obtain_json(url);
                
                List<JObject> cards = new List<JObject>();
                foreach(var card in listCardJson["data"])
                {
                    var cardJson = new JObject();
                    cardJson["name"] = card["name"];
                    cards.Add(cardJson);
                }
                return cards;
            }
            catch
            {
                return new List<JObject>();
            }
        }
        public static JObject arquetype_json(Dictionary<string,List<JObject>> cards)
        {
            var resultJson = new JObject();

            foreach (var archetype in cards.Keys)
            {
                resultJson[archetype] = new JArray(cards[archetype.ToString()]);
            }

            return resultJson;
        }
        public static  Dictionary<string,List<JObject>> obtain_archetypes()
        {
            var web = new HtmlWeb();
            var document = web.Load("https://www.yugiohcardguide.com/yugioh-archetypes.html");

            var url = "https://db.ygoprodeck.com/api/v7/cardinfo.php?archetype=";

            var HTMLElements = document.DocumentNode.QuerySelectorAll(".col-12 li");
           
            Dictionary<string,List<JObject>> cardArquetype = new Dictionary<string, List<JObject>>(); 
            foreach(var item in HTMLElements)
            {
                var link = HtmlEntity.DeEntitize(item.QuerySelector("a").InnerText);
                string archetype = link;
                string archetypeMod = archetype.Replace(" ", "%20");

                cardArquetype.Add(archetype, cards(url + archetypeMod));
            }
            return cardArquetype;
        }       
    }
}