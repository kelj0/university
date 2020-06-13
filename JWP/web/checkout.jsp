<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <%@include file="shared/static.jsp"%>
</head>
<body>
    <section class="shop">
        <!-- HEADER TAG -->
        <div id="pageheader">
            <jsp:include page="shared/header.jsp">
                <jsp:param name="title" value="Checkout"/>
            </jsp:include>
        </div>
        <!-- END HEADER TAG -->

        <!-- BODY TAG -->
        <div id="base">
            <div id="cart_items">
                <!-- FILL WITH JS-->
            </div>
            <br/>
            <% if(session.getAttribute("logged_in") != null) {%>
                <input class="btn btn-success" type="submit" value="Buy" onclick="handle_checkout()"/>
            <% }else{ %>
                <a href="/user_mgmt">Log in first</a>
            <%} %>
        </div>
        <!-- END BODY TAG -->
    </section>
    <!-- FOOTER TAG -->
    <div id="pagefooter">
        <%@include file="shared/footer.jsp"%>
    </div>
    <!-- END FOOTER TAG -->
    <script src="${pageContext.request.contextPath}/static/checkout.js"></script>
</body>
