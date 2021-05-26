using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace PromotionEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string jsonDataPath = @"ShopData.json";
            string jsonShopData = new StreamReader(jsonDataPath).ReadToEnd();
            //Preloading Shop
            Shop shop = (Shop)JsonConvert.DeserializeObject<Shop>(jsonShopData);
            
            Cart customerShoppingCart = null;

            //Promotion Engine Service
            PromotionEngineService promotionEngineService = new PromotionEngineService();

            do
            {
                Console.WriteLine("Login as :");
                Console.WriteLine("1: Shop Admin - Manage Products, Prices");
                Console.WriteLine("2: As Customer - Do Shopping");
                var userIp = Convert.ToInt32(Console.ReadLine());

                switch (userIp)
                {
                    //Login as Shop Admin
                    case 1:
                        string shopUserIP = "n";
                        do
                        {
                            Console.WriteLine("Shop has " + shop.Products.Count.ToString() + " products");
                            Console.WriteLine("Do you want to add more: (Y/N)");
                            shopUserIP = Console.ReadLine().ToString().ToLower();
                            if (shopUserIP == "y")
                            {
                                Console.WriteLine("Please provide product details as SKU, Price");
                                string productDetails = Console.ReadLine().ToString();
                                shop.AddProductToShop(productDetails);
                            }

                        } while (shopUserIP == "y");
                        break;

                    //Log in as Customer
                    case 2:
                        int cartUserIP = 4;
                        customerShoppingCart = new Cart(false, false);
                        do
                        {
                            Console.WriteLine("-----Welcome:------");
                            Console.WriteLine("Please choose from the following : ");
                            Console.WriteLine("1. Add Products to Cart ");
                            Console.WriteLine("2. Show Products in Cart");
                            Console.WriteLine("3. Finalize Cart / Apply Promotions");
                            Console.WriteLine("4. Exit Shop");
                            cartUserIP = Convert.ToInt32(Console.ReadLine());

                            switch (cartUserIP)
                            {
                                //Adding Products to Cart
                                case 1:
                                    int i = 1;
                                    Console.WriteLine("Id : SKU");
                                    foreach (Product product in shop.Products)
                                    {
                                        Console.WriteLine(i + ": " + product.skuID);
                                        i += 1;
                                    }
                                    string userCartIP = Console.ReadLine();

                                    foreach (string s in userCartIP.Split(','))
                                    {
                                        int productLoc = Convert.ToInt32(s);
                                        customerShoppingCart.AddProductToCart(shop.GetProductDetails(productLoc - 1), 1);
                                    }
                                    break;

                                //Display Customer Cart
                                case 2:
                                    Console.WriteLine("Your Cart holds: " + customerShoppingCart.Products.Keys.Count + " Products");
                                    int k = 1;
                                    foreach (KeyValuePair<string, int> product in customerShoppingCart.Products)
                                    {
                                        Console.WriteLine(k + ". Product: " + product.Key + ", Qty: " + product.Value);
                                    }
                                    break;

                                //Apply Running Promotions on Cart
                                case 3:
                                    ApplyPromotions(promotionEngineService, customerShoppingCart, shop);
                                    break;
                            }

                        } while (cartUserIP != 4);
                        break;
                }
            }
            while (true);
        }

        private static void ApplyPromotions(PromotionEngineService promotionEngineService, Cart customerShoppingCart, Shop shop)
        {
            Promotion singlePromotion = new Promotion()
            {
                Name = "Bulk on Single Product",
                ProductIdQtyMap = new Dictionary<string, int>()
            };
            singlePromotion.ProductIdQtyMap.Add("A", 2);
            singlePromotion.Price = 750;

            BulkBuySingleProductPromotionService bulkBuyPromotion = new BulkBuySingleProductPromotionService(singlePromotion);

            Promotion comboPromotion = new Promotion()
            {
                Name = "Combo Promotion",
                ProductIdQtyMap = new Dictionary<string, int>()
            };

            comboPromotion.ProductIdQtyMap.Add("B", 1);
            comboPromotion.ProductIdQtyMap.Add("C", 1);
            comboPromotion.Price = 250;

            ComboProductsPromotionService comboBuyPromotion = new ComboProductsPromotionService(comboPromotion);

            //By Default going for Bulk Buy Promotion on Single Product
            promotionEngineService.SetPromotionService(bulkBuyPromotion);

            var discountedPrice = promotionEngineService.GetDiscountedPrice(customerShoppingCart.Products, shop, customerShoppingCart.ApplyDiscountOnMoreThanOneProduct);

            //In case Single Buy Promotion is not applicable
            if(discountedPrice == customerShoppingCart.TotalPrice)
            {
                promotionEngineService.SetPromotionService(comboBuyPromotion);
                discountedPrice = promotionEngineService.GetDiscountedPrice(customerShoppingCart.Products, shop, customerShoppingCart.ApplyDiscountOnMoreThanOneProduct);

                if(discountedPrice == 0)
                {
                    discountedPrice = customerShoppingCart.TotalPrice;
                }
            }
            

            Console.WriteLine("Cart Value:");
            Console.WriteLine("Total cart Value: " + customerShoppingCart.TotalPrice);
            Console.WriteLine("Cart Value after Discount: " + discountedPrice);


        }
    }
}
