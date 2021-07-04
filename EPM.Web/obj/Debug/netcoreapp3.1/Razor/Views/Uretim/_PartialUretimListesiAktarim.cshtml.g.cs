#pragma checksum "E:\Eren\Eren-Production-Management\EPM.Web\Views\Uretim\_PartialUretimListesiAktarim.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dbad8c680c384ec8a3cc850db4fc43d73cea6e80"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Uretim.Views_Uretim__PartialUretimListesiAktarim), @"mvc.1.0.view", @"/Views/Uretim/_PartialUretimListesiAktarim.cshtml")]
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
#line 1 "E:\Eren\Eren-Production-Management\EPM.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Eren\Eren-Production-Management\EPM.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbad8c680c384ec8a3cc850db4fc43d73cea6e80", @"/Views/Uretim/_PartialUretimListesiAktarim.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Uretim__PartialUretimListesiAktarim : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div id=\"fileuploader\">\r\n    <div class=\"widget-container\">\r\n        ");
#nullable restore
#line 3 "E:\Eren\Eren-Production-Management\EPM.Web\Views\Uretim\_PartialUretimListesiAktarim.cshtml"
    Write(Html.DevExtreme().FileUploader()
            .ID("file-uploader")
            .Name("myFile")
            .Multiple(false)
            .Accept(".xlsx").LabelText("Yada Excel Dosyasını Sürükleyip Bırakınız")
            .UploadMode(FileUploadMode.Instantly)
            .UploadUrl(Url.Action("UretimListesiAktarExcelYukle", "Uretim"))
            .OnValueChanged("fileUploader_valueChanged")
            .SelectButtonText("DOSTA SEÇİNİZ")
            .UploadButtonText("Yükle")
            .UploadedMessage("Dosys Başarıyla Yüklendi. İşlemler Tamamlandığında Tarafınıza Mail Bilgilendirmesi Yapılacaktır.") 
            .OnUploadError("uploadError")
            .OnUploadAborted("uploadError") 
        );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <div class=""content"" id=""selected-files"">
            <div>
                <h4>Selected Files</h4>
            </div>
        </div>
    </div>

</div>

<script>
    function uploadError(e) {
        console.log(e.file.name);
        console.log(e.request.responseText);
        setTimeout(function () {
            var $file = e.element.find("".dx-fileuploader-file:contains('"" + e.file.name + ""')"");
            $file.find("".dx-fileuploader-file-status-message"").text(""Error: "" + e.request.responseText);
        }, 400);
    }
    function getFileUploaderInstance() {
        return $(""#file-uploader"").dxFileUploader(""instance"");
    }

    function fileUploader_valueChanged(e) {
        var files = e.value;
        if (files.length > 0) {
            $(""#selected-files .selected-item"").remove();

            $.each(files, function (i, file) {
                var $selectedItem = $(""<div />"").addClass(""selected-item"");
                $selectedItem.append(
                  ");
            WriteLiteral(@"  $(""<span />"").html(""Name: "" + file.name + ""<br/>""),
                    $(""<span />"").html(""Size "" + file.size + "" bytes"" + ""<br/>""),
                    $(""<span />"").html(""Type "" + file.type + ""<br/>""),
                    $(""<span />"").html(""Last Modified Date: "" + file.lastModifiedDate)
                );
                $selectedItem.appendTo($(""#selected-files""));
            });
            $(""#selected-files"").show();
        }
        else
            $(""#selected-files"").hide();
    }

</script>
");
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
