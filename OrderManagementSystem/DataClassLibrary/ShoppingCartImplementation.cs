using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataClassLibrary.Models;

namespace DataClassLibrary
{
    /// <summary>
    /// This class implements all the functionalities related to cart.
    /// </summary>
    public class ShoppingCartImplementation
    {
        /// <summary>
        /// This method adds a single product to the cart, corresponding to a particular user.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static string AddToCart(int ProductId, string UserId)
        {
            string result=string.Empty;
           
            return result;

        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method reduces the quantity of a particular product in the cart by one, corresponding to a particular user.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>

        public static string ReduceFromCart(int ProductId, string UserId)
        {
            string result=string.Empty;
            
            return result;
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to get all the products details present in the cart of a particular user.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        
        public static List<AddToCartModel> GetFromCart(string userid)
        {
            List<AddToCartModel> listCartModel = new List<AddToCartModel>();

            
            return listCartModel;

        }

        //______________________________________________________________________________________

        /// <summary>
        /// This is used to remove an individual product from the cart.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static string RemoveFromCart(int ProductId, string UserId)
        {
            string result=string.Empty;
            
           
            return result;
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to remove all the products from the cart.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static string EmptyCart(string UserId)
        {
            string result=string.Empty;
         
            return result;
        }

        //______________________________________________________________________________________

    }
}