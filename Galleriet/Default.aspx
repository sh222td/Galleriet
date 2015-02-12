<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            <asp:Image ID="BigImage" runat="server" Visible="false"/>
        </div>
        <div>
            <asp:FileUpload ID="FileUploadButton" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Men kära du, välj en bild först!" ControlToValidate="FileUploadButton"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Nej nej. Accepterar bara gif, jpg och png filer!" ValidationExpression="^.+(.gif|.GIF|.jpg|.JPG|.png|.PNG)$" ControlToValidate="FileUploadButton"></asp:RegularExpressionValidator>
        </div>
        <div>
            <asp:Button ID="SendButton" runat="server" Text="Upload" OnClick="SendButton_Click"/>
        </div>
        <div>
            <asp:Repeater ID="ImageRepeater" runat="server" 
                ItemType="System.String" 
                SelectMethod ="ImageRepeater_GetData">
                <HeaderTemplate>
                    <div>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:HyperLink ID="HyperLinkImage" runat="server" NavigateUrl='<%#"?name=" + Item %>' ImageUrl='<%#"~/Content/Images/" + Item%>' />
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    </form>
</body>
</html>



