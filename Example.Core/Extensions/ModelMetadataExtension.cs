﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Example.Core.Extensions
{
    public static class ModelMetadataExtension
    {
        internal static Dictionary<Type, SelectListDataCollector> CacheCollector = new Dictionary<Type, SelectListDataCollector>();
        public static List<SelectListItem> GetSelectList(this ModelMetadata modelMetadata)
        {
            var list = new List<SelectListItem>();
            var property = modelMetadata.ContainerType.GetProperty(modelMetadata.PropertyName);
            var attribute = property.GetCustomAttribute(typeof(SelectListDataAttribute), true);
            var selectListDataAttribute = attribute as SelectListDataAttribute;
            if (selectListDataAttribute != null)
            {
                if (typeof(SelectListDataCollector).IsAssignableFrom(selectListDataAttribute.Type))
                {
                    SelectListDataCollector collector;
                    if (!CacheCollector.TryGetValue(selectListDataAttribute.Type, out collector))
                    {
                        collector = Activator.CreateInstance(selectListDataAttribute.Type) as SelectListDataCollector;
                        CacheCollector.Add(selectListDataAttribute.Type, collector);
                    }

                    return collector != null ? collector.Execute() : list;
                }
            }
            return list;
        }
    }

    public class SelectListDataAttribute : Attribute
    {
        public SelectListDataAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; set; }
    }

    public abstract class SelectListDataCollector : Attribute
    {
        public abstract List<SelectListItem> Execute();
    }
}
