using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using My_Attribute.Models;

namespace My_Attribute
{
    public class ArticlesFactory
    {
        public Articles CreateArticles(Bulletin bulletin)
        {
            Articles articles = new Articles
            {
                Author = "大奶妹",
                Title = "幹死她",
                Content = "讓她舔我的大屌",
                PostOn = DateTime.Now
            };

            SilencerAttribute attr = bulletin.GetType().GetCustomAttributes<SilencerAttribute>().FirstOrDefault();


            if (attr == null)
            {
                // throw new InvalidOperationException("SilencerAttribute not found.");
                return articles;
            }

            switch (attr.Mode)
            {
                case SilencerMode.Normal:
                    articles.Author = "d";
                    articles.Title = "幹死她";
                    articles.Content = "讓她舔我的大屌";
                    break;

                case SilencerMode.Strict:
                    articles.Author = "f";
                    articles.Title = "幹死她";
                    articles.Content = "讓她舔我的大屌";
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return articles;

        }
    }
}