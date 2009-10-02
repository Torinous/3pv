using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Net {
  public interface IDrawable {
    void Draw(object sender, PaintEventArgs args);
  }
}
