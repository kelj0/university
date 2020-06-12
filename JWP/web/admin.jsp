<%@ page contentType="text/html;charset=UTF-8" language="java" %>

<% if(request.getSession().getAttribute("admin") != null ){ response.sendRedirect("/web/admin_.jsp");} %>

<html>
<head>
    <%@include file="shared/static.jsp"%>
</head>
<body>
<!-- HEADER TAG -->
<div id="pageheader">
    <jsp:include page="shared/header.jsp">
        <jsp:param name="title" value="Admin login"/>
    </jsp:include>
</div>
<!-- END HEADER TAG -->

<!-- BODY TAG -->
<div id="base">
    <h1>Admin login:</h1>
    <form name="form" action="/admin" method="post">
        <input type="text" name="username" placeholder="Username"/>
        <input type="password" name="password" placeholder="Password"/>
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
</body>
</html>