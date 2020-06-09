<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <%@include file="shared/static.jsp"%>
</head>
    <body>
        <!-- HEADER TAG -->
        <div id="pageheader">
            <jsp:include page="shared/header.jsp">
                <jsp:param name="title" value="Login/Register"/>
            </jsp:include>
        </div>
        <!-- END HEADER TAG -->

        <!-- BODY TAG -->
        <div id="base">
            <h1>User form:</h1>
            <form name="form" action="/user_mgmt" method="post">
                <input type="text" name="username" placeholder="Username"/>
                <input type="password" name="password" placeholder="Password"/>
                <div id="show_register" class="btn btn-warning" onclick="show_register()">Register</div>
                <input id="rpassword" class="hide" type="password" name="rpassword" placeholder="Repeat password"/>
                <input class="btn btn-success" type="submit" value="Submit"/>
            </form>
            <br/>
            <div><%= (String)request.getAttribute("message") %></div>
        </div>
        <!-- END BODY TAG -->

        <!-- FOOTER TAG -->
        <div id="pagefooter">
            <%@include file="shared/footer.jsp"%>
        </div>
        <!-- END FOOTER TAG -->
        <script src="${pageContext.request.contextPath}/static/user_mgmt.js"></script>
</body>
</html>