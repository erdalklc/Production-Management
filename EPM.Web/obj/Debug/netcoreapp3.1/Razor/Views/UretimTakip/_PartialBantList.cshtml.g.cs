#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialBantList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a93dbef5e2162c4f1c244041aaa2c40a6f0031c4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartialBantList), @"mvc.1.0.view", @"/Views/UretimTakip/_PartialBantList.cshtml")]
namespace EPM_Web.Models.UretimTakip
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
#line 1 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a93dbef5e2162c4f1c244041aaa2c40a6f0031c4", @"/Views/UretimTakip/_PartialBantList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialBantList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<object[]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialBantList.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Track.Dto.Track.BantBitisleri>
    ().ID("edBantBitisList")
    .ShowBorders(false)
    .DataSource(
    ob => ob.Mvc().Controller("UretimTakip")
    .LoadParams(new { ITEM_ID = Model[0], PO_HEADER_ID = Model[1], RENK_DETAY = Model[2] })
    .LoadAction("BantBitisListLoad")
    .Key("ISEMRIKAYITID")
    )
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .Scrolling(ob => ob.Mode(GridScrollingMode.Virtual))
    .Height("calc((100vh / 2) - 100px)")
    .ShowColumnLines(true)
    .ShowColumnHeaders(true)
    .ColumnMinWidth(60)
    .Summary(sm =>
    {
    sm.TotalItems(it =>
    {
    it.AddFor(ob => ob.MIKTAR).SummaryType(SummaryType.Sum);
    });
    })
    .Columns(cl =>
    {
    cl.AddFor(m => m.PAZARADI).Width(100);
    cl.AddFor(m => m.MODELADI).Width(100);
    cl.AddFor(m => m.RENKADI).Width(70);
    cl.AddFor(m => m.BEDENID).Caption("COLOR").Width(100);
    cl.AddFor(m => m.BEDENID).Caption("SIZE").Width(100);
    cl.AddFor(m => m.MIKTAR).Width(100);
    })

    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<object[]> Html { get; private set; }
    }
}
#pragma warning restore 1591
