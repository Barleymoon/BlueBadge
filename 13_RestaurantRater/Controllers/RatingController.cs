﻿using _13_RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace _13_RestaurantRater.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestuarantDbContext _context = new RestuarantDbContext();
        // GET: Rating

        [HttpPost]
        public async Task<IHttpActionResult> PostRating(Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok(); // 200
            }
            return InternalServerError(); // 500
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRatings()
        {
            List<RatingListItem> ratings = await _context.Ratings
                 .Select(r => new RatingListItem()
                 {
                     Id = r.Id,
                     // AverageScore = r.AverageScore,
                     FoodScore = r.FoodScore,
                     RestaurantName = r.Restaurant.Name,
                 })
                 .ToListAsync();
            return Ok(ratings);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRatingsForRestaurant(int id)
        {
            List<RatingListItem> ratings = await _context.Ratings
                // LINQ
                .Where(r => r.RestaurantId == id)
                .Select(r => new RatingListItem()
                {
                    Id = r.Id,
                    // AverageScore = r.AverageScore,
                    FoodScore = r.FoodScore,
                    RestaurantName = r.Restaurant.Name,
                })
                .ToListAsync();
            return Ok(ratings);
        }
    }
}