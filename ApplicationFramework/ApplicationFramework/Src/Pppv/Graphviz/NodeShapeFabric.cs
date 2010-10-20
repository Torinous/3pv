/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 19.10.2010
 * Time: 18:24
 *
 */

namespace Pppv.Graphviz
{
   using System;

   using Pppv.ApplicationFramework;

   public static class NodeShapeFabric
   {
      public static string Get(NodeShape shape)
      {
         switch (shape)
         {
            case NodeShape.Rectangle:
               return "rectangle";
            case NodeShape.Circle:
               return "circle";
            case NodeShape.Box:
               return "box";
            case NodeShape.Poligon:
               return "poligon";
            case NodeShape.Point:
               return "point";
            case NodeShape.Triangle:
               return "triangle";
            case NodeShape.Dimond:
               return "diamond";
            case NodeShape.Octagon:
               return "octagon";
            case NodeShape.Hexagon:
               return "hexagon";
            default:
               throw new PppvException("Invalid value for NodeShape");
         }
      }
   }
}
