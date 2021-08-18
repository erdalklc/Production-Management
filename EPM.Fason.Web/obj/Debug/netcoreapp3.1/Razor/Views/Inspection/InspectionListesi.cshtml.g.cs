#pragma checksum "E:\Eren\Eren Production Management\EPM.Fason.Web\Views\Inspection\InspectionListesi.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "14639aebc4ed355449c55a4b8e04825ae1e32f71"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Inspection.Views_Inspection_InspectionListesi), @"mvc.1.0.view", @"/Views/Inspection/InspectionListesi.cshtml")]
namespace EPM_Web.Models.Inspection
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
#line 1 "E:\Eren\Eren Production Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Eren\Eren Production Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14639aebc4ed355449c55a4b8e04825ae1e32f71", @"/Views/Inspection/InspectionListesi.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Inspection_InspectionListesi : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EPM.Fason.Dto.Fason.INSPECTION_FILTER>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\Eren\Eren Production Management\EPM.Fason.Web\Views\Inspection\InspectionListesi.cshtml"
  
    ViewData["Title"] = "SiparisListesi";
    Layout = "~/Views/Shared/_LayoutInspection.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div id=\"form-container\">\r\n    <div class=\"dx-card wide-card\">\r\n        ");
#nullable restore
#line 9 "E:\Eren\Eren Production Management\EPM.Fason.Web\Views\Inspection\InspectionListesi.cshtml"
    Write(Html.DevExtreme().Form<EPM.Fason.Dto.Fason.INSPECTION_FILTER>()
        .ID("edOnayliListe").ShowValidationSummary(true)
        .ColCount(Mode.Auto)
        .MinColWidth(150)
        .Items(items =>
        {
            items.AddSimpleFor(m => m.FIRMA_ID).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Inspection").LoadAction("GetFasonUsers").Key("ID")).DisplayExpr("NAME").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.INSPECTOR_ID).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Inspection").LoadAction("GetInspectorList").Key("ID")).DisplayExpr("NAME").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.SEASON).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Inspection").LoadAction("GetSeasonList").Key("ADI")).DisplayExpr("ADI").ValueExpr("ADI")).IsRequired(false);
            items.AddSimpleFor(m => m.MODEL).Editor(ob=>ob.TextBox().ID("edModel")).IsRequired(false);
            items.AddSimpleFor(m => m.COLOR).Editor(ob=>ob.TextBox().ID("edColor")).IsRequired(false);
            items.AddEmpty();
            items.AddButton().HorizontalAlignment(HorizontalAlignment.Right).VerticalAlignment(VerticalAlignment.Center)
                   .ButtonOptions(b => b.Text("FİLTRELE")
                       .Type(ButtonType.Default)
                       .Icon("fas fa-sign-in-alt")
                       .Width("100%")
                       .OnClick("InspectionLisFilter")
                       .UseSubmitBehavior(true)
               );

        })
        .FormData(Model).ShowValidationSummary(false)
    );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <div id=""popup""></div>
        <div id=""popupDetay""></div>
        <div id=""edListe"" style=""display:inline-block;float:left; width:70%""> </div>
        <div id=""edListeDetail"" style=""display: inline-block; float: right; width: 29%""> </div>
        <div id=""edListeProcess""></div>
    </div>
</div> 
<script>
    function InspectionLisFilter() {
    $(""#edListe"").html("""");
    var data = {
        BASLANGIC_TARIHI: jQuery(""[name=BASLANGIC_TARIHI]"").val(),
        BITIS_TARIHI: jQuery(""[name=BITIS_TARIHI]"").val(),
        MODEL: jQuery(""[name=MODEL]"").val(),
        COLOR: jQuery(""[name=COLOR]"").val(),
        SEASON: jQuery(""[name=SEASON]"").val(),
        FIRMA_ID: jQuery(""[name=FIRMA_ID]"").val(),
        INSPECTOR_ID: jQuery(""[name=INSPECTOR_ID]"").val(),
    };
    $.ajax({
        type: ""POST"",
        url: '");
#nullable restore
#line 54 "E:\Eren\Eren Production Management\EPM.Fason.Web\Views\Inspection\InspectionListesi.cshtml"
         Write(Url.Action("_PartialInspectionList", "Inspection"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
        data: data,
        contentType: 'application/x-www-form-urlencoded',
        timeout: 180000,
        success: function (income, status, xmlRequest) {
            $(""#edListe"").html(income);

        }, beforeSend: function () {
            getLoadPanelInstance().show();
        }


    }).always(function () {
        getLoadPanelInstance().hide();
    });
}

</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EPM.Fason.Dto.Fason.INSPECTION_FILTER> Html { get; private set; }
    }
}
#pragma warning restore 1591
