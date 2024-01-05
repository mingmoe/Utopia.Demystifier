// Copyright (c) Ben A Adams. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;
using Utopia.Demystifier;
using Spectre.Console;

namespace System.Diagnostics
{
    public class ResolvedParameter
    {
        public string? Name { get; set; }

        public Type ResolvedType { get; set; }

        public string? Prefix { get; set; }
        public bool IsDynamicType { get; set; }

        public ResolvedParameter(Type resolvedType) => ResolvedType = resolvedType;

        public override string ToString() => Append(new StringBuilder()).ToString();

        public StringBuilder Append(StringBuilder sb)
        {
            if (ResolvedType.Assembly.ManifestModule.Name == "FSharp.Core.dll" && ResolvedType.Name == "Unit")
                return sb;
            
            if (!string.IsNullOrEmpty(Prefix))
            {
                sb.Append(Prefix)
                  .Append(" ");
            }

            if (IsDynamicType)
            {
                sb.Append("dynamic");
            }
            else if (ResolvedType != null)
            {
                AppendTypeName(sb);
            }
            else
            {
                sb.Append("?");
            }

            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append(" ")
                  .Append(Name);
            }

            return sb;
        }

        public MarkupBuilder Append(MarkupBuilder sb)
        {
            if (ResolvedType.Assembly.ManifestModule.Name == "FSharp.Core.dll" && ResolvedType.Name == "Unit")
                return sb;

            if (!string.IsNullOrEmpty(Prefix))
            {
                sb.AddMarkup($"[white bold]{Markup.Escape(Prefix)}[/]")
                  .Append(" ");
            }

            if (IsDynamicType)
            {
                sb.AddMarkup("[blue]dynamic[/]");
            }
            else if (ResolvedType != null)
            {
                AppendTypeName(sb);
            }
            else
            {
                sb.Append("?");
            }

            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append(" ")
                  .Append(Name);
            }

            return sb;
        }

        protected virtual void AppendTypeName(StringBuilder sb) 
        {
            sb.AppendTypeDisplayName(ResolvedType, fullName: false, includeGenericParameterNames: true);
        }

        protected virtual void AppendTypeName(MarkupBuilder sb)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendTypeDisplayName(ResolvedType, fullName: false, includeGenericParameterNames: true);

            sb.AddMarkup($"[blue]{Markup.Escape(stringBuilder.ToString())}[/]");
        }
    }
}
