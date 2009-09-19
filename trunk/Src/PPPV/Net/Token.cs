using System.Collections;

namespace PPPv.Net {
   public class Token{
      private string text;
      /*cons*/
      public Token(){
      }
      public Token(string text_){
         text = text_;
      }
      
      public string Text{
         get{
            return text;
         }
         set{
            text = value;
         }
      }
   }
}

