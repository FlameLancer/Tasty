#pragma checksum "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c7e3e18250f1ae8ba81afbdf3a1acb537789e4be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart_Index), @"mvc.1.0.view", @"/Views/Cart/Index.cshtml")]
namespace AspNetCore
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
#line 1 "D:\Solvsoft\ASP\Tasty\Tasty\Views\_ViewImports.cshtml"
using Tasty;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Solvsoft\ASP\Tasty\Tasty\Views\_ViewImports.cshtml"
using Tasty.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Solvsoft\ASP\Tasty\Tasty\Views\_ViewImports.cshtml"
using Tasty.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7e3e18250f1ae8ba81afbdf3a1acb537789e4be", @"/Views/Cart/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"215700f6592db9c6cf5491a26275dbfe31a45eb1", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CartIndexViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-flex justify-content-center"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RemoveFromCart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("me-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ChangeQuantity", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Checkout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
  
    ViewData["Title"] = "Koszyk";
    ViewBag.Prev = true;
    ViewBag.ReturnUrl = Model.ReturnUrl;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div style=""display: flex; flex-direction: column; margin: 10px 0 0"">
    <div class=""shoplist-item my-border backgroud-color-secondary my-box-shadow"" style=""color: inherit; text-decoration: none; display: flex; flex-direction: column; box-shadow: rgb(0, 0, 0, 0.2) 0px 0px 4px; padding: 20px; margin: 10px 0 0"">
        <h3>Tw??j Koszyk</h3>
        <h5>");
#nullable restore
#line 13 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
       Write(Model.Cart.Shop.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h5>
    </div>
    <table class=""table table-bordered table-striped backgroud-color-secondary my-box-shadow"" style=""box-shadow: rgb(0, 0, 0, 0.2) 0px 0px 4px; margin: 15px 0 "">
        <thead>
            <tr>
                <th></th>
                <th>Ilo????</th>
                <th></th>
                <th>Przedmiot</th>
                <th class=""text-end"">Cena</th>
                <th class=""text-end"">Warto????</th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 27 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
             foreach (var line in Model.Cart.Lines)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7e3e18250f1ae8ba81afbdf3a1acb537789e4be7668", async() => {
                WriteLiteral("\r\n                            <input type=\"hidden\" name=\"ShopId\"");
                BeginWriteAttribute("value", " value=\"", 1303, "\"", 1334, 1);
#nullable restore
#line 32 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 1311, Model.Cart.Shop.ShopId, 1311, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            <input type=\"hidden\" name=\"ItemId\"");
                BeginWriteAttribute("value", " value=\"", 1402, "\"", 1427, 1);
#nullable restore
#line 33 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 1410, line.Item.ItemId, 1410, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            <input type=\"hidden\" name=\"returnUrl\"");
                BeginWriteAttribute("value", " value=\"", 1498, "\"", 1522, 1);
#nullable restore
#line 34 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 1506, Model.ReturnUrl, 1506, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            <button type=\"submit\" class=\"btn btn-sm btn-danger\">\r\n                                Usu??\r\n                            </button>\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </td>\r\n                    <td class=\"text-center\">");
#nullable restore
#line 40 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                                       Write(line.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n                        <div class=\"d-flex justify-content-center\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7e3e18250f1ae8ba81afbdf3a1acb537789e4be11303", async() => {
                WriteLiteral("\r\n                                <input type=\"hidden\" name=\"ShopId\"");
                BeginWriteAttribute("value", " value=\"", 2064, "\"", 2095, 1);
#nullable restore
#line 44 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 2072, Model.Cart.Shop.ShopId, 2072, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                                <input type=\"hidden\" name=\"ItemId\"");
                BeginWriteAttribute("value", " value=\"", 2167, "\"", 2192, 1);
#nullable restore
#line 45 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 2175, line.Item.ItemId, 2175, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                                <input type=\"hidden\" name=\"returnUrl\"");
                BeginWriteAttribute("value", " value=\"", 2267, "\"", 2291, 1);
#nullable restore
#line 46 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 2275, Model.ReturnUrl, 2275, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                                <input type=""hidden"" name=""difference"" value=""1"" />
                                <button type=""submit"" class=""btn btn-sm btn-success"">
                                    <i class=""fa fa-plus"" aria-hidden=""true""></i>
                                </button>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7e3e18250f1ae8ba81afbdf3a1acb537789e4be14686", async() => {
                WriteLiteral("\r\n                                <input type=\"hidden\" name=\"ShopId\"");
                BeginWriteAttribute("value", " value=\"", 2776, "\"", 2807, 1);
#nullable restore
#line 53 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 2784, Model.Cart.Shop.ShopId, 2784, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                                <input type=\"hidden\" name=\"ItemId\"");
                BeginWriteAttribute("value", " value=\"", 2879, "\"", 2904, 1);
#nullable restore
#line 54 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 2887, line.Item.ItemId, 2887, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                                <input type=\"hidden\" name=\"returnUrl\"");
                BeginWriteAttribute("value", " value=\"", 2979, "\"", 3003, 1);
#nullable restore
#line 55 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 2987, Model.ReturnUrl, 2987, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                                <input type=""hidden"" name=""difference"" value=""-1"" />
                                <button type=""submit"" class=""btn btn-sm btn-success"">
                                    <i class=""fa fa-minus"" aria-hidden=""true""></i>
                                </button>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </td>\r\n                    <td class=\"text-start\">");
#nullable restore
#line 63 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                                      Write(line.Item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"text-end\">");
#nullable restore
#line 64 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                                    Write(line.Item.Price.ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"text-end\">\r\n                        ");
#nullable restore
#line 66 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                    Write((line.Quantity * line.Item.Price).ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 70 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n        <tfoot>\r\n            <tr>\r\n");
#nullable restore
#line 74 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                 if (@Model.Cart.CheckWithMinPrice() != 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td colspan=\"4\" class=\"text-end\">Do minimalnej warto??ci zam??wienia brakuje: ");
#nullable restore
#line 76 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                                                                                           Write(Model.Cart.CheckWithMinPrice().ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"text-end\">Razem:</td>\r\n");
#nullable restore
#line 78 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td colspan=\"5\" class=\"text-end\">Razem:</td>\r\n");
#nullable restore
#line 82 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td class=\"text-end\">\r\n                    ");
#nullable restore
#line 84 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
               Write(Model.Cart.ComputeTotalValue().ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td colspan=\"5\" class=\"text-end\">Z dostaw??:</td>\r\n                <td class=\"text-end\">\r\n                    ");
#nullable restore
#line 90 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
               Write(Model.Cart.ComputeTotalValueWithTransport().ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n        </tfoot>\r\n    </table>\r\n\r\n    <div class=\"text-center\">\r\n        <a class=\"btn btn-success\"");
            BeginWriteAttribute("href", " href=\"", 4705, "\"", 4728, 1);
#nullable restore
#line 97 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
WriteAttributeValue("", 4712, Model.ReturnUrl, 4712, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Kontynuuj zakupy</a>\r\n");
#nullable restore
#line 98 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
         if (@Model.Cart.CheckWithMinPrice() != 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <button disabled=\"disabled\" class=\"btn btn-secondary\">Z?????? zam??wienie</button>\r\n");
#nullable restore
#line 101 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7e3e18250f1ae8ba81afbdf3a1acb537789e4be22214", async() => {
                WriteLiteral("Z?????? zam??wienie");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-shopId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 104 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
                                                                                          WriteLiteral(Model.Cart.Shop.ShopId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["shopId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-shopId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["shopId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 105 "D:\Solvsoft\ASP\Tasty\Tasty\Views\Cart\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CartIndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
