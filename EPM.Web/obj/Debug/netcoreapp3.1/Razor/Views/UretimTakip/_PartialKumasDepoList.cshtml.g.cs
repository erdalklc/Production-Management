#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialKumasDepoList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "344cae80a9650816af6c453ce01dc27eb7591d91"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartialKumasDepoList), @"mvc.1.0.view", @"/Views/UretimTakip/_PartialKumasDepoList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"344cae80a9650816af6c453ce01dc27eb7591d91", @"/Views/UretimTakip/_PartialKumasDepoList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialKumasDepoList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int[]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    ");
#nullable restore
#line 2 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialKumasDepoList.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Track.Dto.Track.KumasDepo>().ID("edKumasDepoList")
    .ShowBorders(false)
    .DataSource(
        ob => ob.Mvc().Controller("UretimTakip")
        .LoadParams(new { ITEM_ID = Model[0], PO_HEADER_ID = Model[1] })
        .LoadAction("KumasDepoListLoad")
        .Key("TRANSACTION_ID")
    )
    .FocusedRowEnabled(true)
    .Height("calc((100vh / 2) - 100px)")
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .Scrolling(ob => ob.Mode(GridScrollingMode.Virtual))
    .ShowColumnLines(true)
    .ShowColumnHeaders(true)
    .ColumnMinWidth(60)
    .Columns(cl =>
    {
        cl.AddFor(m => m.CREATION_DATE).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Width(100);
        cl.AddFor(m => m.OLUSTURAN).Width(120);
        cl.AddFor(m => m.KALEM).Width(100);
        cl.AddFor(m => m.URUN_GRUBU).Width(170);
        cl.AddFor(m => m.ISLEM_TIPI).Width(120);
        cl.AddFor(m => m.ISLEM_MIKTARI).Width(100);
        cl.AddFor(m => m.BIRIM).Width(100);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int[]> Html { get; private set; }
    }
}
#pragma warning restore 1591
