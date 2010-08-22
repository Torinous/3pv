using System.Windows.Forms;

namespace PPPV.Utils
{
	public class RefreshingListBox : ListBox
	{
	    public new void RefreshItem(int index)
	    {
	        base.RefreshItem(index);
	    }
	
	    public new void RefreshItems()
	    {
	        base.RefreshItems();
	    }
	}
}