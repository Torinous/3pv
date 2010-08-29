/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 28.08.2010
 * Time: 14:09
 */

namespace Pppv.Utils
{
   using System;

   /// <summary>
   /// This class implements an alias attribute to work in conjunction
   /// with the <see cref="CommandLineSwitchAttribute">CommandLineSwitchAttribute</see>
   /// attribute.  If the CommandLineSwitchAttribute exists, then this attribute
   /// defines an alias for it.
   /// </summary>
   [AttributeUsage(AttributeTargets.Property)]
   public sealed class CommandLineAliasAttribute : System.Attribute
   {
      #region Private Variables
      private string alias = String.Empty;
      #endregion

      #region Constructors
      public CommandLineAliasAttribute(string alias) 
      {
         this.alias = alias;
      }
      #endregion

      #region Public Properties
      public string Alias 
      {
         get { return this.alias; }
      }
      #endregion
   }
}
