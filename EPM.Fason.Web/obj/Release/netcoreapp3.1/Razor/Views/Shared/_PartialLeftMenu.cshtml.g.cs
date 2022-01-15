#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\Shared\_PartialLeftMenu.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d5b47a7fa1bb1632b409427e8637784ab3cf7fbc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Shared.Views_Shared__PartialLeftMenu), @"mvc.1.0.view", @"/Views/Shared/_PartialLeftMenu.cshtml")]
namespace EPM_Web.Models.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d5b47a7fa1bb1632b409427e8637784ab3cf7fbc", @"/Views/Shared/_PartialLeftMenu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__PartialLeftMenu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 6 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\Shared\_PartialLeftMenu.cshtml"
  
    List<EPM.Fason.Dto.Fason.PRODUCTION_FASON_MENU> menuData = new EPM.Fason.Web.Helpers.MenuHelper().GetFasonMenuList();

    foreach (var item in menuData)
    {
        if (item.ACTION != null)
        {
            item.SELECTED = IsCurrentUrl(item.ACTION);
            if (item.SELECTED)
            {
                if (item.CATEGORY_ID != 0)
                {

                    EPM.Fason.Dto.Fason.PRODUCTION_FASON_MENU menu = menuData.Find(ob => ob.ID == item.CATEGORY_ID);
                    menu.ISEXPANDED = true;
                }
            }

        }
        else item.SELECTED = false;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 29 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\Shared\_PartialLeftMenu.cshtml"
Write(Html.DevExtreme().TreeView()
                .DataSource(menuData)
                .DataSourceOptions(ob=>ob.Map("mapTreeView"))
                .ExpandEvent(TreeViewExpandEvent.Click)
                .SelectionMode(NavSelectionMode.Single)
                .KeyExpr("ID")
                .ParentIdExpr("CATEGORY_ID")
                .SelectedExpr("SELECTED")
                .ExpandedExpr("ISEXPANDED")
                .DisplayExpr("TEXT")
                .DataStructure(TreeViewDataStructure.Plain)
                .Width(220)
                .OnItemClick("EPM_Web.onTreeViewItemClick")
            );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
#nullable restore
#line 1 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\Shared\_PartialLeftMenu.cshtml"
           
    string GetUrl(string action) => Url.Action(action);
    string GetCurrentUrl() => Url.Action(ViewContext.RouteData.Values["action"].ToString() + "" + Context.Request.QueryString.Value.ToString());
    bool IsCurrentUrl(string pageName) => GetUrl(pageName) == GetCurrentUrl();

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
