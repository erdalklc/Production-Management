#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\Uretim\UretimListesiAktarim.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "86ea04581dd5c3c9cb9ffff98a22562907e64eb8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Uretim.Views_Uretim_UretimListesiAktarim), @"mvc.1.0.view", @"/Views/Uretim/UretimListesiAktarim.cshtml")]
namespace EPM_Web.Models.Uretim
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"86ea04581dd5c3c9cb9ffff98a22562907e64eb8", @"/Views/Uretim/UretimListesiAktarim.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Uretim_UretimListesiAktarim : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"dx-card wide-card\" id=\"fileuploader\">\r\n\r\n\r\n    <div class=\"widget-container\">\r\n        ");
#nullable restore
#line 5 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\Uretim\UretimListesiAktarim.cshtml"
    Write(Html.DevExtreme().FileUploader()
            .ID("file-uploader")
            .Name("myFile")
            .Multiple(false)
            .Accept(".xlsx").LabelText("Yada Excel Dosyasını Sürükleyip Bırakınız")
            .UploadMode(FileUploadMode.Instantly)
            .UploadUrl(Url.Action("UretimListesiAktarExcelYukle", "Uretim"))
            .OnValueChanged("fileUploader_valueChanged")
            .SelectButtonText("DOSYA SEÇİNİZ")
            .UploadButtonText("Yükle")
            .UploadedMessage("Dosya Başarıyla Yüklendi. İşlemler Tamamlandığında Tarafınıza Mail Bilgilendirmesi Yapılacaktır.")
            .UploadFailedMessage("Dosya yüklenemedi").OnUploadError("uploadError")
            .OnUploadAborted("uploadError")
        );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"content\" id=\"selected-files\">\r\n            <div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div style=\"margin:20px 10px\"> \r\n        <a class=\"fas fa-file-excel fa-lg\" style=\"border:none;text-decoration:none\"");
            BeginWriteAttribute("href", " href=\'", 1096, "\'", 1141, 1);
#nullable restore
#line 25 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\Uretim\UretimListesiAktarim.cshtml"
WriteAttributeValue("", 1103, Url.Action("SablonDownload","Uretim"), 1103, 38, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" download>\r\n            AKTARIM ŞABLON ÖRNEĞİ\r\n        </a>\r\n    </div>\r\n    <div style=\"margin:20px 10px\"> \r\n        <a class=\"fas fa-file-excel fa-lg\" style=\"border:none;text-decoration:none\"");
            BeginWriteAttribute("href", " href=\'", 1335, "\'", 1389, 1);
#nullable restore
#line 30 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\Uretim\UretimListesiAktarim.cshtml"
WriteAttributeValue("", 1342, Url.Action("SablonBilgileriDownload","Uretim"), 1342, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" download>
            AKTARIM ŞABLON ALAN BİLGİLERİ
        </a>
    </div>
</div>

<script> 
    function uploadError(e) { 
        setTimeout(function () {
            $("".dx-fileuploader-file-status-message"").html(e.request.responseText);
        }, 1000);
    }
    function getFileUploaderInstance() {
        return $(""#file-uploader"").dxFileUploader(""instance"");
    }

    function fileUploader_valueChanged(e) {
        var files = e.value;
        if(files.length > 0) {
            $(""#selected-files .selected-item"").remove();

            $.each(files, function(i, file) {
                var $selectedItem = $(""<div />"").addClass(""selected-item"");
                $selectedItem.append(
                    $(""<span />"").html(""Adı: "" + file.name + ""<br/>""),
                    $(""<span />"").html(""Boyutu "" + file.size + "" bytes"" + ""<br/>""),
                    $(""<span />"").html(""Tipi "" + file.type + ""<br/>""),
                    $(""<span />"").html(""Last Modified Date: "" + file.l");
            WriteLiteral("astModifiedDate)\r\n                );\r\n                $selectedItem.appendTo($(\"#selected-files\"));\r\n            });\r\n            $(\"#selected-files\").show();\r\n        }\r\n        else\r\n            $(\"#selected-files\").hide();\r\n    }\r\n     \r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
