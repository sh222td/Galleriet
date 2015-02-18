<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/style.css"/>
    <script src="Script/script.js"></script>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <div>
        </div>
        <div>
            <asp:Label ID="Headline" runat="server" Font-Size="2em" Font-Names="Arial" Text="Gallery"></asp:Label>
        </div>
        <div>
            <fieldset class="imgUpload">
                <legend>Ladda upp en bild</legend>
                <asp:FileUpload ID="FileUploadButton" runat="server" />
                <div class="errorMSG">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Men kära du, välj en bild först!" ControlToValidate="FileUploadButton"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ErrorMessage="Nej nej. Accepterar bara gif, jpg och png filer!" ValidationExpression="^.+(gif|GIF|jpg|JPG|png|PNG)$" ControlToValidate="FileUploadButton"></asp:RegularExpressionValidator>
                </div>
                <div>
                <asp:Button ID="SendButton" runat="server" Text="Upload" OnClick="SendButton_Click"/>
                </div>
            </fieldset>
        </div>
        <asp:placeholder ID="PlaceholderMSG" runat="server" Visible="false">
        <div id="sucessMSG" class="sucessMSG" onclick="removeMSG()">
            <asp:label ID="SucessMSG" runat="server" text="Uppladdning lyckades!"></asp:label>
            <div>
                <asp:Label runat="server" CssClass="removeMSG" Text="Klicka här för att ta bort meddelandet"></asp:Label>
            </div>
        </div>
        </asp:placeholder>
        <asp:placeholder ID="PlaceholderImage" runat="server" Visible="false">
        <div>
            <asp:Image ID="BigImage" runat="server" CssClass="bigImage"/>
        </div>
        </asp:placeholder>
        <div class="imageListScroller">
            <div class="imageList">
                <asp:repeater id="ImageRepeater" runat="server"
                    itemtype="System.String"
                    selectmethod="ImageRepeater_GetData">
                    <HeaderTemplate>
                        <div>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:HyperLink ID="HyperLinkImage" runat="server" NavigateUrl='<%#"?name=/" + Item %>' ImageUrl='<%#"~/Content/Images/ThumbImages/" + Item%>'/>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    </form>
</body>
</html>



