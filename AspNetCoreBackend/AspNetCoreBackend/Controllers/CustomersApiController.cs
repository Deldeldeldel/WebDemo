﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBackend.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    { 
        [HttpGet]  // filtteriattribuutti, vain getti
        [Route("")] // tyhjä: listaa koko taulun
        public List<Customers> GetAll()
        {
            NorthwindContext context = new NorthwindContext();
            List<Customers> all = context.Customers.ToList();

            return all;
        }

        [HttpGet]
        [Route("{customerid}")]
        public Customers GetSingle(string customerid)
        {
            NorthwindContext context = new NorthwindContext();

            if (customerid != null)
            {
                Customers customer = context.Customers.Find(customerid);
                return customer;
            }
            return null;
        }

        [HttpGet]
        [Route("pvm")]
        public string Päivämäärä()
        {
            string pvm = DateTime.Now.ToString();
            return pvm;
        }



        [HttpPost]
        [Route("")]
        public Customers PostCreateNew(Customers customer)  // parametrina oletettu tuleva muoto
        {
            NorthwindContext context = new NorthwindContext();

            context.Customers.Add(customer);
            context.SaveChanges();

            return customer;

        }

        //muokkaus
        [HttpPut]
        [Route("{customerid}")]
        public Customers PutEdit([FromRoute]string customerid, [FromBody]Customers newData)  // hakasuluisssa tieto, mistä parametrit tulevat
        {
            NorthwindContext context = new NorthwindContext();


            if (customerid != null)
            {
                Customers customer = context.Customers.Find(customerid);

                if (customerid != null)
                {
                    customer.CompanyName = newData.CompanyName;
                    customer.ContactName = newData.ContactName;
                    customer.Address = newData.Address;
                    //...
                    context.SaveChanges();  // ideana, että voidaan tehdä muutoksia useisiin objekteihin

                }
                return customer;
            }
            return null;

        }

        //poisto
        [HttpDelete]
        [Route("{customerid}")]
        public Customers Delete([FromRoute]string customerid)  // hakasuluisssa tieto, mistä parametrit tulevat
        {
            NorthwindContext context = new NorthwindContext();


            if (customerid != null)
            {
                Customers customer = context.Customers.Find(customerid);

                if (customerid != null)
                {
                    context.Remove(customer); // myös context.Customers.Remove(customer);
                    
                    context.SaveChanges();  // context. :een ideana, että voidaan tehdä muutoksia useisiin objekteihin

                }
                return customer;
            }
            return null;

        }

    }
}