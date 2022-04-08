// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Web;
using TestTaskFlatRock.Models;

string textHtml = File.ReadAllText("../../../../TestTaskFlatRock/MyHtml.html");
HtmlDocument doc = new HtmlDocument();
doc.LoadHtml(textHtml);

HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(".//div[@class='item']");
List<Item> itemsList = new List<Item>();
foreach (HtmlNode item in collection)
{
    string ItemName = HttpUtility.HtmlDecode(item.SelectSingleNode(".//img [@alt]").GetAttributeValue("alt", "default"));

    string ItemRating = HttpUtility.HtmlDecode(item.GetAttributeValue("rating", "default"));
    if (Convert.ToDecimal(ItemRating) >= 5)
    {
        ItemRating = "5";
    }
    string ItemPrice = HttpUtility.HtmlDecode(item.SelectSingleNode(".//div [@class='pricing']//span [@itemprop='price']//span [@style='display: none']").InnerText);
    decimal ItemPriceDecimal = Decimal.Parse(ItemPrice.Substring(1));
    ;
    var newItem = new Item
    {

        ProductName = ItemName,
        Price = ItemPriceDecimal.ToString(),
        Rating = ItemRating

    };
    itemsList.Add(newItem);


}

JavaScriptSerializer js = new JavaScriptSerializer();
DefaultContractResolver contractResolver = new DefaultContractResolver
{
    NamingStrategy = new CamelCaseNamingStrategy()
};
var jsonSettings = new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ContractResolver = contractResolver
};

Console.WriteLine(JsonConvert.SerializeObject(itemsList, jsonSettings));