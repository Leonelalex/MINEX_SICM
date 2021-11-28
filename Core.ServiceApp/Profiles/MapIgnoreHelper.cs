using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAPI.Core.Profiles
{
    public static class MapIgnoreHelper
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }

    }



    // Fuente: https://www.iteramos.com/pregunta/26216/ignorar-la-asignacion-de-una-propiedad-con-automapper
}
