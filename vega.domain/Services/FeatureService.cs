using System;
using System.Collections.Generic;
using Vega.Domain.Entities;

namespace Vega.Domain.Services
{
    public class FeatureService
    {
        public FeatureService()
        {
        }

        public IList<Feature> GetAll() => new List<Feature>() { new Feature() { Id = 1, Name = "feature" } };
    }
}