<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>

<% if(request.getSession().getAttribute("admin") == null ){ response.sendRedirect("/web/admin.jsp");} %>

<head>
    <%@include file="shared/static.jsp"%>
</head>
<body>
<section class="shop">
    <!-- HEADER TAG -->
    <div id="pageheader">
        <jsp:include page="shared/header.jsp">
            <jsp:param name="title" value="ADMIN"/>
        </jsp:include>
    </div>
    <!-- END HEADER TAG -->

    <!-- BODY TAG -->
    <div id="base">
        <h1>Welcome <%= request.getSession().getAttribute("user") %>, here are website logs: </h1>
        <div class="shop__products">
            <div class="logs">
                <div>ip|user_id|url|time</div>
                <!-- FILL WITH JS -->
            </div>
        </div>
    </div>
    <!-- END BODY TAG -->
</section>

<!-- FOOTER TAG -->
<div id="pagefooter">
    <%@include file="shared/footer.jsp"%>
</div>
<!-- END FOOTER TAG -->
<script>
    $(document).ready(get_purchases());
    function get_purchases(){
        $.get("/api/get_logs").done(function(d){fill_purchases(d)})
    };

    function fill_purchases(data) {
        [...data].forEach(log => {
            $(".logs").append("<div>" + log + "<br></div>");
        })
    }
</script>

</body>
