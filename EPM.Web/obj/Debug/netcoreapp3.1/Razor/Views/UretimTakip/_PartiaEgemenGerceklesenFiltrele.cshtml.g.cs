#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartiaEgemenGerceklesenFiltrele.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "930a4c48fcc3be69f9e713324063cf22c3a00c85"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartiaEgemenGerceklesenFiltrele), @"mvc.1.0.view", @"/Views/UretimTakip/_PartiaEgemenGerceklesenFiltrele.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"930a4c48fcc3be69f9e713324063cf22c3a00c85", @"/Views/UretimTakip/_PartiaEgemenGerceklesenFiltrele.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartiaEgemenGerceklesenFiltrele : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EPM.Track.Dto.Track.KaliteGerceklesenFilter>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div class=\"dx-card wide-card\">\r\n    ");
#nullable restore
#line 5 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartiaEgemenGerceklesenFiltrele.cshtml"
Write(Html.DevExtreme().PivotGrid<EPM.Track.Dto.Track.KaliteGerceklesen>().ID("edgridHeader")
    .ShowBorders(false)
    .DataSource(ob => ob.Store(s => s.Mvc().Controller("UretimTakip").LoadParams(Model).LoadAction("EgemenGerceklesenLoad").Key("ID"))
    .Fields(f =>
    {
        f.AddFor(ff => ff.BRAND_NAME).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.SUB_BRAND_NAME).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.SEASON_NAME).Area(PivotGridArea.Column).Expanded(true);
        f.AddFor(ff => ff.MARKET_NAME).Area(PivotGridArea.Row).Expanded(true);
        f.AddFor(ff => ff.ORDER_TYPE).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.PRODUCT_GROUP).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.PRODUCTION_TYPE).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.RECIPE).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.FABRIC_TYPE).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.COLLECTION).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.TEMA).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.ANA_TEMA).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.ROYALTY).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.DEADLINE).Area(PivotGridArea.Filter).Expanded(true);
        f.AddFor(ff => ff.MODEL).Area(PivotGridArea.Row).Expanded(true);
        f.AddFor(ff => ff.COLOR).Area(PivotGridArea.Row).Expanded(true);
        f.AddFor(ff => ff.PRODUCT_SIZE).Area(PivotGridArea.Row).Expanded(true);
        f.AddFor(ff => ff.PLANNED).Area(PivotGridArea.Data)
                    .SummaryType(SummaryType.Sum)
                    .Format(Format.FixedPoint);
        f.AddFor(ff => ff.RELEASED).Area(PivotGridArea.Data)
                    .SummaryType(SummaryType.Sum)
                    .Format(Format.FixedPoint);

    }
    ))
    .ShowBorders(true)
    .Scrolling(ob=>ob.Mode(PivotGridScrollingMode.Virtual))
    .Height("calc(100vh - 150px)")
    .AllowSortingBySummary(true)
        .AllowFiltering(true)
        .ShowBorders(true)
        .ShowColumnGrandTotals(false)
        .ShowRowGrandTotals(true)
        .ShowRowTotals(false)
        .ShowColumnTotals(false)
        .AllowExpandAll(true)
        .FieldPanel(p => p.Visible(true))
        .FieldChooser(c => c.Enabled(true).Height(400))
    .Export(ob => ob.Enabled(true))
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EPM.Track.Dto.Track.KaliteGerceklesenFilter> Html { get; private set; }
    }
}
#pragma warning restore 1591
