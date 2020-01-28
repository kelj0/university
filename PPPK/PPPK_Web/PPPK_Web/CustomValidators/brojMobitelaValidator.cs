using PPPK_Web.HELPERS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PPPK_Web.CustomValidators
{
    public class BrojMobitelaValidator:ValidationAttribute
    {
        public override bool IsValid(object value)
            => (value != null && Validators.validBrojMobitela(value.ToString())) ? true : false;
    }
}