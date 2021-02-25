﻿using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _categoryId;

        public CategoryService(Guid categoryId)
        {
            _categoryId = categoryId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    OwnerId = _categoryId,
                    Title = model.Title,
                    Content = model.Content
                };

            using (var ctg = new ApplicationDbContext())
            {
                ctg.Categories.Add(entity);
                return ctg.SaveChanges() == 1;
            }
        }
    }
}
