namespace Pppv.Utils
{
   using System;

   /// <summary>Implements a basic command-line switch by taking the
   /// switching name and the associated description.</summary>
   /// <remark>Only currently is implemented for properties, so all
   /// auto-switching variables should have a get/set method supplied.</remark>
   [AttributeUsage(AttributeTargets.Property)]
   public sealed class CommandLineSwitchAttribute : System.Attribute
   {
      #region Private Variables
      private string name = String.Empty;
      private string description = String.Empty;
      #endregion

      #region Constructors
      /// <summary>Attribute constructor.</summary>
      public CommandLineSwitchAttribute(string name, string description)
      {
         this.name = name;
         this.description = description;
      }
      #endregion

      #region Public Properties
      /// <summary>Accessor for retrieving the switch-name for an associated
      /// property.</summary>
      public string Name
      {
         get { return this.name; }
      }

      /// <summary>Accessor for retrieving the description for a switch of
      /// an associated property.</summary>
      public string Description
      {
         get { return this.description; }
      }
      #endregion
   }
}