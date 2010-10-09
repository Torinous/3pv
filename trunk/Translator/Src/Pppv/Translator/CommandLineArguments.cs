/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 08.10.2010
 * Time: 21:58
 *
 *
 */

namespace Pppv.Translator
{
   using System;
   using System.Collections.Specialized;
   using System.Text.RegularExpressions;

   public class CommandLineArguments
   {
      private StringDictionary parameters;

      public StringDictionary Parameters
      {
         get { return parameters; }
         set { parameters = value; }
      }

      public CommandLineArguments(string[] args)
      {
         parameters = new StringDictionary();
         Regex Spliter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase|RegexOptions.Compiled);

         Regex Remover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase|RegexOptions.Compiled);

         string Parameter = null;
         string[] Parts;

         // Valid parameters forms:

         // {-,/,--}param{ ,=,:}((",')value(",'))

         // Examples:

         // -param1 value1 --param2 /param3:"Test-:-work"

         //   /param4=happy -param5 '--=nice=--'

         foreach(string Txt in args)
         {
            // Look for new parameters (-,/ or --) and a

            // possible enclosed value (=,:)

            Parts = Spliter.Split(Txt,3);

            switch(Parts.Length)
            {
                  // Found a value (for the last parameter

                  // found (space separator))

               case 1:
                  if(Parameter != null)
                  {
                     if(!parameters.ContainsKey(Parameter))
                     {
                        Parts[0] = Remover.Replace(Parts[0], "$1");

                        parameters.Add(Parameter, Parts[0]);
                     }
                     Parameter=null;
                  }
                  // else Error: no parameter waiting for a value (skipped)

                  break;

                  // Found just a parameter

               case 2:
                  // The last parameter is still waiting.

                  // With no value, set it to true.

                  if(Parameter!=null)
                  {
                     if(!parameters.ContainsKey(Parameter))
                        parameters.Add(Parameter, "true");
                  }
                  Parameter=Parts[1];
                  break;

                  // Parameter with enclosed value

               case 3:
                  // The last parameter is still waiting.

                  // With no value, set it to true.

                  if(Parameter != null)
                  {
                     if(!parameters.ContainsKey(Parameter))
                        parameters.Add(Parameter, "true");
                  }

                  Parameter = Parts[1];

                  // Remove possible enclosing characters (",')

                  if(!parameters.ContainsKey(Parameter))
                  {
                     Parts[2] = Remover.Replace(Parts[2], "$1");
                     parameters.Add(Parameter, Parts[2]);
                  }

                  Parameter=null;
                  break;
            }
         }
         // In case a parameter is still waiting

         if(Parameter != null)
         {
            if(!parameters.ContainsKey(Parameter))
            {
               parameters.Add(Parameter, "true");
            }
         }
      }

      public string this [string param]
      {
         get { return(Parameters[param]); }
      }
   }
}
