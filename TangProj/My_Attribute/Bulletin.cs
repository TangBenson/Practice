using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My_Attribute.Models;

namespace My_Attribute
{
    public abstract class Bulletin
    {
        public Articles Article { get; set; }
        public string Description { get; set; }
        protected Bulletin()
        {
            ArticlesFactory articlesFactory = new ArticlesFactory();

            Article = articlesFactory.CreateArticles(this);
        }
    }
}