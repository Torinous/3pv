using System.Drawing;
using System.Windows.Forms;

namespace PPPv.Net {
  public interface IDrawable {
    void Draw(Graphics dc);
    void Draw(object sender, PaintEventArgs args);
  }
}
