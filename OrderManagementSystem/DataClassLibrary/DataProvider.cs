using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataClassLibrary.Models;

using System.Transactions;
using System.IO;
using System.Web.Configuration;

namespace DataClassLibrary
{

    /// <summary>
    /// This class file is used for accessing the database and performing all the operations on it.
    /// </summary>
    public class DataProvider
    {
        /// <summary>
        /// This method is used to register a new user in the database.
        /// </summary>
        /// <param name="usersignup"></param>
        /// <returns></returns>

        public static string Registration(UserSignUp usersignup)
        {
            try
            {
                //Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                //DbCommand CmdObj = db.GetStoredProcCommand("SignUp");
                //db.AddInParameter(CmdObj, "@UserId", DbType.String, usersignup.UserId);
                //db.AddInParameter(CmdObj, "@Password", DbType.String, usersignup.Password);
                //db.AddInParameter(CmdObj, "@EmailId", DbType.String, usersignup.EmailId);
                //db.AddOutParameter(CmdObj, "@strMessage", DbType.String, 255);
                //db.ExecuteNonQuery(CmdObj);
                //return db.GetParameterValue(CmdObj, "@strMessage").ToString();
            }
            catch (Exception ex)
            {
                //bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                //if (rethrow) throw;
                //string result = " Error in Registration.";
                //return result;
            }
            return string.Empty;
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method is used to login a registered user and grant corresponding privileges.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>

        public static string Login(string user, string pwd)
        {
            try
            {
                MendixGebruikersService.GebruikersServicePortTypeClient msClient = new MendixGebruikersService.GebruikersServicePortTypeClient();
                var response = msClient.GetGebruiker(new MendixGebruikersService.GetGebruiker() { Naam = user, Wachtwoord = pwd });

                return response.Result != null
                    ? "Admin logged in successfully"
                    : "";
            }
            catch (Exception ex)
            {

                string result = " Error in Login.";
                return result;
            }
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method creates a new product in the database. It can be accessed only by an administrator.
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>

        public static string AddProductToDB(Product prod)
        {
            try
            {
                //AddItToMendix

                return string.Empty;
            }
            catch (Exception ex)
            {

                string result = " Error in adding product to database.";
                return result;
            }
        }
        //_____________________________________________________________________

        /// <summary>
        /// An administrator uses this method to update the details of an existing product in the database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public static string UpdateProduct(Product product)
        {
            try
            {
                return string.Empty;
            }
            catch (Exception ex)
            {

                string result = " Error in Updating Product.";
                return result;
            }

        }
        //_____________________________________________________________________

        /// <summary>
        /// This method is used by an administrator to delete an existing product from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public static string Delete(int id)
        {
            string result;
            try
            {
                return string.Empty;
            }
            catch (Exception ex)
            {

                result = " Error in deleting product.";
                return result;
            }
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method is used to search for an existing product in the database by its name. (Can be accessed by all users.)
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>

        public static List<Product> ProductSearch(string ProductName)
        {
            return new List<Product>();
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method is used to retrieve all the products from the database for display. 
        /// </summary>
        /// <returns></returns>

        public static List<Product> GetProducts()
        {
            MendixArtikelenService.ArtikelenServicePortTypeClient masClient = new MendixArtikelenService.ArtikelenServicePortTypeClient();
            masClient.Open();

            MendixArtikelenService.mfGetArtikelenResponseArtikel[] artikels =
                masClient.mfGetArtikelen(new MendixArtikelenService.mfGetArtikelen() { PageSize = -1, OffSet = -1 });

            var listOfProduct = new List<Product>();
            foreach (var artikel in artikels)
            {
                
                //var product = new Product()
                //{
                //    CategoryOmschrijving = artikel.Artikel_Categorie.Categorie.Omschrijving,
                //    ProductId = artikel.Artikelcode,
                //    ProductName = artikel.Omschrijving,
                //    Price = artikel.Verkoopprijs != null
                //                ? Convert.ToDouble(artikel.Verkoopprijs)
                //                : 0.00,
                //    Picture = "noDetailPic.jpg"

                //};
                var productCatRelation = GetProduct(Convert.ToInt16(artikel.Artikelcode));
                listOfProduct.Add(productCatRelation.product);
            };
            masClient.Close();
            return listOfProduct;
        }
        //_____________________________________________________________________

        /// <summary>
        /// An administrator uses this method to keep a track on all the users registered with the website.
        /// </summary>
        /// <returns></returns>

        public static List<UserData> GetAllUsers()
        {
            MendixGebruikersService.GebruikersServicePortTypeClient msClient = new MendixGebruikersService.GebruikersServicePortTypeClient();
            msClient.Open();
            var response = msClient.GetGebruikers(new MendixGebruikersService.GetGebruikers());
            
            var userslist = new List<UserData>();
            foreach (var gebruiker in response)
            {
                var user = new UserData()
                {
                    CustomerId = gebruiker.GebruikerId.ToString(),
                    UserId = gebruiker.GebruikerId.ToString(),
                    FirstName = gebruiker.Voornaam,
                    LastName = gebruiker.Achternaam,
                    BillingAddress = gebruiker.Straat + " " + gebruiker.Huisnummer,
                    EmailId = gebruiker.Emailadres
                };
                userslist.Add(user);
            }
            msClient.Close();
            return userslist;


        }
        //_____________________________________________________________________

        /// <summary>
        /// This method is used to get the information regarding a particular (logged in) user.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>

        public static UserData GetUserDetailsById(int CustomerId)
        {
            var user = new UserData();
            return user;
        }

        //_____________________________________________________________________

        /// <summary>
        /// This method gets the details of the logged user in order to display it in his account summary page.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>

        public static UserData GetUserDetailsByUserName(string UserId)
        {
            MendixGebruikersService.GebruikersServicePortTypeClient msClient = new MendixGebruikersService.GebruikersServicePortTypeClient();
            var response = msClient.GetGebruiker(new MendixGebruikersService.GetGebruiker() { Naam = UserId, Wachtwoord = "" });
            var gebruiker = response.Result;

            var user = new UserData()
            {
                BillingAddress = gebruiker.Straat + " " + gebruiker.Huisnummer,
                FirstName = gebruiker.Voornaam,
                LastName = gebruiker.Achternaam,
                UserId = gebruiker.GebruikerId.ToString()

            };

            return user;

        }

        //_____________________________________________________________________

        /// <summary>
        /// This method gets the details of the logged user in order to display it in his account summary page.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>

        public static UserData GetUserDetailsByUserName2(string UserId)
        {
            var user = new UserData();
            return user;

        }

        //_____________________________________________________________________

        /// <summary>
        /// Gets the details of the particular product through its ProductId.
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>

        public static ProductCategoryRelation GetProduct(long productid)
        {
            MendixArtikelenService.ArtikelenServicePortTypeClient masClient = new MendixArtikelenService.ArtikelenServicePortTypeClient();
            masClient.Open();

            MendixArtikelenService.mfGetArtikelMetArtikelcodeResponse artikel =
                masClient.mfGetArtikelMetArtikelcode(new MendixArtikelenService.mfGetArtikelMetArtikelcode() { ArtikelCode = productid });

            ProductCategoryRelation pc = new ProductCategoryRelation();

            Product prod = new Product();

            Categories cat = new Categories();
            pc.product = new Product()
            {
                ProductId = artikel.Artikel.Artikelcode,
                CategoryOmschrijving = artikel.Artikel.Artikel_Categorie.Categorie.Omschrijving,
                Price = Convert.ToDouble(artikel.Artikel.Verkoopprijs),
                ProductDescription = artikel.Artikel.Omschrijving,
                ProductName = artikel.Artikel.Omschrijving,
                Picture = artikel.Artikel.Artikel_Afbeelding.Afbeelding?.Contents == null
                    ? "noDetailPic.jpg"
                    : "detailPic" + artikel.Artikel.Artikelcode + ".jpg",
                PictureContent = artikel.Artikel.Artikel_Afbeelding.Afbeelding?.Contents
            };

            return pc;
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method gets all the products in a particular category.
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>

        public static List<Product> GetProductsByCategoryName(string CategoryName)
        {
            var prodlist = new List<Product>();

            return prodlist;


        }
        //_______________________________________________________________________

        /// <summary>
        /// This method gets all the products in a particular subcategory.
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="SubCategoryName"></param>
        /// <returns></returns>

        public static List<Product> GetProductsBySubCategoryName(string CategoryName, string SubCategoryName)
        {
            var prodlist = new List<Product>();

            return prodlist;


        }
        //_______________________________________________________________________

        /// <summary>
        /// This method returns a list of all the different categories in the database.
        /// </summary>
        /// <returns></returns>

        public static List<string> GetAllCategories()
        {
            var catnames = new List<string>();
            return catnames;

        }
        //_______________________________________________________________________

        /// <summary>
        /// This method is used by a registered user to update his information (such as default shipping address, card no. etc).
        /// </summary>
        /// <param name="ud"></param>
        /// <returns></returns>

        public static string UpdateUser(UserData ud)
        {
            try
            {

                return string.Empty;
            }
            catch (Exception ex)
            {

                string result = " Error in updating user.";
                return result;
            }

        }
        //_______________________________________________________________________

        /// <summary>
        /// This method is used to fetch all the information from the cart and the user, during the placing of an order, and saves the order in the database. 
        /// </summary>
        /// <param name="userupdate"></param>
        /// <returns></returns>

        public static List<string> PlaceOrder(CheckOutModel userupdate)
        {
            List<string> ls = new List<string>();

            return ls;


        }

        //_____________________________________________________________________

        /// <summary>
        /// This method is used to get full information of a particular order, and helps an user track his order.
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>

        public static OrderInformation GetOrderDetails(int orderid)
        {


            var orderslist = new List<OrderDetails>();


            OrderInformation oi = new OrderInformation();
            Orders ord = new Orders();


            return oi;

        }
        //_____________________________________________________________________

        /// <summary>
        /// An administrator can use this method to get all the orders registered in the website.
        /// </summary>
        /// <returns></returns>

        public static List<Orders> GetAllOrders()
        {
            var orderslist = new List<Orders>();

            return orderslist;

        }
        //_____________________________________________________________________

        /// <summary>
        /// A registered user can use this method to get the history of all his orders.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public static List<Orders> GetMyOrders(string userId)
        {
            var orderslist = new List<Orders>();

            return orderslist;
        }
        //_____________________________________________________________________

        /// <summary>
        /// Any registered user uses this method to cancel his order after placing, provided it is not yet shipped. 
        /// </summary>
        /// <returns></returns>

        public static string CancelMyOrder(int orderid, string userid)
        {
            return string.Empty;
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method filters the price of the items between a specified range.
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="Max"></param>
        /// <param name="Min"></param>
        /// <returns></returns>

        public static List<Product> PriceFilter(string CategoryName, int Max, int Min)
        {
            var prodlist = new List<Product>();

            return prodlist;

        }
        //_____________________________________________________________________

        /// <summary>
        /// This method returns the corresponding category id from the category name.
        /// </summary>
        /// <param name="catname"></param>
        /// <param name="subcatname"></param>
        /// <returns></returns>

        public static Categories GetCategoryIdByName(string catname, string subcatname)
        {
            Categories catobj = new Categories();


            return catobj;

        }

        //_____________________________________________________________________

        /// <summary>
        /// This method is used by an administrator to change the order status from 'Processing' to 'Shipped'.
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="UserId"></param>
        /// <param name="TransactionStatus"></param>
        /// <returns></returns>

        public static string OrderStatus(int orderid, string UserId, string TransactionStatus)
        {


            return string.Empty;
        }

        //_____________________________________________________________________

        /// <summary>
        /// This method is used during login by a security answer, when a user forgets his/her password.
        /// </summary>
        /// <param name="UserIds"></param>
        /// <param name="SecurityAnswer"></param>
        /// <returns></returns>

        public static UserCredentials LoginAfterForgotPassword(string UserIds, string SecurityAnswer)
        {

            UserCredentials use1 = new UserCredentials();

            return use1;

        }

    }

}
