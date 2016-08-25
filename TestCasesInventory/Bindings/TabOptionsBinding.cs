using System;
using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Config;

namespace TestCasesInventory.Bindings
{
    public class TabOptionsBinding : System.Web.Mvc.IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var tabOptions = new TabOptions();
            var request = controllerContext.HttpContext.Request;
            var activeTab = request.Cookies[TabConfig.ActiveTabCookie];
            var activeTabIndex = activeTab == null ? 0 : activeTab.Value.ToInt(0);
            tabOptions.ActiveTab = activeTabIndex;
            return tabOptions;
        }
    }
}