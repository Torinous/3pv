namespace Pppv.Utils
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Globalization;
   using System.Text.RegularExpressions;

   /// <summary>Implementation of a command-line parsing class.  Is capable of
   /// having switches registered with it directly or can examine a registered
   /// class for any properties with the appropriate attributes appended to
   /// them.</summary>
   [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "не мой код", Scope = "class")]
   public class Parser
   {
      #region Private Variables
      private string commandLine = String.Empty;
      private string workingString = String.Empty;
      private string applicationName = String.Empty;
      private string[] splitParameters;
      private System.Collections.ArrayList switches;
      #endregion

      #region Constructors
      public Parser(string commandLine)
      {
         this.commandLine = commandLine;
      }

      [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Так надо")]
      public Parser(string commandLine, object classForAutoAttributes)
      {
         this.commandLine = commandLine;

         Type type = classForAutoAttributes.GetType();
         System.Reflection.MemberInfo[] members = type.GetMembers();

         for (int i = 0; i < members.Length; i++)
         {
            object[] attributes = members[i].GetCustomAttributes(false);
            if (attributes.Length > 0)
            {
               SwitchRecord rec = null;

               foreach (Attribute attribute in attributes)
               {
                  if (attribute is CommandLineSwitchAttribute)
                  {
                     CommandLineSwitchAttribute switchAttrib = (CommandLineSwitchAttribute) attribute;

                     // Get the property information.  We're only handling
                     // properties at the moment!
                     if (members[i] is System.Reflection.PropertyInfo)
                     {
                        System.Reflection.PropertyInfo pi = (System.Reflection.PropertyInfo) members[i];

                        rec = new SwitchRecord(
                                               switchAttrib.Name,
                                               switchAttrib.Description,
                                               pi.PropertyType);

                        // Map in the Get/Set methods.
                        rec.SetMethod = pi.GetSetMethod();
                        rec.GetMethod = pi.GetGetMethod();
                        rec.PropertyOwner = classForAutoAttributes;

                        // Can only handle a single switch for each property
                        // (otherwise the parsing of aliases gets silly...)
                        break;
                     }
                  }
               }

               // See if any aliases are required.  We can only do this after
               // a switch has been registered and the framework doesn't make
               // any guarantees about the order of attributes, so we have to
               // walk the collection a second time.
               if (rec != null)
               {
                  foreach (Attribute attribute2 in attributes)
                  {
                     if (attribute2 is CommandLineAliasAttribute)
                     {
                        rec.AddAlias(((CommandLineAliasAttribute) attribute2).Alias);
                     }
                  }
               }

               // Assuming we have a switch record (that may or may not have
               // aliases), add it to the collection of switches.
               if (rec != null)
               {
                  if (this.switches == null)
                  {
                     this.switches = new System.Collections.ArrayList();
                  }

                  this.switches.Add(rec);
               }
            }
         }
      }
      #endregion

      #region Properties
      public string ApplicationName
      {
         get { return this.applicationName; }
      }

      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Код не мой, просто давим сообщения")]
      public string[] Parameters
      {
         get { return this.splitParameters; }
      }

      /// <summary>This function returns a list of the unhandled switches
      /// that the parser has seen, but not processed.</summary>
      /// <remark>The unhandled switches are not removed from the remainder
      /// of the command-line.</remark>
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Код не мой, просто давим сообщения")]
      public string[] UnhandledSwitches
      {
         get
         {
            string switchPattern = @"(\s|^)(?<match>(-{1,2}|/)(.+?))(?=(\s|$))";
            Regex r = new Regex(
                                switchPattern,
                                RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            MatchCollection m = r.Matches(this.workingString);

            if (m != null)
            {
               string[] unhandled = new string[m.Count];
               for (int i = 0; i < m.Count; i++)
               {
                  unhandled[i] = m[i].Groups["match"].Value;
               }

               return unhandled;
            }
            else
            {
               return null;
            }
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
      private SwitchInfo[] Switches
      {
         get
         {
            if (this.switches == null)
            {
               return null;
            }
            else
            {
               SwitchInfo[] si = new SwitchInfo[this.switches.Count];
               for (int i = 0; i < this.switches.Count; i++)
               {
                  si[i] = new SwitchInfo(this.switches[i]);
               }

               return si;
            }
         }
      }

      public object this[string name]
      {
         get
         {
            if (this.switches != null)
            {
               for (int i = 0; i < this.switches.Count; i++)
               {
                  if (string.Compare((this.switches[i] as SwitchRecord).Name, name, true, CultureInfo.CurrentCulture) == 0)
                  {
                     return (this.switches[i] as SwitchRecord).ValueObject;
                  }
               }
            }

            return null;
         }
      }
      #endregion

      #region Public Methods
      public void AddSwitch(string name, string description)
      {
         if (this.switches == null)
         {
            this.switches = new System.Collections.ArrayList();
         }

         SwitchRecord rec = new SwitchRecord(name, description);
         this.switches.Add(rec);
      }

      public void AddSwitch(string[] names, string description)
      {
         if (this.switches == null)
         {
            this.switches = new System.Collections.ArrayList();
         }

         SwitchRecord rec = new SwitchRecord(names[0], description);
         for (int s = 1; s < names.Length; s++)
         {
            rec.AddAlias(names[s]);
         }

         this.switches.Add(rec);
      }

      public bool Parse()
      {
         this.ExtractApplicationName();

         // Remove switches and associated info.
         this.HandleSwitches();

         // Split parameters.
         this.SplitParameters();

         return true;
      }

      public object InternalValue(string name)
      {
         if (this.switches != null)
         {
            for (int i = 0; i < this.switches.Count; i++)
            {
               if (string.Compare((this.switches[i] as SwitchRecord).Name, name, true, CultureInfo.CurrentCulture) == 0)
               {
                  return (this.switches[i] as SwitchRecord).InternalValue;
               }
            }
         }

         return null;
      }
      #endregion

      #region Private Utility Functions
      private void ExtractApplicationName()
      {
         Regex r = new Regex(@"^(?<commandLine>(String.Empty.*?String.Empty)|(\S)+)(?<remainder>.+)", RegexOptions.ExplicitCapture);
         Match m = r.Match(this.commandLine);
         if (m != null && m.Groups["commandLine"] != null)
         {
            this.applicationName = m.Groups["commandLine"].Value;
            this.workingString = m.Groups["remainder"].Value;
         }
      }

      private void SplitParameters()
      {
         // Populate the split parameters array with the remaining parameters.
         // Note that if quotes are used, the quotes are removed.
         // e.g.   one two three "four five six"
         //                   0 - one
         //                   1 - two
         //                   2 - three
         //                   3 - four five six
         // (e.g. 3 is not in quotes).
         Regex r = new Regex(@"((\s*(String.Empty(?<param>.+?)String.Empty|(?<param>\S+))))", RegexOptions.ExplicitCapture);
         MatchCollection m = r.Matches(this.workingString);

         if (m != null)
         {
            this.splitParameters = new string[m.Count];
            for (int i = 0; i < m.Count; i++)
            {
               this.splitParameters[i] = m[i].Groups["param"].Value;
            }
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength", Justification = "Код не мой, просто давим сообщения")]
      private void HandleSwitches()
      {
         if (this.switches != null)
         {
            foreach (SwitchRecord s in this.switches)
            {
               Regex r = new Regex(s.Pattern, RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
               MatchCollection m = r.Matches(this.workingString);
               if (m != null)
               {
                  for (int i = 0; i < m.Count; i++)
                  {
                     string value = null;
                     if (m[i].Groups != null && m[i].Groups["value"] != null)
                     {
                        value = m[i].Groups["value"].Value;
                     }

                     if (s.Type == typeof(bool))
                     {
                        bool state = true;

                        // The value string may indicate what value we want.
                        if (m[i].Groups != null && m[i].Groups["value"] != null)
                        {
                           switch (value)
                           {
                                 case "+": state = true;
                                 break;
                                 case "-": state = false;
                                 break;
                                 case "":
                                 if (s.ReadValue != null)
                                 {
                                    state = !(bool)s.ReadValue;
                                 }

                                 break;
                                 default:
                                 break;
                           }
                        }

                        s.Notify(state);
                        break;
                     }
                     else if (s.Type == typeof(string))
                     {
                        s.Notify(value);
                     }
                     else if (s.Type == typeof(int))
                     {
                        s.Notify(int.Parse(value, CultureInfo.CurrentCulture));
                     }
                     else if (s.Type.IsEnum)
                     {
                        s.Notify(System.Enum.Parse(s.Type, value, true));
                     }
                  }
               }

               this.workingString = r.Replace(this.workingString, " ");
            }
         }
      }
      #endregion

      /// <summary>A simple internal class for passing back to the caller
      /// some information about the switch.  The internals/implementation
      /// of this class has privillaged access to the contents of the
      /// SwitchRecord class.</summary>
      private class SwitchInfo
      {
         #region Private Variables
         private object switchObject;
         #endregion

         /// <summary>
         /// Constructor for the SwitchInfo class.  Note, in order to hide to the outside world
         /// information not necessary to know, the constructor takes a System.Object (aka
         /// object) as it's registering type.  If the type isn't of the correct type, an exception
         /// is thrown.
         /// </summary>
         /// <param name="rec">The SwitchRecord for which this class store information.</param>
         /// <exception cref="ArgumentException">Thrown if the rec parameter is not of
         /// the type SwitchRecord.</exception>
         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public SwitchInfo(object rec)
         {
            if (rec is SwitchRecord)
            {
               this.switchObject = rec;
            }
            else
            {
               throw new ArgumentException("rec parameter is not of the type SwitchRecord");
            }
         }

         #region Public Properties
         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public string Name
         { 
            get { return (this.switchObject as SwitchRecord).Name; }
         }

         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public string Description
         {
            get { return (this.switchObject as SwitchRecord).Description; }
         }

         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public string[] Aliases
         {
            get { return (this.switchObject as SwitchRecord).Aliases; }
         }

         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public System.Type Type
         {
            get { return (this.switchObject as SwitchRecord).Type; }
         }

         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public object Value
         {
            get { return (this.switchObject as SwitchRecord).ValueObject; }
         }

         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public object InternalValue
         {
            get { return (this.switchObject as SwitchRecord).InternalValue; }
         }

         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public bool IsEnum
         {
            get { return (this.switchObject as SwitchRecord).Type.IsEnum; }
         }

         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public string[] Enumerations
         {
            get { return (this.switchObject as SwitchRecord).Enumerations; }
         }
         #endregion
      }

      /// <summary>
      /// The SwitchRecord is stored within the parser's collection of registered
      /// switches.  This class is private to the outside world.
      /// </summary>
      private class SwitchRecord
      {
         #region Private Variables
         private string name = String.Empty;
         private string description = String.Empty;
         private object valueObject;
         private System.Type switchType = typeof(bool);
         private System.Collections.ArrayList aliases;
         private string pattern = String.Empty;

         // The following advanced functions allow for callbacks to be
         // made to manipulate the associated data type.
         private System.Reflection.MethodInfo setMethod;
         private System.Reflection.MethodInfo getMethod;
         private object propertyOwner;
         #endregion

         #region Constructors
         public SwitchRecord(string name, string description)
         {
            this.Initialize(name, description);
         }

         public SwitchRecord(string name, string description, System.Type type)
         {
            if (type == typeof(bool) || type == typeof(string) || type == typeof(int) || type.IsEnum)
            {
               this.switchType = type;
               this.Initialize(name, description);
            }
            else
            {
               throw new ArgumentException("Currently only Ints, Bool and Strings are supported");
            }
         }
         #endregion

         #region Public Properties
         public object ValueObject
         {
            get
            {
               if (this.ReadValue != null)
               {
                  return this.ReadValue;
               }
               else
               {
                  return this.valueObject;
               }
            }
         }

         public object InternalValue
         {
            get { return this.valueObject; }
         }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public string Name
         {
            get { return this.name;  }
            set { this.name = value; }
         }

         [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Код не мой, просто давим сообщения")]
         public string Description
         {
            get { return this.description; }
            set { this.description = value; }
         }

         public System.Type Type
         {
            get { return this.switchType; }
         }

         public string[] Aliases
         {
            get { return (this.aliases != null) ? (string[])this.aliases.ToArray(typeof(string)) : null; }
         }

         public string Pattern
         {
            get { return this.pattern; }
         }
         
         public System.Reflection.MethodInfo SetMethod
         {
            set { this.setMethod = value; }
         }
         
         public System.Reflection.MethodInfo GetMethod
         {
            set { this.getMethod = value; }
         }

         public object PropertyOwner
         {
            set { this.propertyOwner = value; }
         }

         public object ReadValue
         {
            get
            {
               object o = null;
               if (this.propertyOwner != null && this.getMethod != null)
               {
                  o = this.getMethod.Invoke(this.propertyOwner, null);
               }

               return o;
            }
         }

         public string[] Enumerations
         {
            get
            {
               if (this.switchType.IsEnum)
               {
                  return System.Enum.GetNames(this.switchType);
               }
               else
               {
                  return null;
               }
            }
         }
         #endregion

         #region Public Methods
         public void AddAlias(string alias)
         {
            if (this.aliases == null)
            {
               this.aliases = new System.Collections.ArrayList();
            }

            this.aliases.Add(alias);

            this.BuildPattern();
         }

         public void Notify(object value)
         {
            if (this.propertyOwner != null && this.setMethod != null)
            {
               object[] parameters = new object[1];
               parameters[0] = value;
               this.setMethod.Invoke(this.propertyOwner, parameters);
            }

            this.valueObject = value;
         }
         #endregion

         #region Private Utility Functions
         private void Initialize(string name, string description)
         {
            this.name = name;
            this.description = description;

            this.BuildPattern();
         }

         private void BuildPattern()
         {
            string matchString = this.Name;

            if (this.Aliases != null && this.Aliases.Length > 0)
            {
               foreach (string s in this.Aliases)
               {
                  matchString += "|" + s;
               }
            }

            string strPatternStart = @"(\s|^)(?<match>(-{1,2}|/)(";
            string strPatternEnd;  // To be defined below.

            // The common suffix ensures that the switches are followed by
            // a white-space OR the end of the string.  This will stop
            // switches such as /help matching /helpme
            string strCommonSuffix = @"(?=(\s|$))";

            if (this.Type == typeof(bool))
            {
               strPatternEnd = @")(?<value>(\+|-){0,1}))";
            }
            else if (this.Type == typeof(string))
            {
               strPatternEnd = @")(?::|\s+))((?:String.Empty)(?<value>.+)(?:String.Empty)|(?<value>\S+))";
            }
            else if (this.Type == typeof(int))
            {
               strPatternEnd = @")(?::|\s+))((?<value>(-|\+)[0-9]+)|(?<value>[0-9]+))";
            }
            else if (this.Type.IsEnum)
            {
               string[] enumNames = this.Enumerations;
               string enumStr = enumNames[0];
               for (int e = 1; e < enumNames.Length; e++)
               {
                  enumStr += "|" + enumNames[e];
               }

               strPatternEnd = @")(?::|\s+))(?<value>" + enumStr + @")";
            }
            else
            {
               throw new System.ArgumentException("Wrong Argument");
            }

            // Set the internal regular expression pattern.
            this.pattern = strPatternStart + matchString + strPatternEnd + strCommonSuffix;
         }
         #endregion
      }
   }
}