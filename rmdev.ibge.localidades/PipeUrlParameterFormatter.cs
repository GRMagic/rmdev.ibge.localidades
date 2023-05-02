﻿using Refit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace rmdev.ibge.localidades
{
    internal sealed class PipeUrlParameterFormatter : DefaultUrlParameterFormatter
    {
        public override string? Format(object? parameterValue, ICustomAttributeProvider attributeProvider, Type type)
        {
            if (type.IsArray && parameterValue is IEnumerable elementValues)
            {
                var elementType = type.GetElementType()!;
                var elementStrings = new List<string?>();
                foreach (var elementValue in elementValues)
                {
                    var elementString = base.Format(elementValue, attributeProvider, elementType);
                    elementStrings.Add(elementString);
                }
                return string.Join('|', elementStrings);
            }
            return base.Format(parameterValue, attributeProvider, type);
        }

        //public override string Format(object? parameterValue, ParameterInfo parameterInfo)
        //{
        //    if (parameterValue == null)
        //        return null;

        //    // Option 1: Just look for IEnumerable<int>
        //    if (typeof(IEnumerable<int>).IsAssignableFrom(parameterInfo.ParameterType))
        //        return string.Join(",", (IEnumerable<int>)parameterValue);



        //    return base.Format(parameterValue, parameterInfo);
        //}
    }
}