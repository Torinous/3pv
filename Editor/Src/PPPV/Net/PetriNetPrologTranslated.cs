﻿namespace Pppv.Net
{
   using System;
   using System.Globalization;
   using System.IO;
   using System.Reflection;
   using System.Text;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Editor;
   using Pppv.Utils;

   public class PetriNetPrologTranslated : PetriNet
   {
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string ToProlog()
      {
         StringBuilder text = new StringBuilder(2000);
         text.AppendFormat("%This file is generated by pnml2prolog programm v. {0}.", Assembly.GetExecutingAssembly().GetName().Version.ToString());
         text.AppendLine();
         text.AppendLine("%Changes to this file may cause incorrect behavior. Manual editing, only for experts.");
         text.AppendLine();
         text.AppendFormat("netname('{0}').", Id);
         text.AppendLine();
         text.AppendFormat("nettype('{0}').", NetType);
         text.AppendLine();
         text.AppendLine();
         text.Append(this.TransitionsList());
         text.AppendLine();
         text.Append(this.PlacesList());
         text.AppendLine();
         text.AppendLine("%initial marking");
         text.AppendLine();
         text.AppendFormat("init({0}).", this.InitialMarking());
         text.AppendLine();
         text.AppendLine("%transitions semantic");
         text.AppendLine();
         foreach (Transition tr in this.Transitions)
         {
            text.AppendFormat(
                              CultureInfo.InvariantCulture,
                              "arc(S0,{0},S2):-remove({1},S0,S1),{2}insert({3},S1,S2).",
                              tr.Name.ToLower(CultureInfo.InvariantCulture),
                              this.Precondition(tr),
                              (!String.IsNullOrEmpty(tr.GuardFunction) ? tr.GuardFunction + "," : String.Empty),
                              this.Postcondition(tr));
            text.AppendLine();
         }

         /*TODO: Проверить а есть ли дополнительный код*/
         if (this.AdditionalCode.Length != 0)
         {
            text.AppendLine();
            text.AppendLine("%additional code");
            text.AppendLine();
            text.Append(this.AdditionalCode);
            text.AppendLine();
         }

         return text.ToString();
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string InitialMarking()
      {
         StringBuilder text = new StringBuilder(400);
         foreach (Place pl in this.Places)
         {
            foreach (Token token in pl.Tokens)
            {
               text.AppendFormat("{0}({1}),", pl.Name.ToLower(CultureInfo.InvariantCulture), token.Text);
            }
         }

         if (text.Length > 0)
         {
            text.Remove(text.Length - 1, 1);
         }

         text.Append("]");
         text.Insert(0, "[");
         return text.ToString();
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string TransitionsList()
      {
         StringBuilder text = new StringBuilder(400);
         foreach (Transition transition in this.Transitions)
         {
            text.AppendFormat("transition({0}).", transition.Name.ToLower(CultureInfo.InvariantCulture));
            text.AppendLine();
         }

         return text.ToString();
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string PlacesList()
      {
         StringBuilder text = new StringBuilder(400);
         foreach (Place place in this.Places)
         {
            text.AppendFormat("place({0}).", place.Name.ToLower(CultureInfo.InvariantCulture));
            text.AppendLine();
         }

         return text.ToString();
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string Precondition(INetElement inTransition)
      {
         StringBuilder text = new StringBuilder(100);
         foreach (Arc arc in this.Arcs)
         {
            if (arc.TargetId == inTransition.Id)
            {
               foreach (Place place in this.Places)
               {
                  if (place.Id == arc.SourceId)
                  {
                     foreach (Predicate predicate in arc.Cortege.List)
                     {
                        text.AppendFormat("{0}({1}),", place.Name.ToLower(CultureInfo.InvariantCulture), predicate);
                     }
                  }
               }
            }
         }

         if (text.Length > 0)
         {
            text.Remove(text.Length - 1, 1);
         }

         text.Append("]");
         text.Insert(0, "[");
         return text.ToString();
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string Postcondition(INetElement transition)
      {
         StringBuilder text = new StringBuilder(100);
         foreach (Arc arc in this.Arcs)
         {
            if (arc.SourceId == transition.Id)
            {
               foreach (Place place in this.Places)
               {
                  if (place.Id == arc.TargetId)
                  {
                     foreach (Predicate predicate in arc.Cortege.List)
                     {
                        text.AppendFormat("{0}({1}),", place.Name.ToLower(CultureInfo.InvariantCulture), predicate);
                     }
                  }
               }
            }
         }

         if (text.Length > 0)
         {
            text.Remove(text.Length - 1, 1);
         }

         text.Append("]");
         text.Insert(0, "[");
         return text.ToString();
      }
   }
}