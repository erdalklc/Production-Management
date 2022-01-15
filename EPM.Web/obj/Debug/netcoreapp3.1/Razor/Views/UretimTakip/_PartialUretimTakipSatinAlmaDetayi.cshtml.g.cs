#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipSatinAlmaDetayi.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6317d7ae6541e46cce0120eadaa2c0192780baef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartialUretimTakipSatinAlmaDetayi), @"mvc.1.0.view", @"/Views/UretimTakip/_PartialUretimTakipSatinAlmaDetayi.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6317d7ae6541e46cce0120eadaa2c0192780baef", @"/Views/UretimTakip/_PartialUretimTakipSatinAlmaDetayi.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialUretimTakipSatinAlmaDetayi : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipSatinAlmaDetayi.cshtml"
   
    bool ormeVisible = true; 

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"dx-card wide-card\">\r\n    <div style=\"width:calc(100% - 305px);display:inline-block;float:left\">\r\n        ");
#nullable restore
#line 9 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipSatinAlmaDetayi.cshtml"
    Write(Html.DevExtreme().DataGrid<EPM.Track.Dto.Track.SatinAlmaDetay> ().ID("edgridSatinAlmaDetay")
    .ShowBorders(false)
    .DataSource(
        ob=>ob.Mvc().Controller("UretimTakip")
        .LoadAction("SatinAlmaDetayGetir")
        .LoadParams(new { HEADER_ID = Model })
        .Key("DETAIL_TAKIP_NO")
    )
    .FilterRow(f => f.Visible(true))
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .Height("calc((100vh / 2) - 100px)")
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true) 
    .ShowColumnLines(true)
    .ShowColumnHeaders(true) 
    .ColumnMinWidth(30)
    .Scrolling(ob=>ob.Mode(GridScrollingMode.Virtual))
    .Columns(columns => {
        columns.AddFor(m => m.PO_HEADER_ID);
        columns.AddFor(m => m.TAKIP_NO);
        columns.AddFor(m => m.DETAIL_TAKIP_NO);
        columns.AddFor(m => m.FIRMA).Width(250);
        columns.AddFor(m => m.AGENT_NAME).Width(110);
        columns.AddFor(m => m.CREATION_DATE).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Width(110);
        columns.AddFor(m => m.URUN).Width(200);
        columns.AddFor(m => m.KALEM).Width(80);
        columns.AddFor(m => m.MODELDETAY).Width(80);
        columns.AddFor(m => m.RENKDETAY).Width(80);
        columns.AddFor(m => m.BIRIM).Width(80);
        columns.AddFor(m => m.BIRIM_FIYAT).Width(80);
        columns.AddFor(m => m.TEDARIK).Width(80);
        columns.AddFor(m => m.TUTAR).Width(80);
    }).OnFocusedRowChanged("gridSatinAlmaFocusedRowChanged")
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div style=\"width:300px;display:inline-block;float:right\" id=\"edProcessInformations\">\r\n\r\n    </div>\r\n</div>\r\n\r\n");
#nullable restore
#line 51 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipSatinAlmaDetayi.cshtml"
Write(Html.DevExtreme().TabPanel().ID("edDetailTabs")
    .Loop(false)
    .AnimationEnabled(true)
    .SwipeEnabled(true)
    .DeferRendering(false)
    .ActiveStateEnabled(true)
    .Items(itm =>
    {
        if (ormeVisible)
        {
            itm.Add().Title("ÖRME").Html("<div id='edOrmeTab'>ÖRME DETAY</div>");
        }
        itm.Add().Title("KUMAŞ DEPO").Html("<div id='edKumasDepoTab'>KUMAŞ DEPO DETAY</div>");
        itm.Add().Title("KESİM - TASNİF").Html("<div id='edKesimTab'>KESİ DETAY</div>");
        itm.Add().Title("BANT").Html("<div id='edBantTab'>BANT DETAY</div>");
        itm.Add().Title("KALİTE").Html("<div id='edKaliteTab'>KALİTE DETAY</div>");
    })

);

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591
