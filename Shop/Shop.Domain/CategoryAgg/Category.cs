using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.CategoryAgg
{
    public class Category:AggregateRoot
    {
        public Category(string title, string slug, SeoData seoData , ICategoryDomainServices services)
        {
            slug = slug?.ToSlug();
            Guard(title , slug, services);
            Title = title;
            Slug = slug;
            SeoData = seoData;
        }

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public SeoData SeoData { get; private set; }
        public long? ParentId { get; private set; }
        public List<Category> Childs { get; private set; }

        public void Edit(string title, string slug, SeoData seoData , ICategoryDomainServices services) 
        {
            slug = slug?.ToSlug();
            Guard(title, slug, services);

            Title = title;
            Slug = slug;
            SeoData = seoData;
        }

        public void AddChild(string title, string slug, SeoData seoData, ICategoryDomainServices services)
        {
            Childs.Add(new Category(title, slug, seoData , services)
            {
                ParentId = Id,
            });
        }

        public void Guard(string title, string slug , ICategoryDomainServices services)
        {
            NullOrEmptyDomainDataException.CheckString(title , nameof(title));
            NullOrEmptyDomainDataException.CheckString(slug , nameof(slug));

            if(slug != Slug)
                if(services.IsSlugExist(slug))
                    throw new SlugIsDuplicateException();
        }
    }
}
