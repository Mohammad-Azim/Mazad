using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.EntityModels;
using Infrastructure.Context;

namespace Application.Services.CategoryService
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}