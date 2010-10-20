﻿namespace Pppv.Verificator
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

   using Pppv.Net;

   public class PetriNetPrologTranslated
   {
      private PetriNet net;

      public PetriNetPrologTranslated()
      {
      }

      public PetriNetPrologTranslated(PetriNet net)
      {
         this.Net = net;
      }

      public PetriNet Net
      {
         get { return this.net; }
         set { this.net = value; }
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string ToProlog()
      {
         StringBuilder text = new StringBuilder(2000);
         text.AppendFormat("%This file is generated by pnml2prolog programm v. {0}.", Assembly.GetExecutingAssembly().GetName().Version.ToString());
         text.AppendLine();
         text.AppendLine(":-use_module(pppv_library(statespace)).");
         text.AppendLine(":-use_module(pppv_library(properties)).");
         text.AppendLine(":-use_module(pppv_library(temporallogic)).");
         text.AppendLine(":-use_module(pppv_library(temporalproperties)).");
         text.AppendLine();
         text.AppendFormat("netname('{0}').", this.Net.Id);
         text.AppendLine();
         text.AppendFormat("nettype('{0}').", this.Net.NetType);
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
         foreach (Transition tr in this.Net.Transitions)
         {
            text.AppendFormat(
                              CultureInfo.InvariantCulture,
                              "arc(S0,{0},S2):-statespace:remove({1},S0,S1),{2}statespace:insert({3},S1,S2).",
                              tr.Name.ToLower(CultureInfo.InvariantCulture),
                              this.Precondition(tr),
                              (!String.IsNullOrEmpty(tr.GuardFunction) ? tr.GuardFunction + "," : String.Empty),
                              this.Postcondition(tr));
            text.AppendLine();
         }

         if (!String.IsNullOrEmpty(this.Net.AdditionalCode))
         {
            text.AppendLine();
            text.AppendLine("%additional code");
            text.AppendLine();
            text.Append(this.Net.AdditionalCode);
            text.AppendLine();
         }

         return text.ToString();
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string InitialMarking()
      {
         StringBuilder text = new StringBuilder(400);
         foreach (Place pl in this.Net.Places)
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
         foreach (Transition transition in this.Net.Transitions)
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
         foreach (Place place in this.Net.Places)
         {
            text.AppendFormat("place({0}).", place.Name.ToLower(CultureInfo.InvariantCulture));
            text.AppendLine();
         }

         return text.ToString();
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Синтасис Prolog обязывает")]
      public string Precondition(INetElement inputTransition)
      {
         StringBuilder text = new StringBuilder(100);
         foreach (Arc arc in this.Net.Arcs)
         {
            if (arc.TargetId == inputTransition.Id)
            {
               Place place = this.Net.GetElementById(arc.SourceId) as Place;
               foreach (Predicate predicate in arc.Cortege.List)
               {
                  text.AppendFormat("{0}({1}),", place.Name.ToLower(CultureInfo.InvariantCulture), predicate);
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
         foreach (Arc arc in this.Net.Arcs)
         {
            if (arc.SourceId == transition.Id)
            {
               foreach (Place place in this.Net.Places)
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