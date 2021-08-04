#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialKesimFoyuList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a1cdb06c593b0027a802d8acd432d1864b077a9e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartialKesimFoyuList), @"mvc.1.0.view", @"/Views/UretimTakip/_PartialKesimFoyuList.cshtml")]
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
#line 1 "D:\Projects\Eren Production Management\EPM.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projects\Eren Production Management\EPM.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1cdb06c593b0027a802d8acd432d1864b077a9e", @"/Views/UretimTakip/_PartialKesimFoyuList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialKesimFoyuList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<object[]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialKesimFoyuList.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Track.Dto.Track.KesimFoyleri>().ID("edKesimFoyuList")
    .ShowBorders(false)
    .DataSource(
        ob => ob.Mvc().Controller("UretimTakip")
        .LoadParams(new { ITEM_ID = Model[0], PO_HEADER_ID = Model[1], RENK_DETAY = Model[2] })
        .LoadAction("KesimFoyuListLoad")
        .Key("IS_EMRI_ID")
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
            it.AddFor(ob => ob.PLANLANAN_KESIM).SummaryType(SummaryType.Sum);
            it.AddFor(ob => ob.FIILI_KESIM).SummaryType(SummaryType.Sum);
            it.AddFor(ob => ob.TASNIF_MIKTARI).SummaryType(SummaryType.Sum);
        });
    })
    .Columns(cl =>
    { 
        cl.AddFor(m => m.IS_EMRI_NO).Width(120);
        cl.AddFor(m => m.SEZON_BILGISI).Width(100);
        cl.AddFor(m => m.WIP_ENTITY_NAME).Width(100);
        cl.AddFor(m => m.MODEL).Width(70);
        cl.AddFor(m => m.MAMUL_RENGI).Caption("COLOR").Width(100);
        cl.AddFor(m => m.BEDEN).Caption("SIZE").Width(100);
        cl.AddFor(m => m.DEGERLENDIRME).Width(150);
        cl.AddFor(m => m.PLANLANAN_KESIM).Width(100);
        cl.AddFor(m => m.FIILI_KESIM).Width(100);
        cl.AddFor(m => m.TASNIF_MIKTARI).Width(100);
    })

    );

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
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