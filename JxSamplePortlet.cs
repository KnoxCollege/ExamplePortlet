using Jenzabar.Portal.Framework.Web.UI;

namespace Jenzabar.JX.Examples.JICS.Portlet
{
    public class JxSamplePortlet : LinkablePortletBase
    {
	    protected override PortletViewBase GetCurrentScreen()
	    {
			return this.LoadPortletView("ICS/JxSamplePortlet/DefaultView.ascx");
		}
    }
}
