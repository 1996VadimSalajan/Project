using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebApplication3.Models.Validation
{   [AttributeUsage (AttributeTargets.Property,AllowMultiple =false,Inherited =true)]
    public sealed class ValidBirthday:ValidationAttribute,IClientValidatable
    {
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var mvr = new ModelClientValidationRule
            {
                ErrorMessage = "Join date can not be greater than current date.",
                ValidationType = "validbirthday"
            };
            return new[] { mvr };
            
        }

        protected override ValidationResult IsValid( object value,ValidationContext validationContexts)
        {
            DateTime _dateJoin = Convert.ToDateTime(value);
            if (_dateJoin < DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult
                    ("Join date can not be greater than current date.");
            }
        }
      
    }
}